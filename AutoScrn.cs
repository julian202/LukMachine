﻿using System;
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
  public partial class AutoScrn : Form
  {
    public delegate void ProgressEventHandler(string message);
    BurstTest test;
    private Thread testThread;
    private double currentTemp;
    private double targetTemp;
    private double athena1Temp; //temp of tank
    private double athena2Temp; //temp of pipes
    private double chamberTemp;
    int collectedVolume;
    public AutoScrn()
    {
      InitializeComponent();
    }
    void Run_Test_Progress(string message)
    {
      //ghetto way to send messages back from the auto test thread.
      if (listBox1.InvokeRequired)
      {
        // Running on a different thread than the one created the control
        Delegate d = new ProgressEventHandler(Run_Test_Progress);
        listBox1.Invoke(d, message);
      }
      else
      {
        // Running on the same thread which created the control

        // CONTROLS FOR DEVICES IN PMI MACHINES
        #region Reading Messages
        if (message.Contains("Reading:"))
        {
          // split the reading string into usable doubles
          // for the graph display
          string[] splitString = message.Split(':');
          string[] splitString2 = splitString[1].Split(',');
          string X = splitString2[1];
          string Y = splitString2[0];
          chart1.Series["Series1"].Points.AddXY(Y, X);
          labelTotalTime.Text = "Total time  =  " + (Convert.ToDouble(Y) / 60).ToString("0.00") + " mins";

          /*try
          {
            if (Convert.ToInt32(Convert.ToDouble(Y)) > 0)
            {
              verticalProgressBar2.Value = Convert.ToInt32(Convert.ToDouble(Y));
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine("JP " + ex.Message);
            //throw;
          }*/

          dataGridView1.Rows.Add(Y.ToString(), X.ToString());
          //dataGridView1.TopIndex = listBox1.Items.Count - 1;
          //now scroll to the last row:
          dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
          System.Diagnostics.Debug.WriteLine("X: " + X.ToString() + " Y: " + Y.ToString());
        }
        else if (message.Contains("display current step time"))
        {
          string[] msgSplit = message.Split('=');
          string currentDuration = msgSplit[1];
          labelDuration.Text = "Step time  =  " + currentDuration + " mins";
        }
        else if (message.Contains("set label7 to targetTemp"))
        {

          label4.Text = "Please wait...  Setting Temperature";
          panel1.Visible = true;
          buttonSkipSettingTemp.Visible = true;


          //targetTemp = Properties.Settings.Default.selectedTemp;
          /*
          string[] msgSplit = message.Split('=');
          double targetTemp = Convert.ToDouble(msgSplit[1]);
          int targetTempInCelsius = (Convert.ToInt32((targetTemp - 32) * 5 / 9));
          label8.Text = "Temperature  = " + targetTemp.ToString() + " F / " + String.Format("{0:0}", targetTempInCelsius) + " C ";
          */
        }
        else if (message.Contains("panel label setting pressure"))
        {
          panel1.Visible = true;
          label4.Text = "Please wait...  Setting Pressure (adjusting pump power)";
          buttonSkipSettingTemp.Visible = false;
          buttonSkipPressure.Visible = true;
        }
        else if (message.Contains("hide panel 1"))
        {
          panel1.Visible = false;
        }
        else if (message.Contains("Reservoir is not full"))
        {
          label4.Text = "Reservoir is not full. Please add more volume to reservoir.";
        }
        
          

        else if (message.Contains("set label5 to currentTemp"))
        {
          string[] msgSplit = message.Split('=');
          int myint = Convert.ToInt32(Convert.ToDouble(msgSplit[1]));
          //MessageBox.Show(myint.ToString());  
          int myintInCelsius = (Convert.ToInt32((myint - 32) * 5 / 9));
          label6.Text = "Temperature  =  " + String.Format("{0:0}", myintInCelsius) + " C / " + String.Format("{0:0}", myint) + " F" ;
          //MessageBox.Show(label5.Text);
        }
        else if (message.Contains("disable stop button"))
        {
          button2.Enabled = false;
        }  
        else if (message.Contains("display pressure"))
        {
          string[] msgSplit = message.Split('=');
          string pressure = msgSplit[1];
          labelPressure.Text = "Diff Pressure  =  " + String.Format("{0:0.0} PSI", Convert.ToDouble(pressure));
          string pumpstate = Properties.Settings.Default.MainPumpStatePercent.ToString();
          labelPumpState.Text = "Pump Power  =  " + pumpstate + "%";
        }
        else if (message.Contains("display target pressure"))
        {
          //string[] msgSplit = message.Split('=');
          //string targetpressure = msgSplit[1];
          //labelTargetPressure.Text = "Pressure    =  " + String.Format("{0:0.0} PSI", Convert.ToDouble(targetpressure));
          /*
          string pressures;
          pressures = Properties.Settings.Default.CollectionPressure[0];
          for (int i = 1; i < Properties.Settings.Default.CollectionPressure.Count; i++)
          {
            pressures = pressures + ", " + Properties.Settings.Default.CollectionPressure[i];
          }
          labelTargetPressure.Text = "Pressure  =  " + pressures;*/
        }
        else if (message.Contains("display duration"))
        {
          //string[] msgSplit = message.Split('=');
          //string duration = msgSplit[1];

          //labelDuration.Text = "Duration  =  " + String.Format("{0:0.0} mins", Convert.ToDouble(duration));
        }
        else if (message.Contains("hide panel1"))
        {
          panel1.Visible = false;
          buttonSkipPressure.Visible = false;
        }
        else if (message.Contains("disable stop button"))
        {
          button2.Enabled = false;
        }
        else if (message.Contains("display volume levels"))
        {
          //read penetrometers
          string[] msgSplit = message.Split('=');
          //MessageBox.Show("message = "+ message);
          string ReservoirPercent = msgSplit[1];
          string CollectedPercent = msgSplit[2];
          //MessageBox.Show("ReservoirPercent = " + ReservoirPercent+ " CollectedPercent = " + CollectedPercent);
          //int ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
          //groupBoxReservoir.Text = "Reservoir " + ReservoirPercent.ToString() + "% Full";
          //int CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
          // groupBoxCollectedVolume.Text = "Collected Volume " + CollectedPercent.ToString() + "% Full";

          groupBoxReservoir.Text = "Reservoir " + (Convert.ToInt32(ReservoirPercent) * Convert.ToInt32(Properties.Settings.Default.MaxCapacityInML) / 100).ToString() + " mL";

          collectedVolume = Convert.ToInt32(CollectedPercent) * Convert.ToInt32(Properties.Settings.Default.MaxCapacityInML) / 100;
          //mlCollected.Text = (collectedVolume).ToString() + " mL";
          groupBoxCollectedVolume.Text = "Collected Volume " + collectedVolume.ToString() + " mL";
          //MessageBox.Show("ReservoirPercent = " + ReservoirPercent + " CollectedPercent = " + CollectedPercent);

          try
          {
            verticalProgressBar1.Value = Convert.ToInt32(ReservoirPercent);
          }
          catch (Exception)
          {

            if (Convert.ToInt32(ReservoirPercent) >= 100)
            {
              verticalProgressBar1.Value = 100;
            }
            else
            {
              verticalProgressBar1.Value = 0;
            }
          }
          try
          {
            verticalProgressBar2.Value = Convert.ToInt32(CollectedPercent);
          }
          catch (Exception)
          {
            if (Convert.ToInt32(ReservoirPercent) >= 100)
            {
              verticalProgressBar2.Value = 100;
            }
            else
            {
              verticalProgressBar2.Value = 0;
            }
          }


        }
        else if (message.Contains("display stepCount"))
        {
          string[] msgSplit = message.Split('=');
          labelStepCurrent.Text = "Step  =  " + msgSplit[1];
        }
        else if (message.Contains("display duration"))
        {
          string[] msgSplit = message.Split('=');
          labelDuration.Text = "Duration  =  " + msgSplit[1];
        }
        else if (message.Contains("end Test"))
        {
          endTest();
        }
        else if (message.Contains("B="))
        {
          //do some stuff, maybe open the repOrt with a capital NOW.
          string[] msgSplit = message.Split('=');
          string burst = msgSplit[1];
          listBox1.Items.Add("Test Complete! Burst Pressure was: " + burst);
          listBox1.TopIndex = listBox1.Items.Count - 1;

          //MessageBox.Show("Test Complete!Burst Pressure was: " + burst, "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Stop);
          BurstDialog2 asdf = new BurstDialog2(Convert.ToDouble(burst));
          asdf.ShowDialog();
          this.Close();
        }
        else if (message.Equals("Test Aborted"))
        {
          MessageBox.Show("Test Aborted!", "Burst Tester", MessageBoxButtons.OK, MessageBoxIcon.Stop);
          if (Properties.Settings.Default.mustRunReport)
          {
            Properties.Settings.Default.mustRunReport = false;
            Properties.Settings.Default.Save();
          }
          this.Close();
        }
        else
        {
          // update status box
          listBox1.Items.Add(message);
          listBox1.TopIndex = listBox1.Items.Count - 1;
        }
        #endregion
      }
    }

    private void AutoScrn_Load(object sender, EventArgs e)
    {

      COMMS.calculateVoltageReferences();   

      if (Properties.Settings.Default.Chamber == "Ring")
      {
        labelChamber.Text = "Chamber = Ring";
      }
      else
      {
        labelChamber.Text = "Chamber = Disk";
      }
      buttonSkipSettingTemp.Visible = false;
      buttonSkipPressure.Visible = false;
      string pressures;
      pressures = Properties.Settings.Default.CollectionPressure[0];
      for (int i = 1; i < Properties.Settings.Default.CollectionPressure.Count; i++)
      {
        pressures = pressures + ", " + Properties.Settings.Default.CollectionPressure[i];
      }
      labelTargetPressure.Text = "Pressure  =  " + pressures + " (PSI)";
      labelStepsTotal.Text = "Total Steps  =  " + Properties.Settings.Default.StepCount.ToString();
      labelStepCurrent.Text = "Step  =  1";

      string durations;
      durations = Properties.Settings.Default.CollectionDuration[0];
      for (int i = 1; i < Properties.Settings.Default.CollectionDuration.Count; i++)
      {
        durations = durations + ", " + Properties.Settings.Default.CollectionDuration[i];
      }
      labelDurations.Text = "Step duration  =  " + durations + " (mins)";

      string temperature;
      temperature = Properties.Settings.Default.CollectionTemperature[0];
      for (int i = 1; i < Properties.Settings.Default.CollectionTemperature.Count; i++)
      {
        temperature = temperature + ", " + Properties.Settings.Default.CollectionTemperature[i];
      }
      label8.Text = "Temperature  =  " + temperature +" (C)";


      button4.Visible = false;
      //button5.Visible = false;
      Text = "Auto test [" + Properties.Settings.Default.TestSampleID + "]";
      if (Properties.Settings.Default.promptSafety)
      {
        MessageBox.Show("Please verify that your sample is centered and that the clamping mechanism is safely securing the sample.");
      }
      chart1.ChartAreas[0].AxisY.Title = "Flow (mL/min)";
      chart1.ChartAreas[0].AxisX.Title = "Time (seconds)";
      chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
      chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
      startTestButton();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      startTestButton();
    }

    private void startTestButton()
    {
      if (button1.Text == "Start Test")
      {
        test = new BurstTest();
        test.Progress += Run_Test_Progress;
        testThread = new Thread(test.RunBurstTest);
        testThread.Start();
        //button1.Image = Properties.Resources._1427928255_Pause;
        //button1.Text = "Pause Test";
        button2.Enabled = true;
        button1.Enabled = false;
        //button2.Enabled = false;
        return;
      }
      if (button1.Text == "Pause Test")
      {
        //testThread.Suspend();
        test.testPaused = true;

        button1.Text = "Resume Test";
        button1.Image = Properties.Resources._1427928241_Play;
        button2.Enabled = true;
        button3.Enabled = true;
        return;
      }
      if (button1.Text == "Resume Test")
      {
        //testThread.Resume();
        //test.abort = true;
        test.testPaused = false;
        button1.Text = "Pause Test";
        button1.Image = Properties.Resources._1427928255_Pause;
        button2.Enabled = false;
        button3.Enabled = false;
        return;
      }



    }

    private void button2_Click(object sender, EventArgs e)
    {
      endTest();
    }

    private void endTest()
    {
      button2.Enabled = false;
      test.AbortTest();
      //testThread.Abort();

      //this sleep added to try to fix program hanging on next line (.Join):
      Thread.Sleep(2000);
      listBox1.Items.Add("Data saved to " + Properties.Settings.Default.TestData);
      //listBox1.Items.Add("Test ended successfully");
      button4.Visible = true;
      //button5.Visible = true;
      listBox1.TopIndex = listBox1.Items.Count - 1;
      //System.Windows.Forms.MessageBox.Show("Data saved to " + Properties.Settings.Default.TestData);
      //DataSavedForm Form = new DataSavedForm();
      //Form.Show();

      testThread.Abort();
      testThread.Join();

      //stop main pump
      //Progress("Stopping pump...");
      //COMMS.Instance.ZeroRegulator(1);
      Pumps.SetPump2(0);
      //pumpRunning = false;

      //open relief pressure valve
      //Progress("Relieving pressure...");
      //Valves.OpenValve2();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      Manual manScrn = new Manual();
      this.Hide();
      manScrn.ShowDialog();
      this.Show();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      this.Hide();
      Report rep = new Report(true);
      rep.ShowDialog();
      this.Show();
    }

    private void button5_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("explorer.exe", System.IO.Directory.GetParent(Properties.Settings.Default.TestData).ToString());
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("explorer.exe", System.IO.Directory.GetParent(Properties.Settings.Default.TestData).ToString());
    }

    private void buttonSkipSettingTemp_Click(object sender, EventArgs e)
    {
      COMMS.skipTemp = true;
    }

    private void buttonSkipPressure_Click(object sender, EventArgs e)
    {
      COMMS.skipPressure = true;
    }
  }
}
