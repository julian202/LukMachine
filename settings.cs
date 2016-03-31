using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LukMachine
{
  public partial class settings : Form
  {
    public settings()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      try
      {
        if ((Convert.ToInt32(textBoxmaxEmptyCollectedPercentFull.Text) >= 0) && (Convert.ToInt32(textBoxmaxEmptyCollectedPercentFull.Text) <= 50))
        {
          Properties.Settings.Default.maxEmptyCollectedPercentFull = Convert.ToInt32(textBoxmaxEmptyCollectedPercentFull.Text);
        }
        else
        {
          MessageBox.Show("Collected volume percent should be between 0 and 50%");
        }
      }
      catch (Exception)
      {
        MessageBox.Show("Collected volume percent should be between 0 and 50%");
        return;
      }
      
      //Properties.Settings.Default.maxEmptyCollectedPercentFull
      try
      {
        if ((Convert.ToDouble(textBoxPressureTolerance.Text) >= 0.1) && (Convert.ToDouble(textBoxPressureTolerance.Text) <= 5))
        {
          Properties.Settings.Default.pressureTolerance = Convert.ToDouble(textBoxPressureTolerance.Text);
        }
        else
        {
          MessageBox.Show("Pressure tolerance should be between 0.1 and 5 PSI");
          return;
        }
      }
      catch (Exception)
      {
        MessageBox.Show("Pressure should be a valid number");
        return;
      }

      try
      {
        if ((Convert.ToDouble(textBoxTemperatureTolerance.Text) >= 1) && (Convert.ToDouble(textBoxTemperatureTolerance.Text) <= 75))
        {
          Properties.Settings.Default.temperatureTolerance = Convert.ToDouble(textBoxTemperatureTolerance.Text);
        }
        else
        {
          MessageBox.Show("Temperature tolerance should be between 1 and 75 deg C");
          return;
        }
      }
      catch (Exception)
      {
        MessageBox.Show("Temperature should be a valid number");
        return;
      }
      


      try
      {
        Properties.Settings.Default.p1Max = Convert.ToInt32(textBoxPressure.Text);
      }
      catch (Exception)
      {
        MessageBox.Show("'Max Gauge Pressure' must be an integer");
      }

      Properties.Settings.Default.LowPumpSetting = textBox1.Text;
      Properties.Settings.Default.MediumPumpSetting = textBox2.Text;
      Properties.Settings.Default.HighPumpSetting = textBox3.Text;

      System.Collections.Specialized.StringCollection pressures = new System.Collections.Specialized.StringCollection();
      System.Collections.Specialized.StringCollection fluids = new System.Collections.Specialized.StringCollection();

      //look at each item and see if there is something there to save
      foreach (DataGridViewRow dRow in dataGridView2.Rows)
      {
        try
        {
          if (dRow.Cells[0].Value.ToString() != "" && dRow.Cells[1].Value.ToString() != "")
            pressures.Add(dRow.Cells[0].Value.ToString() + ":" + dRow.Cells[1].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
          //show error and return
          //MessageBox.Show("Invalid entry for Pressure Unit on line " + (dRow.Index + 1).ToString() + "!", COMMS.Instance.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
          //return;
        }
      }
      //make sure we save any good values user has entered here too...
      foreach (DataGridViewRow dRow in dataGridView3.Rows)
      {
        try
        {
          if (dRow.Cells[0].Value.ToString() != "" && dRow.Cells[1].Value.ToString() != "")
            fluids.Add(dRow.Cells[0].Value.ToString() + ":" + dRow.Cells[1].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
          //show error and return
          //MessageBox.Show("Invalid entry for Fluid/viscosity on line " + (dRow.Index + 1).ToString() + "!", COMMS.Instance.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
          //return;
        }
      }

      Properties.Settings.Default.pressureUnits = pressures;
      Properties.Settings.Default.fluids = fluids;
      Properties.Settings.Default.COMM = comboBox1.Text;
      Properties.Settings.Default.Save();
      Close();
    }

    private void loadPortList()
    {
      //add a selection for demo mode
      comboBox1.Items.Add("Demo");
      foreach (string port in COMMS.Instance.GetAvailablePorts())
      {
        //iterate through available ports and add them for selection.
        comboBox1.Items.Add(port);
        //if we have found our last selected port, set it as the selected item.
        if (port == Properties.Settings.Default.COMM)
        {
          comboBox1.SelectedIndex = comboBox1.FindStringExact(port);
        }
      }

    }

    private void settings_Load(object sender, EventArgs e)
    {
      textBoxmaxEmptyCollectedPercentFull.Text = Properties.Settings.Default.maxEmptyCollectedPercentFull.ToString();
      textBoxPressureTolerance.Text = Properties.Settings.Default.pressureTolerance.ToString();
      textBoxTemperatureTolerance.Text = Properties.Settings.Default.temperatureTolerance.ToString();
      textBoxPressure.Text = Properties.Settings.Default.p1Max.ToString();

      if (Properties.Settings.Default.TempCorF == "C")
      {
        radioButton1.Checked = true;
        radioButton2.Checked = false;
      }
      if (Properties.Settings.Default.TempCorF == "F")
      {
        radioButton1.Checked = false;
        radioButton2.Checked = true;
      }




      textBox1.Text = Properties.Settings.Default.LowPumpSetting;
      textBox2.Text = Properties.Settings.Default.MediumPumpSetting;
      textBox3.Text = Properties.Settings.Default.HighPumpSetting;

      label6.Text = "Software Version: " + COMMS.Instance.version;


      loadPortList();



      //load settings for pressure units
      foreach (string s in Properties.Settings.Default.pressureUnits)
      {
        string[] split = s.Split(':');
        dataGridView2.Rows.Add(split[0], split[1]);
      }
      //load fluids & viscosities
      foreach (string s in Properties.Settings.Default.fluids)
      {
        string[] split = s.Split(':');
        dataGridView3.Rows.Add(split[0], split[1]);
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      //reset to default settings, need to ask the user, "Fo Realz bro?"
      Properties.Settings.Default.Reset();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton1.Checked)
      {
        Properties.Settings.Default.TempCorF = "C";
        Properties.Settings.Default.Save();
      }
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton2.Checked)
      {
        Properties.Settings.Default.TempCorF = "F";
        Properties.Settings.Default.Save();
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      comboBox1.Items.Clear();
      loadPortList();
    }

    private void textBox4_TextChanged(object sender, EventArgs e)
    {
      try
      {
        Properties.Settings.Default.intervalBetweenTimePoints = Convert.ToInt32(textBox4.Text);
      }
      catch (Exception)
      {
        MessageBox.Show("Please enter a valid number");
      }

    }
  }
}
