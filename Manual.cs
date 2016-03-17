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

    private double ground;
    private double twoVolt;
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
    bool Valve3wayToRight;
    bool Valve3wayBToRight;
    double outputPressure = 0; string counts = ""; double realCounts = 0; double currentPressure;
    private double pConversion = Properties.Settings.Default.defaultPressureConversion;
    double p1Psi;
    double p2Psi;
    double rawP1;
    bool MainPumpOn;
    bool RefillPumpOn;
    int targetPressure = 0;
    bool firstSetOfChamberCheckbox;
    double startTime;
    int currentVolume;
    double elapsedTime;
    int counter;
    private void Manual_Load(object sender, EventArgs e)
    {
      startTime = Convert.ToDouble(Environment.TickCount);

      Properties.Settings.Default.ground= COMMS.Instance.getGround();
      Properties.Settings.Default.RefCount2V = COMMS.Instance.get2v();
      labelGround.Text = "Ground = " + Properties.Settings.Default.ground;
      label2V.Text = "2V = " + Properties.Settings.Default.RefCount2V;
      int Ref0V = Convert.ToInt32(Properties.Settings.Default.ground);
      int Ref2V = Properties.Settings.Default.RefCount2V;
      Properties.Settings.Default.RefCount10V = Ref0V + Convert.ToInt32((Ref2V - Ref0V) * 4.75);// or 5;  4.75 is 9.5/2 becuase 9.5V is the voltage when it is empty
      label10V.Text = "10V = " + Properties.Settings.Default.RefCount10V;
      textBoxFlow.Select();
      ground = Properties.Settings.Default.ground;
      twoVolt = Properties.Settings.Default.twoVolt;

      firstSetOfChamberCheckbox = true;
      if (Properties.Settings.Default.checkbLeftChecked)
      {
        checkBoxLeftChamber.Checked = true;
        checkBoxRightChamber.Checked = false;
      }
      else
      {
        checkBoxLeftChamber.Checked = false;
        checkBoxRightChamber.Checked = true;

      }
      firstSetOfChamberCheckbox = false;

      if (Properties.Settings.Default.RefillPumpState)
      {
        rectangleShape14.BackColor = Color.Bisque;
        label21.BackColor = Color.Bisque;
        label22.BackColor = Color.Bisque;
        label22.Text = "Pump ON";
        RefillPumpOn = true;
      }
      else
      {
        rectangleShape14.BackColor = Color.Gray;
        label21.BackColor = Color.Gray;
        label22.BackColor = Color.Gray;
        label22.Text = "Pump OFF";
        RefillPumpOn = false;
      }

      updateValveColors();
      //updateValveColors doesn't do valve 7 just becuase it flickers so do it here now:
      if (Properties.Settings.Default.Valve7State==false)
      {
        rectangleShape20.BackgroundImage = global::LukMachine.Properties.Resources._112_RightArrowShort_Green_32x32_72;
        //checkBoxLeftChamber.Checked = true;
        //checkBoxRightChamber.Checked = false;
        Valve3wayToRight = false;

      }
      else
      {
        rectangleShape20.BackgroundImage = global::LukMachine.Properties.Resources._112_LeftArrowShort_Green_32x32_72;
        //checkBoxLeftChamber.Checked = false;
        //checkBoxRightChamber.Checked = true;
        Valve3wayToRight = true;
      }

      if (Properties.Settings.Default.Valve8State)
      {
        rectangleShape23.BackgroundImage = global::LukMachine.Properties.Resources._112_UpLeftArrowShort_Green_32x32_72;
        Valve3wayBToRight = true;
      }
      else
      {
        rectangleShape23.BackgroundImage = global::LukMachine.Properties.Resources._112_LeftArrowShort_Green_32x32_72;
        Valve3wayBToRight = false;
      }


      double conversionFactor = Properties.Settings.Default.PressureConversionFactor;
      double maxVal = Properties.Settings.Default.p1Max;
      double stepSize = (Math.Round(maxVal * conversionFactor)) / 5;
      aGauge5.MaxValue = (float)Math.Round(maxVal, 2);
      aGauge5.ScaleLinesMajorStepValue = (float)Math.Round(stepSize, 2);
      aGauge5.RangesStartValue[2] = (float)(Math.Round(maxVal - stepSize, 2));
      aGauge5.RangesEndValue[2] = (float)Math.Round(maxVal, 2);
      aGauge1.MaxValue = (float)Math.Round(maxVal, 2);
      aGauge1.ScaleLinesMajorStepValue = (float)Math.Round(stepSize, 2);
      aGauge1.RangesStartValue[2] = (float)(Math.Round(maxVal - stepSize, 2));
      aGauge1.RangesEndValue[2] = (float)Math.Round(maxVal, 2);
      //Valve3wayToRight = true;
      MainPumpOn = false;
      //RefillPumpOn = false;
      timer1.Enabled = true;

    }

    bool refilldone = false;
    bool startRefill = false;
    //ManualWait form = new ManualWait();

    private void updateValveColors()
    {
      if (Properties.Settings.Default.Valve1State)
      {
        rectangleShape12.BackColor = Color.Green;
      }
      else
      {
        rectangleShape12.BackColor = Color.Brown;
      }

      if (Properties.Settings.Default.Valve2State)
      {
        rectangleShape11.BackColor = Color.Green;
      }
      else
      {
        rectangleShape11.BackColor = Color.Brown;
      }

      if (Properties.Settings.Default.Valve3State)
      {
        rectangleShape6.BackColor = Color.Green;
      }
      else
      {
        rectangleShape6.BackColor = Color.Brown;
      }

      if (Properties.Settings.Default.Valve4State)
      {
        rectangleShape2.BackColor = Color.Green;
      }
      else
      {
        rectangleShape2.BackColor = Color.Brown;
      }

      if (Properties.Settings.Default.Valve5State)
      {
        rectangleShape4.BackColor = Color.Green;
      }
      else
      {
        rectangleShape4.BackColor = Color.Brown;
      }

      if (Properties.Settings.Default.Valve6State)
      {
        rectangleShape5.BackColor = Color.Green;
      }
      else
      {
        rectangleShape5.BackColor = Color.Brown;
      }     
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      //updateValveColors();  //gets values from properties
      elapsedTime = (Convert.ToDouble(Environment.TickCount)-startTime)/1000;
      labelTime.Text = "Time (s): "+ elapsedTime.ToString("#0");

      counter++;
      if (counter>5)
      {
        counter = 0;
      }



      if (startRefill)
      {
        /*
        if (!form.Visible)
        {
          form.ShowDialog();
        }*/
        button21.Text= "Pumping... Please wait...";
        button21.Enabled = false;
        //turn refill pump on:
        rectangleShape14.BackColor = Color.Bisque;
        label21.BackColor = Color.Bisque;
        label22.BackColor = Color.Bisque;
        label22.Text = "Pump ON";
        Pumps.StartPump1();
        RefillPumpOn = true;
        if (COMMS.Instance.getCollectedLevelPercent() < 5)
        {
          refilldone = true;
        }
      }

      if (refilldone)
      {
        Pumps.StopPump1();
        Valves.CloseValve1();
        //form.Hide();
        button21.Text = "Pump Collected back to Reservoir";
        button21.Enabled = true;
        refilldone = false;
        startRefill = false;
        //stop refill pump:
        rectangleShape14.BackColor = Color.Gray;
        label21.BackColor = Color.Gray;
        label22.BackColor = Color.Gray;
        label22.Text = "Pump OFF";
        Pumps.StopPump1();
        RefillPumpOn = false;
      }
    
      //read penetrometers
      int ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
      groupBoxReservoir.Text = "Reservoir " + ReservoirPercent.ToString() + "% Full";
      label29.Text = ReservoirPercent.ToString() + "% Full";
      mLReservoir.Text = (ReservoirPercent * Convert.ToInt32(Properties.Settings.Default.MaxCapacityInML)/100).ToString() + " mL";

      int CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
      groupBoxCollected.Text = "Collected Volume " + CollectedPercent.ToString() + "% Full";
      label28.Text = CollectedPercent.ToString() + "% Full";
      mlCollected.Text = (CollectedPercent * Convert.ToInt32(Properties.Settings.Default.MaxCapacityInML)/100).ToString() + " mL";
      currentVolume = Convert.ToInt32(mlCollected.Text);

      labelCollectedCount.Text = COMMS.Instance.getCollectedLevelCount()+ " counts";
      labelReservoirCounts.Text = COMMS.Instance.getReservoirLevelCount() + " counts";

      label2.Text = "Penetrometer 1: " + COMMS.Instance.getReservoirLevelCount() + " (" + ReservoirPercent.ToString() + "%)"; //is COMMS.Instance.MotorValvePosition(1);
      label3.Text = "Penetrometer 2: " + COMMS.Instance.getCollectedLevelCount() + " (" + CollectedPercent.ToString() + "%)"; //is COMMS.Instance.MotorValvePosition(2);

      try
      {
        verticalProgressBar1.Value = ReservoirPercent;
        verticalProgressBar2.Value = CollectedPercent;
        verticalProgressBar3.Value = CollectedPercent;
        verticalProgressBar4.Value = ReservoirPercent;
      }
      catch (Exception ex)
      {
        //MessageBox.Show(ex.Message);
      }

      //update valve colors:
      updateValveColors();  //gets values from properties


      //maybe read temperatures...
      double temp;
      if (checkBox1.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(1);
        checkBox1.Text = "Chamber 1: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      if (checkBox2.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(2);
        checkBox2.Text = "Chamber 2: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      if (checkBox3.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(3);
        checkBox3.Text = "Reservoir 1: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      if (checkBox4.Checked)
      {
        temp = COMMS.Instance.ReadAthenaTemp(4);
        checkBox4.Text = "Pipe Temp: " + temp + "F / " + Math.Round((temp - 32) * 5 / 9) + "C";
      }
      //control solenoid valves
      if (openValve)
      {
        openValve = false;
        Valves.OpenValve(Convert.ToInt32(comboBox2.Text));
      }
      if (closeValve)
      {
        closeValve = false;
        Valves.CloseValve(Convert.ToInt32(comboBox2.Text));
      }
      //3 way valve
      if (openValve7)
      {
        openValve7 = false;
        Valves.OpenValve(7);//right chamber

      }
      if (closeValve7)
      {
        closeValve7 = false;
        Valves.CloseValve(7);
      }

      //control 3way valve (because it might be different later)
      if (open3Way)
      {
        open3Way = false;
        Valves.OpenValve(7);//right chamber
      }
      if (close3Way)
      {
        close3Way = false;
        Valves.CloseValve(7);
      }

      //pump controls (analog out)
      if (startPump1)
      {
        startPump1 = false;
        Pumps.StartPump1();
      }
      if (stopPump1)
      {
        stopPump1 = false;
        Pumps.StopPump1();
      }
      if (startPump2)
      {
        startPump2 = false;
        //COMMS.Instance.IncreaseRegulator(2, trackBar2.Value);
        //trackBar2.Value = trackBar3.Value;
        Pumps.SetPump2(trackBar3.Value * 100 / 4000); //4000 is 10V, analog output 1. (which is 0-10V)
        rectangleShape15.BackColor = Color.Beige;
        label23.BackColor = Color.Beige;
        label17.BackColor = Color.Beige;
      }
      if (stopPump2)
      {
        stopPump2 = false;
        Pumps.SetPump2(0);
        rectangleShape15.BackColor = Color.Gray;
        label23.BackColor = Color.Gray;
        label17.BackColor = Color.Gray;
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
          Valves.CloseValve4();
          Valves.CloseValve5();
          Valves.CloseValve6();
        }
        if (trackBar4.Value == 1)
        {
          //slow speed
          Valves.OpenValve4();
          Valves.CloseValve5();
          Valves.CloseValve6();
        }
        if (trackBar4.Value == 2)
        {
          //medium speed
          Valves.CloseValve4();
          Valves.OpenValve5();
          Valves.CloseValve6();
        }
        if (trackBar4.Value == 3)
        {
          //fast speed
          Valves.CloseValve4();
          Valves.CloseValve5();
          Valves.OpenValve6();
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
      System.Diagnostics.Debug.WriteLine("my thread finished");
    }


    private void UpdateText(string text)
    {
      System.Diagnostics.Debug.WriteLine("Entering UpdateText()");
      //button21.Text = text;  // use this if you want to set the text from the other thread.
      button21.Text = temptext;
      button21.Enabled = true;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void verticalProgressBar3_Click(object sender, EventArgs e)
    {

    }

    private void verticalProgressBar4_Click(object sender, EventArgs e)
    {

    }

    private void changeColor(object sender)
    {
      if (((Microsoft.VisualBasic.PowerPacks.RectangleShape)sender).BackColor == Color.Green)
      {
        ((Microsoft.VisualBasic.PowerPacks.RectangleShape)sender).BackColor = Color.Brown;
        //MessageBox.Show(((Microsoft.VisualBasic.PowerPacks.RectangleShape)sender).Name);
      }
      else
      {
        ((Microsoft.VisualBasic.PowerPacks.RectangleShape)sender).BackColor = Color.Green;
      }
    }

    private void rectangleShape2_Click(object sender, EventArgs e)
    {
      if (rectangleShape2.BackColor == Color.Green)
      {
        Valves.CloseValve4();
      }
      else
      {
        Valves.OpenValve4();
       
      }
      changeColor(sender);
    }

    private void rectangleShape4_Click(object sender, EventArgs e)
    {
      if (rectangleShape4.BackColor == Color.Green)
      {
        Valves.CloseValve5();
      }
      else
      {
        Valves.OpenValve5();
      }
      changeColor(sender);
    }

    private void rectangleShape5_Click(object sender, EventArgs e)
    {
      if (rectangleShape5.BackColor == Color.Green)
      {
        Valves.CloseValve6();
      }
      else
      {
        Valves.OpenValve6();
      }
      changeColor(sender);
    }

    private void rectangleShape11_Click(object sender, EventArgs e)
    {
      /*
      if (rectangleShape11.BackColor == Color.Green)
      {
        Valves.CloseValve2();
      }
      else
      {
        Valves.OpenValve2();
      }
      changeColor(sender);*/
    }

    private void rectangleShape6_Click(object sender, EventArgs e)
    {
      if (rectangleShape6.BackColor == Color.Green)
      {
        Valves.CloseValve3();
       
      }
      else
      {
        Valves.OpenValve3();
      }
      changeColor(sender);
    }

    private void rectangleShape12_Click(object sender, EventArgs e)
    {
      if (rectangleShape12.BackColor == Color.Green)
      {
        Valves.CloseValve1();
    
      }
      else
      {
        Valves.OpenValve1();

      }
      changeColor(sender);
    }

    private void panelleft_Paint(object sender, PaintEventArgs e)
    {

    }

    private void run3wayValve7()
    {
      //switch3wayValveB();
      if (Valve3wayToRight)
      {
        rectangleShape20.BackgroundImage = global::LukMachine.Properties.Resources._112_RightArrowShort_Green_32x32_72;
        Valve3wayToRight = false;
        Properties.Settings.Default.Valve7State = false;
        //Valves.CloseValve7();  //valve 7 is the 3 way valve
        Valves.Valve7toLeft();
        System.Diagnostics.Debug.WriteLine("set to left chamber");

      }
      else
      {
        rectangleShape20.BackgroundImage = global::LukMachine.Properties.Resources._112_LeftArrowShort_Green_32x32_72;
        Valve3wayToRight = true;
        Properties.Settings.Default.Valve7State = true;
        //Valves.OpenValve7();
        Valves.Valve7toRight();
        System.Diagnostics.Debug.WriteLine("set to right chamber");
      }

    }

    private void rectangleShape8_Click(object sender, EventArgs e)
    {
      run3wayValve7();
    }

    private void rectangleShape22_Click(object sender, EventArgs e)
    {
      switch3wayValveB();
    }

    private void switch3wayValveB()
    {
      if (Valve3wayBToRight)
      {
        rectangleShape23.BackgroundImage = global::LukMachine.Properties.Resources._112_LeftArrowShort_Green_32x32_72;
        Valve3wayBToRight = false;
        Properties.Settings.Default.Valve8State = false;
        //Valves.CloseValve8();  //valve 8 is the 3 way valve
        Valves.Valve8toLeft();
        System.Diagnostics.Debug.WriteLine("set to left chamber");
      }
      else
      {
        rectangleShape23.BackgroundImage = global::LukMachine.Properties.Resources._112_UpLeftArrowShort_Green_32x32_72;
        Valve3wayBToRight = true;
        Properties.Settings.Default.Valve8State = true;
        //Valves.OpenValve8();
        Valves.Valve8toRight();
        System.Diagnostics.Debug.WriteLine("set to right chamber");
      }
    }

    private void rectangleShape15_Click(object sender, EventArgs e)
    {
      clickedMainPump();
    }
    private void clickedMainPump()
    {

      if (textBox6.Text == "0")
      {
        textBox6.Text = "100";
      }
      else
      {
        textBox6.Text = "0";
      }

    }
    private void rectangleShape14_Click(object sender, EventArgs e)
    {
      clickedPump1();
    }
    public void clickedPump1()
    {
      if (RefillPumpOn)
      {
        rectangleShape14.BackColor = Color.Gray;
        label21.BackColor = Color.Gray;
        label22.BackColor = Color.Gray;
        label22.Text = "Pump OFF";
        Pumps.StopPump1();
        RefillPumpOn = false;
      }
      else
      {
        rectangleShape14.BackColor = Color.Bisque;
        label21.BackColor = Color.Bisque;
        label22.BackColor = Color.Bisque;
        label22.Text = "Pump ON";
        Pumps.StartPump1();
        RefillPumpOn = true;
      }
    }

    private void button23_Click(object sender, EventArgs e)
    {

    }

    private void button27_Click(object sender, EventArgs e)
    {
      runPumpAtMax();
    }

    private void runPumpAtMax()
    {
      trackBar3.Value = trackBar3.Maximum;
      textBox6.Text = ((trackBar3.Value) * 100 / 4000).ToString();
      startPump2 = true;
    }



    private void button26_Click(object sender, EventArgs e)
    {
      stopMainPump();
    }

    private void stopMainPump()
    {
      trackBar3.Value = 0;
      textBox6.Text = ((trackBar3.Value) * 100 / 4000).ToString();
      stopPump2 = true;
    }

    private void trackBar3_Scroll(object sender, EventArgs e)
    {
      startPump2 = true;
      textBox6.Text = ((trackBar3.Value) * 100 / 4000).ToString();
      label17.Text = "Pump " + textBox6.Text + "%";
      if (trackBar3.Value == 0)
      {
        stopPump2 = true;
      }
    }

    private void button24_Click(object sender, EventArgs e)
    {
      int newvalue = Convert.ToInt32(Properties.Settings.Default.MediumPumpSetting) * 4000 / 100;
      //trackBar2.Value = newvalue;
      textBox6.Text = (newvalue * 100 / 4000).ToString();
      startPump2 = true;
    }

    private void textBox6_TextChanged(object sender, EventArgs e)
    {
      try
      {
        textBoxFlow.Text = (Convert.ToInt32(textBox6.Text) * 10).ToString();
        trackBar3.Value = Convert.ToInt32(textBox6.Text) * 4000 / 100;
        startPump2 = true;
        label17.Text = "Pump " + textBox6.Text + "%";
        if (trackBar3.Value == 0)
        {
          stopPump2 = true;
        }
      }
      catch
      {
        //MessageBox.Show("invalid");
      }
    }

    private void textBoxTemp_TextChanged(object sender, EventArgs e)
    {
      labelSetTemp.Text = "deg C (" + (Math.Round((Convert.ToDouble(textBoxTemp.Text) * 9 / 5 + 32))).ToString() + " F)";
    }

  

    private void button21_Click_1(object sender, EventArgs e)
    {
      PumpCollectedVolumeToReservoir();
    }


    public void PumpCollectedVolumeToReservoir()
    {

      startRefill = true;
      Valves.OpenValve1();
      Valves.CloseValve2();
      Valves.CloseValve3();
      //start refill pump 1
      Pumps.StartPump1();


      //stop refill pump 1
      //Pumps.StopPump1();
      //Valves.CloseValve1();
      // form.Hide();
      //refilldone = true;
    }




    private void checkBoxHeatSystem_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxHeatSystem.Checked == false)
      {
        textBoxTemp.Text = "20";
        textBoxTemp.Enabled = false;
        heat();

      }
      else
      {
        textBoxTemp.Enabled = true;
        if (textBoxTemp.Text == "")
        {
          //MessageBox.Show("Please enter a temperature");
          textBoxTemp.Text = "20";       
        }
        heat();
      }
    }

    double chamberTemp;
    double currentTemp;
    double athena1Temp;
    double athena2Temp;
    double targetTemp;

    private void heat()
    {
      try
      {
        //ReadAllTemperatures();
        //targetTemp = Properties.Settings.Default.selectedTemp; 


        //while ((currentTemp < targetTemp - 2) || (currentTemp > targetTemp + 2))
        // {
        //ReadAllTemperatures();
        //set heaters:
        targetTemp = Convert.ToDouble(textBoxTemp.Text);
        COMMS.Instance.SetAthenaTemp(1, (Math.Round(((targetTemp) * 9 / 5 + 32))));
        COMMS.Instance.SetAthenaTemp(2, (Math.Round(((targetTemp) * 9 / 5 + 32))));
        COMMS.Instance.SetAthenaTemp(3, (Math.Round(((targetTemp) * 9 / 5 + 32))));
        COMMS.Instance.SetAthenaTemp(4, (Math.Round(((targetTemp) * 9 / 5 + 32))));

        //Thread.Sleep(2000);//wait before checking again
        //Console.WriteLine("athena1Temp " + athena1Temp);
        //Console.WriteLine("chamberTemp " + chamberTemp);
        //}
      }
      catch (Exception ex)
      {
        MessageBox.Show("Problem reading temperature from Athena ");
      }
    }

    private void ReadAllTemperatures()
    {
      targetTemp = Convert.ToDouble(textBoxTemp.Text);
      athena1Temp = COMMS.Instance.ReadAthenaTemp(1);
      //read pipes temperature
      athena2Temp = COMMS.Instance.ReadAthenaTemp(2);
      //read selected chamber temperature
      if (Properties.Settings.Default.Chamber == "Ring")
      {
        chamberTemp = COMMS.Instance.ReadAthenaTemp(3);
      }
      else
      {
        chamberTemp = COMMS.Instance.ReadAthenaTemp(4);
      }
      //get average temp and display it
      currentTemp = (athena1Temp + athena2Temp + chamberTemp) / 3;
    }

    private void textBox6_KeyUp(object sender, KeyEventArgs e)
    {



    }


    private void buttonSetRate_Click(object sender, EventArgs e)
    {

    }

    private void textBox7_TextChanged(object sender, EventArgs e)
    {

    }

    private void button23_Click_1(object sender, EventArgs e)
    {
      try
      {
        targetPressure = Convert.ToInt32(textBox7.Text);
        Properties.Settings.Default.TargetPressure = targetPressure.ToString();
      }
      catch (Exception)
      {
        targetPressure = 0;
        Properties.Settings.Default.TargetPressure = "0";
        MessageBox.Show("Please enter an integer number for target pressure ");
      }
      /*
      if (targetPressure >= 0 && targetPressure < 33)
      {
        Valves.OpenValve4();
        Valves.CloseValve5();
        Valves.CloseValve6();
      }
      if (targetPressure >= 33 && targetPressure < 66)
      {
        Valves.CloseValve4();
        Valves.OpenValve5();
        Valves.CloseValve6();
      }
      if (targetPressure >= 66)
      {
        Valves.CloseValve4();
        Valves.CloseValve5();
        Valves.OpenValve6();
      }*/


    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {

    }

    private void timer2_Tick(object sender, EventArgs e)
    {
      readPressureAndDisplayIt(); //this also sets the currentPressure.
      if (checkBoxTargetPressure.Checked)
      {     
        //MessageBox.Show("d");
        if (targetPressure > currentPressure)
        {
          Pumps.IncreaseMainPump(1);
        }
        else if (targetPressure < currentPressure)
        {
          Pumps.DecreaseMainPump(1);
        }
        else
        {

        }
        label17.Text = "Pump "+ Properties.Settings.Default.MainPumpStatePercent+"%";


      }
    }

    private void readPressureAndDisplayIt()
    {
      //read pressure gauge 2
      counts = COMMS.Instance.ReadPressureGauge(2);
      realCounts = Convert.ToDouble(counts);
      currentPressure = (realCounts - ground) * Properties.Settings.Default.p1Max / twoVolt;  //ground is 2000 
      //p1Max is 100 //twoVolt is 60000
      outputPressure = currentPressure * pConversion;
      p1Psi = outputPressure;
      rawP1 = realCounts;
      p2Psi = p1Psi;
      labelP2.Text = p2Psi.ToString("#0.0") +" PSI";

      //read pressure gauge 1, convert to PSI (will need to * by conversion factor and set units label later)
      counts = COMMS.Instance.ReadPressureGauge(1);
      realCounts = Convert.ToDouble(counts);
      currentPressure = (realCounts - ground) * Properties.Settings.Default.p1Max / twoVolt;  //ground is 2000 //p1Max is 100  //twoVolt is 60000
      outputPressure = currentPressure * pConversion;
      p1Psi = outputPressure;
      rawP1 = realCounts;
      /*
      double rawP1 = 0; // Convert.ToDouble(COMMS.Instance.ReadPressureGauge(1));
      double p1Psi = (rawP1 - ground) * Properties.Settings.Default.p1Max / twoVolt;*/
      // aGauge5.Value = (float)p1Psi;
      aGauge1.Value = (float)p1Psi;
      if (p1Psi < 0)
      {
        p1Psi = 0;
      }
      //label1.Text = p1Psi.ToString("#0.000") + " PSI | " + rawP1.ToString() + " cts";
      //label30.Text = p1Psi.ToString("#0.000") + " PSI | " + rawP1.ToString() + " cts";
      groupBox13.Text = "Current Pressure: " + p1Psi.ToString("#0.000") + " PSI (" + rawP1.ToString() + " cts)";
      labelP1.Text = p1Psi.ToString("#0.0") + " PSI";
      labelPressureDifference.Text= "Pressure Difference (P1-P2) = "+ (p1Psi- p2Psi).ToString("#0.0") + " PSI";

    }

    private void Manual_FormClosed(object sender, FormClosedEventArgs e)
    {
      //Close all pumps
      Pumps.StopPump1();
      Pumps.SetPump2(0);
    }

    private void label21_Click(object sender, EventArgs e)
    {
      clickedPump1();
    }

    private void label22_Click(object sender, EventArgs e)
    {
      clickedPump1();
    }

    private void tabPage1_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      Pen pen = new Pen(Color.Gray, 7);
      pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
      pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
      e.Graphics.DrawLine(pen, 590, 550, 680, 550);
      e.Graphics.DrawLine(pen, 620, 260, 725, 285);
      pen = new Pen(Color.Black, 9);
      pen.StartCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
      pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
      e.Graphics.DrawLine(pen, 400, 600, 540, 600);
    }

    private void rectangleShape12_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape12.BorderWidth = 4;
    }

    private void rectangleShape12_MouseLeave(object sender, EventArgs e)
    {
      rectangleShape12.BorderWidth = 3;
    }

    private void rectangleShape6_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape6.BorderWidth = 4;
    }

    private void rectangleShape6_MouseLeave(object sender, EventArgs e)
    {
      rectangleShape6.BorderWidth = 3;
    }

    private void rectangleShape11_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape11.BorderWidth = 4;
      //Refresh();
      //rectangleShape11.Refresh();
    }

    private void rectangleShape11_MouseLeave(object sender, EventArgs e)
    {
      rectangleShape11.BorderWidth = 3;
      //Refresh();
      //rectangleShape11.Refresh();
    }

    private void rectangleShape8_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape8.BorderWidth = 4;
    }

    private void rectangleShape8_MouseLeave(object sender, EventArgs e)
    {
      rectangleShape8.BorderWidth = 3;
    }

    private void rectangleShape2_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape2.BorderWidth = 4;
    }

    private void rectangleShape2_MouseLeave(object sender, EventArgs e)
    {
      rectangleShape2.BorderWidth = 3;
    }

    private void rectangleShape4_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape4.BorderWidth = 4;
    }

    private void rectangleShape4_MouseLeave(object sender, EventArgs e)
    {
      rectangleShape4.BorderWidth = 3;
    }

    private void rectangleShape5_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape5.BorderWidth = 4;
    }

    private void rectangleShape5_MouseLeave(object sender, EventArgs e)
    {
      rectangleShape5.BorderWidth = 3;
    }

    private void rectangleShape20_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape8.BorderWidth = 4;
    }

    private void rectangleShape14_MouseEnter(object sender, EventArgs e)
    {
      rectangleShape8.BorderWidth = 4;
    }

    private void button25_Click(object sender, EventArgs e)
    {
      Valves.OpenValve6();
    }

    private void button28_Click(object sender, EventArgs e)
    {
      Valves.CloseValve6();
    }

    private void trackBar5_Scroll(object sender, EventArgs e)
    {
      textBox8.Text = ((trackBar5.Value) * 100 / 4000).ToString();
    }

    private void textBox8_TextChanged(object sender, EventArgs e)
    {
      try
      {
        trackBar5.Value = Convert.ToInt32(textBox8.Text) * 4000 / 100;
        COMMS.Instance.SetRegulator(2, trackBar5.Value);
      }
      catch
      {
        MessageBox.Show("invalid");
      }
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void groupBox14_Enter(object sender, EventArgs e)
    {

    }

    private void label23_Click(object sender, EventArgs e)
    {
      clickedMainPump();
    }

    private void label17_Click(object sender, EventArgs e)
    {
      clickedMainPump();
    }

    private void button29_Click(object sender, EventArgs e)
    {
      Valves.OpenValve8();
    }

    private void button30_Click(object sender, EventArgs e)
    {
      Valves.CloseValve8();
    }

    private void button32_Click(object sender, EventArgs e)
    {
      Valves.OpenValve7();
    }

    private void button31_Click(object sender, EventArgs e)
    {
      Valves.CloseValve7();
    }

    private void checkBoxRightChamber_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxRightChamber.Checked && !firstSetOfChamberCheckbox)
      {
        checkBoxLeftChamber.Checked = false;
        run3wayValve7();
        switch3wayValveB();
        Properties.Settings.Default.checkbLeftChecked = false;
        firstSetOfChamberCheckbox = false;
      }
      else
      {
        //checkBoxLeftChamber.Checked = true;
      }
    }

    private void checkBoxLeftChamber_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxLeftChamber.Checked && !firstSetOfChamberCheckbox)
      {
        checkBoxRightChamber.Checked = false;
        run3wayValve7();
        switch3wayValveB();
        Properties.Settings.Default.checkbLeftChecked = true;
        firstSetOfChamberCheckbox = false;


      }
      else
      {
        //checkBoxRightChamber.Checked = true;
      }
    }

    private void rectangleShape7_Click(object sender, EventArgs e)
    {

    }

    private void textBoxFlow_TextChanged(object sender, EventArgs e)
    {
      textBox6.Text= (Convert.ToInt32(textBoxFlow.Text) / 10).ToString();
    }

    private void checkBoxShowArrows_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxShowArrows.Checked)
      {
        rectangleShape23.Visible = true;
        rectangleShape20.Visible = true;
      }
      else
      {
        rectangleShape23.Visible = false;
        rectangleShape20.Visible = false;
      }
    }

    private void button22_Click(object sender, EventArgs e)
    {
      COMMS.Instance.MoveValve(7, "O");
      System.Threading.Thread.Sleep(1000);
      COMMS.Instance.MoveValve(7, "C");
    }

    private void button33_Click(object sender, EventArgs e)
    {
      COMMS.Instance.MoveValve(8, "C");
      System.Threading.Thread.Sleep(1000);
      COMMS.Instance.MoveValve(8, "O");
    }

    private void button34_Click(object sender, EventArgs e)
    {
      if (rectangleShape11.BackColor == Color.Green)
      {
        Valves.CloseValve2();
      }
      else
      {
        Valves.OpenValve2();
      }
      changeColor(sender);
    }

    private void label25_Click(object sender, EventArgs e)
    {

    }

    private void buttonReset_Click(object sender, EventArgs e)
    {
      startTime = Convert.ToDouble(Environment.TickCount);
      labelTime.Text = "Time (s): 0";
    }
  }
}
