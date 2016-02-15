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
            label7.Text = "Data File: This field will determine where your burst data is stored.";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            textBox6.Text = saveFileDialog1.FileName;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            label7.Text = "Lot Number: This field is optional, and can be used to further help you classift your samples or tests.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You are about to exit the test setup window, would you like to save your current test setup?", "Burst Tester", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Properties.Settings.Default.mustRunReport = false;
                Properties.Settings.Default.Save();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            if(result == DialogResult.No)
            {
                Properties.Settings.Default.mustRunReport = false;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            if(result == DialogResult.Cancel)
            {
                //this.DialogResult = DialogResult.Cancel;
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Please input a sample ID!", "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Properties.Settings.Default.TestSampleID = textBox1.Text;
            Properties.Settings.Default.TestLotNumber = textBox2.Text;

            try
            {
                int maxPress = Convert.ToInt32(textBox3.Text);
                if (maxPress > 0 && maxPress < Properties.Settings.Default.maxPressure)
                {
                    Properties.Settings.Default.TestMaximumPressure = maxPress;
                }
                else
                {
                    MessageBox.Show("Please enter a whole number for Maximum Pressure that is greater than zero, and less than the machine safe maximum pressure!", "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter a whole number for Maximum Pressure!" + Environment.NewLine + ex.Message, "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Properties.Settings.Default.TestRate = Convert.ToDouble(textBox5.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter a whole/decimal number for Pressure Rate!" + Environment.NewLine + ex.Message, "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int detect = Convert.ToInt32(textBox4.Text);
                if (detect > 50)
                {
                    MessageBox.Show("Please enter a number that is less than 50!", "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Properties.Settings.Default.TestDetection = detect;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter a whole number for Burst Test Detection Pressure!" + Environment.NewLine + ex.Message, "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (textBox6.Text.Length == 0)
            {
                MessageBox.Show("Please choose a data file to save your data to!", "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Properties.Settings.Default.TestData = textBox6.Text;
            }

            if (Properties.Settings.Default.autoReport)
            {
                Properties.Settings.Default.mustRunReport = true;
            }

            if (!radioButton1.Checked)
            {
                Properties.Settings.Default.paper = true;
                Properties.Settings.Default.paperSheets = (int)numericUpDown1.Value;
                try
                {
                    double grammage = Convert.ToDouble(textBox7.Text);
                    Properties.Settings.Default.grammage = grammage;
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Please enter a whole number or decimal for sample grammage!" + Environment.NewLine + ex.Message, "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
            //button2.DialogResult = DialogResult.OK;

        }

        private void Setup_Load(object sender, EventArgs e)
        {
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

            textBox3.Text = Properties.Settings.Default.TestMaximumPressure.ToString();
            textBox5.Text = Properties.Settings.Default.TestRate.ToString();
            textBox4.Text = Properties.Settings.Default.TestDetection.ToString();
        }

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
        }

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
    }
}