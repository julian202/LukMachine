using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

    private double ground = Properties.Settings.Default.ground;
    private double twoVolt = Properties.Settings.Default.twoVolt;
    private bool openValve;
    private bool openValve7;
    private bool closeValve;
    private bool closeValve7;
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
    private bool heater3On;
    private bool heater3Off;
    private bool heater4On;
    private bool heater4Off;
    private bool changedFluidSpeed;
    private Thread myThread;
    public delegate void UpdateTextCallback(string text);


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
      double rawP1 = 0; // Convert.ToDouble(COMMS.Instance.ReadPressureGauge(1));
      double p1Psi = (rawP1 - ground) * Properties.Settings.Default.p1Max / twoVolt;
      aGauge5.Value = (float)p1Psi;

      if (p1Psi < 0)
      {
        p1Psi = 0;
      }
      label1.Text = p1Psi.ToString("#0.000") + " PSI | " + rawP1.ToString() + " cts";

      //read penetrometers
      int ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
      groupBoxReservoir.Text = "Reservoir " + ReservoirPercent.ToString() + "% Full";
      int CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
      groupBoxCollected.Text = "Collected Volume " + CollectedPercent.ToString() + "% Full";

      label2.Text = "Penetrometer 1: " + COMMS.Instance.getReservoirLevelCount() + " (" + ReservoirPercent.ToString() + "%)"; //is COMMS.Instance.MotorValvePosition(1);
      label3.Text = "Penetrometer 2: " + COMMS.Instance.getCollectedLevelCount() + " (" + CollectedPercent.ToString() + "%)"; //is COMMS.Instance.MotorValvePosition(2);

      verticalProgressBar1.Value = ReservoirPercent;
      verticalProgressBar2.Value = CollectedPercent;



      //maybe read temperatures...
      double temp;
      if (checkBox1.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(1);
        checkBox1.Text = "Temp 1: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      if (checkBox2.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(2);
        checkBox2.Text = "Temp 2: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      if (checkBox3.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(3);
        checkBox3.Text = "Temp 3: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      if (checkBox4.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(4);
        checkBox4.Text = "Temp 4: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      //control solenoid valves
      if (openValve)
      {
        openValve = false;
        COMMS.Instance.MoveValve(Convert.ToInt32(comboBox2.Text), "O");
      }
      if (closeValve)
      {
        closeValve = false;
        COMMS.Instance.MoveValve(Convert.ToInt32(comboBox2.Text), "C");
      }
      //3 way valve
      if (openValve7)
      {
        openValve7 = false;
        COMMS.Instance.MoveValve(7, "O");//right chamber
      }
      if (closeValve7)
      {
        closeValve7 = false;
        COMMS.Instance.MoveValve(7, "C");
      }


      //control 3way valve (because it might be different later)
      if (open3Way)
      {
        open3Way = false;
        COMMS.Instance.MoveValve(7, "O");
      }
      if (close3Way)
      {
        close3Way = false;
        COMMS.Instance.MoveValve(7, "C");
      }

      //pump controls (analog out)
      if (startPump1)
      {
        startPump1 = false;
        //COMMS.Instance.IncreaseRegulator(1, trackBar1.Value);
        COMMS.Instance.MoveMotorValve(1, "O");
      }
      if (stopPump1)
      {
        stopPump1 = false;
        //COMMS.Instance.ZeroRegulator(1);
        COMMS.Instance.MoveMotorValve(1, "S");
      }
      if (startPump2)
      {
        startPump2 = false;
        //COMMS.Instance.IncreaseRegulator(2, trackBar2.Value);
        COMMS.Instance.SetRegulator(1, trackBar2.Value);  //4000 is 10V, analog output 1. (which is 0-10V)
      }
      if (stopPump2)
      {
        stopPump2 = false;
        //COMMS.Instance.ZeroRegulator(2);
        COMMS.Instance.ZeroRegulator(1);
      }
      if (heater1On)
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
      if (heater3On)
      {
        heater3On = false;
        COMMS.Instance.SetAthenaTemp(3, Convert.ToDouble(textBox3.Text));
      }
      if (heater3Off)
      {
        heater3Off = false;
        COMMS.Instance.SetAthenaTemp(3, 25.0);
      }
      if (heater4On)
      {
        heater4On = false;
        COMMS.Instance.SetAthenaTemp(4, Convert.ToDouble(textBox4.Text));
      }
      if (heater4Off)
      {
        heater4Off = false;
        COMMS.Instance.SetAthenaTemp(4, 25.0);
      }


      if (changedFluidSpeed)
      {
        changedFluidSpeed = false;

        if (trackBar4.Value == 0)
        {
          //close all 3 valves
          COMMS.Instance.MoveValve(4, "C");
          COMMS.Instance.MoveValve(5, "C");
          COMMS.Instance.MoveValve(6, "C");
        }
        if (trackBar4.Value == 1)
        {
          //slow speed
          COMMS.Instance.MoveValve(4, "O");
          COMMS.Instance.MoveValve(5, "C");
          COMMS.Instance.MoveValve(6, "C");
        }
        if (trackBar4.Value == 2)
        {
          //medium speed
          COMMS.Instance.MoveValve(4, "C");
          COMMS.Instance.MoveValve(5, "O");
          COMMS.Instance.MoveValve(6, "C");
        }
        if (trackBar4.Value == 3)
        {
          //fast speed
          COMMS.Instance.MoveValve(4, "C");
          COMMS.Instance.MoveValve(5, "C");
          COMMS.Instance.MoveValve(6, "O");
        }


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
      //label5.Text = "Counts: " + trackBar2.Value.ToString();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      trackBar1.Value = 0;
      stopPump1 = true;
    }



    private void button3_Click(object sender, EventArgs e)
    {
      //if (trackBar1.Value > 0) startPump1 = true;
      startPump1 = true;
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

    private void button1_Click(object sender, EventArgs e)
    {
      openValve7 = true;
      pictureBox3.Image = global::LukMachine.Properties.Resources.open1;
      pictureBox2.Image = global::LukMachine.Properties.Resources.close;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      closeValve7 = true;
      pictureBox2.Image = global::LukMachine.Properties.Resources.open1;
      pictureBox3.Image = global::LukMachine.Properties.Resources.close;
    }

    private void button11_Click(object sender, EventArgs e)
    {

    }

    private void trackBar1_Scroll(object sender, EventArgs e)
    {

    }

    private void trackBar4_Scroll(object sender, EventArgs e)
    {
      changedFluidSpeed = true;
    }

    private void button11_Click_1(object sender, EventArgs e)
    {
      heater3On = true;
    }

    private void button12_Click(object sender, EventArgs e)
    {
      heater3Off = true;
    }

    private void button13_Click(object sender, EventArgs e)
    {
      heater4On = true;
    }

    private void button14_Click(object sender, EventArgs e)
    {
      heater4Off = true;
    }

    private void button15_Click(object sender, EventArgs e)
    {
      //if (trackBar2.Value > 0) startPump2 = true;    
      trackBar2.Value = trackBar2.Maximum;
      textBox5.Text = ((trackBar2.Value) * 100 / 4000).ToString();
      startPump2 = true;

    }

    private void button16_Click(object sender, EventArgs e)
    {
      trackBar2.Value = 0;
      textBox5.Text = ((trackBar2.Value) * 100 / 4000).ToString();
      stopPump2 = true;
    }

    private void trackBar2_Scroll(object sender, EventArgs e)
    {
      startPump2 = true;
      textBox5.Text = ((trackBar2.Value) * 100 / 4000).ToString();
    }

    private void textBox5_TextChanged(object sender, EventArgs e)
    {
      trackBar2.Value = Convert.ToInt32(textBox5.Text) * 4000 / 100;
      startPump2 = true;
    }

    private void button5_Click(object sender, EventArgs e)
    {
      int newvalue = Convert.ToInt32(Properties.Settings.Default.LowPumpSetting) * 4000 / 100;
      //trackBar2.Value = newvalue;
      textBox5.Text = (newvalue * 100 / 4000).ToString();
      startPump2 = true;
    }

    private void button6_Click(object sender, EventArgs e)
    {
      int newvalue = Convert.ToInt32(Properties.Settings.Default.MediumPumpSetting) * 4000 / 100;
      //trackBar2.Value = newvalue;
      textBox5.Text = (newvalue * 100 / 4000).ToString();
      startPump2 = true;
    }

    private void button19_Click(object sender, EventArgs e)
    {
      int newvalue = Convert.ToInt32(Properties.Settings.Default.HighPumpSetting) * 4000 / 100;
      //trackBar2.Value = newvalue;
      textBox5.Text = (newvalue * 100 / 4000).ToString();
      startPump2 = true;
    }

    private void button20_Click(object sender, EventArgs e)
    {
      //settings button
      settings setScrn = new settings();
      Hide();
      setScrn.ShowDialog();
      Show();
    }

    private void groupBox7_Enter(object sender, EventArgs e)
    {

    }

    string temptext;
    private void button21_Click(object sender, EventArgs e)
    {
      temptext = button21.Text;
      button21.Text = "Please wait until pumping ends";
      button21.Enabled = false;
      myThread = new Thread(myPumpThread);
      myThread.Start();
    }

    public void myPumpThread()
    {
      PumpCollectedVolumeToReservoir(); //this method contains a while loop checking collected volume until its empty.

      //Console.WriteLine("sleeping..");
      //Thread.Sleep(3000);

      //set button text back to it's original text by calling my method UpdateText:
      button21.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { "Text generated on non - UI thread." });
      Console.WriteLine("my thread finished");
    }


    private void UpdateText(string text)
    {
      Console.WriteLine("Entering UpdateText()");
      //button21.Text = text;  // use this if you want to set the text from the other thread.
      button21.Text = temptext;
      button21.Enabled = true;
    }

    public static void PumpCollectedVolumeToReservoir()
    {
      while (COMMS.Instance.getCollectedLevelPercent() > 5) //if collected reservoir is more than 5% full empty it:
      {
        //now refilling reservoir with the liquid from the collected volume
        Valves.OpenValve1();
        Valves.CloseValve2();
        Valves.CloseValve3();
        //start refill pump 1
        Pumps.StartPump1();
      }
      //stop refill pump 1
      Pumps.StopPump1();
      Valves.CloseValve1();

    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
  }
}
