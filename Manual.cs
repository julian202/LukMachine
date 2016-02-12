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
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
        }

        private double ground = 2000.0;
        private double twoVolt = 62000.0;
        private bool openValve;
        private bool closeValve;
        private bool startPump1;
        private bool stopPump1;
        private bool startPump2;
        private bool stopPump2;
        private bool open3Way;
        private bool close3Way;
        private bool heater1On;
        private bool heater1Off;
        private bool heater2On;
        private bool heater2Off;

        private void Manual_Load(object sender, EventArgs e)
        {
            double conversionFactor = Properties.Settings.Default.PressureConversionFactor;

            double maxVal = Properties.Settings.Default.p1Max;
            double stepSize = (Math.Round(maxVal * conversionFactor)) / 5;
            aGauge5.MaxValue = (float)Math.Round(maxVal, 2);
            aGauge5.ScaleLinesMajorStepValue = (float)Math.Round(stepSize, 2);
            aGauge5.RangesStartValue[2] = (float)(Math.Round(maxVal - stepSize, 2));
            aGauge5.RangesEndValue[2] = (float)Math.Round(maxVal, 2);

            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //read pressure gauge, convert to PSI (will need to * by conversion factor and set units label later)
            double rawP1 = Convert.ToDouble(COMMS.Instance.ReadPressureGauge(1));
            double p1Psi = (rawP1 - ground) * Properties.Settings.Default.p1Max / twoVolt;
            aGauge5.Value = (float)p1Psi;
            label1.Text = p1Psi.ToString("#0.000") + " PSI | " + rawP1.ToString() + " cts";

            //read penetrometers
            label2.Text = "Penetrometer 1: " + COMMS.Instance.MotorValvePosition(1);
            label3.Text = "Penetrometer 2: " + COMMS.Instance.MotorValvePosition(2);

            //maybe read temperatures...
            if (checkBox1.Checked) checkBox1.Text = "Temp 1: " + COMMS.Instance.ReadAthenaTemp(1);
            if (checkBox2.Checked) checkBox2.Text = "Temp 2: " + COMMS.Instance.ReadAthenaTemp(2);
            if (checkBox3.Checked) checkBox3.Text = "Temp 3: " + COMMS.Instance.ReadAthenaTemp(3);

            //control solenoid valves
            if (openValve)
            {
                openValve = false;
                COMMS.Instance.MoveValve(Convert.ToInt32(comboBox2.Text), "O");
            }
            if(closeValve)
            {
                closeValve = false;
                COMMS.Instance.MoveValve(Convert.ToInt32(comboBox2.Text), "C");
            }

            //control 3way valve (because it might be different later)
            if(open3Way)
            {
                open3Way = false;
                COMMS.Instance.MoveValve(7, "O");
            }
            if(close3Way)
            {
                close3Way = false;
                COMMS.Instance.MoveValve(7, "C");
            }

            //pump controls (analog out)
            if(startPump1)
            {
                startPump1 = false;
                COMMS.Instance.IncreaseRegulator(1, trackBar1.Value);
            }
            if(stopPump1)
            {
                stopPump1 = false;
                COMMS.Instance.ZeroRegulator(1);
            }
            if(startPump2)
            {
                startPump2 = false;
                COMMS.Instance.IncreaseRegulator(2, trackBar2.Value);
            }
            if(stopPump2)
            {
                stopPump2 = false;
                COMMS.Instance.ZeroRegulator(2);
            }
            if(heater1On)
            {
                heater1On = false;
                COMMS.Instance.SetAthenaTemp(1, Convert.ToDouble(textBox1.Text));
            }
            if (heater1Off)
            {
                heater1Off = false;
                COMMS.Instance.SetAthenaTemp(1, 25.0);
            }
            if (heater2On)
            {
                heater2On = false;
                COMMS.Instance.SetAthenaTemp(2, Convert.ToDouble(textBox2.Text));
            }
            if (heater2Off)
            {
                heater2Off = false;
                COMMS.Instance.SetAthenaTemp(2, 25.0);
            }
        }

        private void Manual_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            closeValve = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            openValve = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            groupBox9.Enabled = false;


        }

        private void timer3_Tick(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label4.Text = "Counts: " + trackBar1.Value.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = "Counts: " + trackBar2.Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            stopPump1 = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            trackBar2.Value = 0;
            stopPump2 = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value > 0) startPump1 = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (trackBar2.Value > 0) startPump2 = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            heater1Off = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            heater2Off = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            heater1On = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            heater2On = true;
        }
    }
}
