using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LukMachine
{
  public partial class Setup : Form
  {
    public Setup()
    {
      InitializeComponent();
    }

    private void textBox1_Enter(object sender, EventArgs e)
    {
      label7.Text = "Sample ID: Used to identify different samples, this name will also be used in graphing multiple sets of data.";
    }

    private void textBox3_Enter(object sender, EventArgs e)
    {
      label7.Text = "Maximum Pressure: This is the maximum pressure (in " + Properties.Settings.Default.defaultPressureUnit + ") that you would like to test to achieve during the test. This value can not be larger than the maximum machine safe operating pressure in the machine settings section.";
    }

    private void textBox5_Enter(object sender, EventArgs e)
    {
      label7.Text = "Pressure Rate: In mL/min, this is the rate at which you would like to pressurize the sample.";
    }

    private void textBox4_Enter(object sender, EventArgs e)
    {
      label7.Text = "Burst Detection Pressure: This value represents the amount of pressure loss (" + Properties.Settings.Default.defaultPressureUnit + ") that the software should use to determine if a sample has burst.";
    }

    private void textBox6_Enter(object sender, EventArgs e)
    {
      label7.Text = "Data File: This field will determine where your data is stored.";
    }

    private void button3_Click(object sender, EventArgs e)
    {
      saveFileDialog1.ShowDialog();
      textBox6.Text = saveFileDialog1.FileName;
    }

    private void textBox2_Enter(object sender, EventArgs e)
    {
      label7.Text = "Lot Number: This field is optional, and can be used to further help you classify your samples or tests.";
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
      /*
      DialogResult result = MessageBox.Show("You are about to exit the test setup window, would you like to save your current test setup?", "Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
      {
        Properties.Settings.Default.mustRunReport = false;
        Properties.Settings.Default.Save();
        this.DialogResult = DialogResult.Cancel;
        this.Close();
      }
      if (result == DialogResult.No)
      {
        Properties.Settings.Default.mustRunReport = false;
        Properties.Settings.Default.Save();
        this.DialogResult = DialogResult.Cancel;
        this.Close();
      }
      if (result == DialogResult.Cancel)
      {
        //this.DialogResult = DialogResult.Cancel;
        return;
      }*/
    }


    private void button2_Click(object sender, EventArgs e)
    {
      //parse Inputs For Errors:
      if (dataGridView1.RowCount==0)
      {
        MessageBox.Show("You must add values to the list");
        return;
      }

      if (radioButtonDiskChamber.Checked == false && radioButtonRingChamber.Checked == false)
      {
        MessageBox.Show("Please select the type of chamber!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (radioButtonHigh.Checked == false && radioButtonMedium.Checked == false && radioButtonLow.Checked == false)
      {
        MessageBox.Show("Please select the type of flow rate!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if (textBox1.Text.Length == 0)
      {
        MessageBox.Show("Please input a sample ID!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      if ((textBoxTemperature.Text.Length == 0) && checkBoxTemperature.Checked)
      {
        MessageBox.Show("Please input a temperature!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      int selectedTemp;
      bool isnumber = int.TryParse(textBoxTemperature.Text, out selectedTemp);
      if (!isnumber && checkBoxTemperature.Checked)
      {
        MessageBox.Show("Please input an integer temperature number without decimals", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      //Save selected values
      Properties.Settings.Default.StepCount = dataGridView1.RowCount;
      Properties.Settings.Default.selectedTemp = selectedTemp;
      Properties.Settings.Default.TestSampleID = textBox1.Text;
      Properties.Settings.Default.TestLotNumber = textBox2.Text;
      if (radioButtonHigh.Checked)
      {
        Properties.Settings.Default.SelectedFlowRate = "High";
      }
      else if (radioButtonMedium.Checked)
      {
        Properties.Settings.Default.SelectedFlowRate = "Medium";
      }
      else if (radioButtonLow.Checked)
      {
        Properties.Settings.Default.SelectedFlowRate = "Low";
      }
      Properties.Settings.Default.Save();

      //


      //Save MaximumPressure
      /*try
      {
        int maxPress = Convert.ToInt32(textBox3.Text);
        if (maxPress > 0 && maxPress < Properties.Settings.Default.maxPressure)
        {
          Properties.Settings.Default.TestMaximumPressure = maxPress;
        }
        else
        {
          MessageBox.Show("Please enter a whole number for Maximum Pressure that is greater than zero, and less than the machine safe maximum pressure!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }
      catch (FormatException ex)
      {
        MessageBox.Show("Please enter a whole number for Maximum Pressure!" + Environment.NewLine + ex.Message, "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }*/

      /*//Save pressure rate
      try
      {
        Properties.Settings.Default.TestRate = Convert.ToDouble(textBox5.Text);
      }
      catch (FormatException ex)
      {
        MessageBox.Show("Please enter a whole/decimal number for Pressure Rate!" + Environment.NewLine + ex.Message, "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }*/

      //Save file path
      if (textBox6.Text.Length == 0)
      {
        MessageBox.Show("Please choose a data file to save your data to!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      else
      {
        Properties.Settings.Default.TestData = textBox6.Text;
        Properties.Settings.Default.Save();

      }

      if (Properties.Settings.Default.autoReport)
      {
        Properties.Settings.Default.mustRunReport = true;
        Properties.Settings.Default.Save();

      }

      /*
      Wait waitForm = new Wait();
      if (Properties.Settings.Default.COMM != "Demo")
      {     
        waitForm.Show();
        waitForm.Refresh();
      }*/

      //Move 3-way valve to selected chamber
      if (radioButtonRingChamber.Checked)
      {
        Properties.Settings.Default.Chamber = "Ring";
        Properties.Settings.Default.Save();

        Valves.OpenValve7();//right chamber
      }
      else if (radioButtonDiskChamber.Checked)
      {
        Properties.Settings.Default.Chamber = "Disk";
        Properties.Settings.Default.Save();

        Valves.CloseValve7();//left chamber
      }
      if (Properties.Settings.Default.COMM != "Demo")
      {
        //COMMS.Instance.Pause(1); //wait 1 second just to not start all the valves at the same time.
      }
      //Open manifold valves
      if (Properties.Settings.Default.SelectedFlowRate == "Low")
      {
        Valves.OpenValve4();
        Valves.CloseValve5();
        Valves.CloseValve6();
      }
      if (Properties.Settings.Default.SelectedFlowRate == "Medium")
      {
        Valves.CloseValve4();
        Valves.OpenValve5();
        Valves.CloseValve6();
      }
      if (Properties.Settings.Default.SelectedFlowRate == "High")
      {
        Valves.CloseValve4();
        Valves.CloseValve5();
        Valves.OpenValve6();
      }


      if (Properties.Settings.Default.COMM != "Demo")
      {
        //COMMS.Instance.Pause(7); //wait 7 seconds for valves to finish moving
        //waitForm.Hide();
      }


      Properties.Settings.Default.Save();
      this.DialogResult = DialogResult.OK;
      this.Close();
      //button2.DialogResult = DialogResult.OK;

    }

    private void Setup_Load(object sender, EventArgs e)
    {
      textBoxPressure.Text = Properties.Settings.Default.TextboxPressure;
      textBoxDuration.Text = Properties.Settings.Default.TextboxDuration;

      if (Properties.Settings.Default.useTemperature)
      {
        checkBoxTemperature.Checked = true;
        textBoxTemperature.Enabled = true;
      }
      else
      {
        checkBoxTemperature.Checked = false;
        textBoxTemperature.Enabled = false;
      }

      textBox1.Text = Properties.Settings.Default.TestSampleID;
      textBox2.Text = Properties.Settings.Default.TestLotNumber;
      textBox6.Text = Properties.Settings.Default.TestData;
      textBoxTemperature.Text = Properties.Settings.Default.selectedTemp.ToString();
      //MessageBox.Show(Properties.Settings.Default.Chamber);
      //MessageBox.Show("radioButtonDisk.Checked "+ radioButtonDisk.Checked);
      //MessageBox.Show("radioButtonRing.Checked " + radioButtonRing.Checked);
      if (Properties.Settings.Default.Chamber == "Ring")
      {
        //radioButtonRing.Checked = true;
        radioButtonRingChamber.Checked = true;
      }
      else if (Properties.Settings.Default.Chamber == "Disk")
      {
        //radioButtonDisk.Checked = true;
        radioButtonDiskChamber.Checked = true;
      }




      //MessageBox.Show("radioButtonDisk.Checked " + radioButtonDisk.Checked);
      // MessageBox.Show("radioButtonRing.Checked " + radioButtonRing.Checked);


      if (Properties.Settings.Default.SelectedFlowRate == "High")
      {
        radioButtonHigh.Checked = true;
      }
      else if (Properties.Settings.Default.SelectedFlowRate == "Medium")
      {
        radioButtonMedium.Checked = true;
      }
      else
      {
        radioButtonLow.Checked = true;
      }


      string sampleID = Properties.Settings.Default.TestSampleID;
      string filePath = Properties.Settings.Default.TestData;
      if (Properties.Settings.Default.paper)
      {
        radioButton2.Checked = true;
        numericUpDown1.Enabled = true;
      }
      else
      {
        radioButton1.Checked = true;
        numericUpDown1.Enabled = false;
      }

      if (Properties.Settings.Default.AutoDataFile)
      {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string filePathOnly = Path.GetDirectoryName(filePath);
        //do auto file name shit here.
        if (File.Exists(filePath))
        {

          var numbers = Regex.Match(fileName, @"\d+$").Value;

          if (numbers.ToString() == "")
          {
            //no numbers, add some
            filePath = filePathOnly + @"\" + fileName + "001.pmi";
          }
          else
          {
            //oh crap, there is numbers, lets get them and increment them and then put them back before any sees.
            int newNumbers = Convert.ToInt32(numbers.ToString());
            int oldNumbers = Convert.ToInt32(numbers.ToString());
            while (File.Exists(filePath))
            {
              newNumbers++;
              fileName = fileName.Replace(oldNumbers.ToString("000"), newNumbers.ToString("000"));
              filePath = filePathOnly + @"\" + fileName + ".pmi";
              oldNumbers = newNumbers;
            }
            textBox6.Text = filePath;
            textBox7.Text = Properties.Settings.Default.grammage.ToString();
          }
        }
      }
      //do auto sample ID if selected.
      if (Properties.Settings.Default.AutoSampleID)
      {
        if (sampleID != "")
        {
          var result = Regex.Match(sampleID, @"\d+$").Value;
          if (result.ToString() != "")
          {
            int digits = Convert.ToInt32(result.ToString());
            digits++;
            sampleID = sampleID.Replace(result.ToString(), digits.ToString("000"));
          }
          else
          {
            sampleID += "001";
          }
        }
        textBox1.Text = sampleID;
      }

      //textBox3.Text = Properties.Settings.Default.TestMaximumPressure.ToString();
      //textBox5.Text = Properties.Settings.Default.TestRate.ToString();
      //textBox4.Text = Properties.Settings.Default.TestDetection.ToString();
    }
    /*
    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton1.Checked)
      {
        numericUpDown1.Enabled = false;
        textBox7.Enabled = false;
        Properties.Settings.Default.paper = false;
      }
      else
      {
        numericUpDown1.Enabled = true;
        textBox7.Enabled = true;
        Properties.Settings.Default.paper = true;
      }
    }*/

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {

    }

    private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.ToString() == "." || e.KeyChar.ToString() == ",")
      {
        e.KeyChar = Convert.ToChar("\0");
      }
    }

    private void textBox7_Enter(object sender, EventArgs e)
    {
      label7.Text = "Grammage of the specimen determined in accordance with the requirements of BS 3432(g/m²)";
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {

    }

    private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void label8_Click(object sender, EventArgs e)
    {

    }

    private void radioButtonRing_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void checkBoxTemperature_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxTemperature.Checked)
      {
        Properties.Settings.Default.useTemperature = true;
        textBoxTemperature.Enabled = true;
      }
      else
      {
        Properties.Settings.Default.useTemperature = false;
        textBoxTemperature.Enabled = false;
      }
      Properties.Settings.Default.Save();
    }

    private void textBox4_TextChanged(object sender, EventArgs e)
    {
      /*
      Properties.Settings.Default.flowRate = textBox4.Text;
      double flowrate = Convert.ToDouble(textBox4.Text);

      if (flowrate >=0 && flowrate < 50)
      {
        radioButtonLow.Checked = true;
        radioButtonMedium.Checked = false;
        radioButtonHigh.Checked = false;
      }
      else if (flowrate >= 50 && flowrate < 200)
      {
        radioButtonLow.Checked = false;
        radioButtonMedium.Checked = true;
        radioButtonHigh.Checked = false;
      }
      else if (flowrate >= 200 && flowrate < 1000)
      {
        radioButtonLow.Checked = false;
        radioButtonMedium.Checked = false;
        radioButtonHigh.Checked = true;
      }
      else
      {
        MessageBox.Show("Value must be between 0 and 1000");
      }
      */
    }

    private void radioButtonHigh_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void textBoxTemperature_TextChanged(object sender, EventArgs e)
    {

    }

    private void buttonAdd_Click(object sender, EventArgs e)
    {
      string pressure;
      string duration;
      string temperature;
      double n;

      //parse double inputs and add to new row in list
      if (double.TryParse(textBoxPressure.Text, out n))
      {
        pressure = n.ToString();
      }
      else
      {
        MessageBox.Show("Please enter a valid number for pressure");
        return;
      }

      if (double.TryParse(textBoxDuration.Text, out n))
      {
        duration = n.ToString();
      }
      else
      {
        MessageBox.Show("Please enter a valid number for duration");
        return;
      }

      if (checkBoxTemperature.Checked)
      {
        if (double.TryParse(textBoxTemperature.Text, out n))
        {
          temperature = n.ToString();
        }
        else
        {
          MessageBox.Show("Please enter a valid number for temperature");
          return;
        }
      }
      else
      {
        temperature = "-";
      }

      dataGridView1.Rows.Add(pressure, duration, temperature);
      saveValues();
    }

    private void saveValues()
    {
      Properties.Settings.Default.CollectionPressure.Clear();
      Properties.Settings.Default.CollectionDuration.Clear();
      Properties.Settings.Default.CollectionTemperature.Clear();
      DataGridViewRowCollection drc = dataGridView1.Rows;
      foreach (DataGridViewRow item in drc)
      {
        Properties.Settings.Default.CollectionPressure.Add(item.Cells[0].Value.ToString());
        Properties.Settings.Default.CollectionDuration.Add(item.Cells[1].Value.ToString());
        Properties.Settings.Default.CollectionTemperature.Add(item.Cells[2].Value.ToString());
      }
      Properties.Settings.Default.Save();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      foreach (DataGridViewCell oneCell in dataGridView1.SelectedCells)
      {
        if (oneCell.Selected)
          dataGridView1.Rows.RemoveAt(oneCell.RowIndex);
      }
      saveValues();
    }

    private void textBoxPressure_TextChanged(object sender, EventArgs e)
    {
      Properties.Settings.Default.TextboxPressure = textBoxPressure.Text;
      Properties.Settings.Default.Save();
    }

    private void textBoxDuration_TextChanged(object sender, EventArgs e)
    {
      Properties.Settings.Default.TextboxDuration = textBoxDuration.Text;
      Properties.Settings.Default.Save();
    }



    private void button6_Click(object sender, EventArgs e)
    {
      dataGridView1.Rows.Clear();
      for (int row = 0; row < Properties.Settings.Default.CollectionPressure.Count ; row++)
      {
        dataGridView1.Rows.Add(Properties.Settings.Default.CollectionPressure[row], Properties.Settings.Default.CollectionDuration[row], Properties.Settings.Default.CollectionTemperature[row]);
      }
    }

  }
}