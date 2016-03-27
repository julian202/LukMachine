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
  public partial class AutoScrn : Form
  {
    int reservoirPercent;
    int collectedPercent = -1;
    int collectedVolume;
    bool skip = false;
    double targetPressure = 0;
    double targetTemperature;
    double targetTemp;
    bool lastStep = false;
    int stepCount;
    bool pressureHasBeenReached = false;
    bool temperatureHasBeenReached = false;
    double pressureCounts;
    double currentPressure;
    double lastPressure;
    private int twoVolt = COMMS.Instance.get2v();
    private int ground = COMMS.Instance.getGround();
    string pumpstate;

    public AutoScrn()
    {
      InitializeComponent();
      backgroundWorkerMainLoop.WorkerReportsProgress = true;
      backgroundWorkerMainLoop.WorkerSupportsCancellation = true;
      backgroundWorkerReadAndDisplay.WorkerReportsProgress = true;
      backgroundWorkerReadAndDisplay.WorkerSupportsCancellation = true;
    }

    private void AutoScrn_Load(object sender, EventArgs e)
    {
      if (backgroundWorkerMainLoop.IsBusy != true)
      {
        backgroundWorkerMainLoop.RunWorkerAsync();
      }
      if (backgroundWorkerReadAndDisplay.IsBusy != true)
      {
        backgroundWorkerReadAndDisplay.RunWorkerAsync();
      }
    }

    //-----------------------------------------------------------------------------------------------
    //-------------Main Loop----------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------

    private void backgroundWorkerMainLoop_DoWork(object sender, DoWorkEventArgs e)
    {
      emptyCollectedVolume();
      fillReservoir();
      while (!lastStep)
      {
        stepCount = 0;
        targetTemperature = Convert.ToDouble(Properties.Settings.Default.CollectionPressure[stepCount]);

        goToTargetPressure();

        lastStep = true;
      }
    }

    private void backgroundWorkerMainLoop_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (e.UserState is string)
      {
        string mystring = e.UserState as string;
        if (mystring.Substring(mystring.Length - 2, 2) == "()") // if it is a function then run the funtion
        {
          /*
          if (mystring == "displayVolumeLevels()")
          {
            displayVolumeLevels();
          }*/
          if (mystring == "displaySkipButton()")
          {
            displaySkipButton();
          }
          if (mystring == "hideSkipButton()")
          {
            hideSkipButton();
          }



        }
        else //if it is not a function then just add the string to the listbox 1
        {
          listBox1.Items.Add(mystring);
          listBox1.TopIndex = listBox1.Items.Count - 1;
          labelPanel.Text = mystring;
        }
      }
    }
    private void goToTargetPressure()
    {
      backgroundWorkerMainLoop.ReportProgress(0, "Setting pressure to target pressure. Please wait...");
      targetPressure = Convert.ToDouble(Properties.Settings.Default.CollectionPressure[stepCount]);
      pressureHasBeenReached = false;
      skip = false;
      backgroundWorkerMainLoop.ReportProgress(0, "displaySkipButton()");
      while (!pressureHasBeenReached && !skip)
      {
        Thread.Sleep(100);//waits until pressure has been set.                 
      }
      //repeat
      Thread.Sleep(700);
      pressureHasBeenReached = false;
      while (!pressureHasBeenReached && !skip)
      {
        Thread.Sleep(100);//waits until pressure has been set.                 
      }
      backgroundWorkerMainLoop.ReportProgress(0, "Target pressure reached");
      backgroundWorkerMainLoop.ReportProgress(0, "hideSkipButton()");

    }
    private void emptyCollectedVolume() //refill reservoir with the liquid from the collected volume
    {
      backgroundWorkerMainLoop.ReportProgress(0, "Checking reservoir levels...");

      //MessageBox.Show("ReservoirPercent "+ ReservoirPercent+ " CollectedPercent "+ CollectedPercent);
      //progress("display volume levels =" + reservoirPercent.ToString() + "=" + collectedPercent.ToString());//fix this, dont read twice     

      //displayVolumeLevels();
      //backgroundWorkerMainLoop.ReportProgress(0, "displayVolumeLevels()");
      int maxPercentFull = Properties.Settings.Default.maxEmptyCollectedPercentFull;
      while (collectedPercent == -1) //-1 is the default value of collectedPercent.
      {
        Thread.Sleep(100);//waits until collectedPercent has been set.
      }
      if (collectedPercent > maxPercentFull)
      {
        MessageBox.Show("The collected penetrometer contains liquid. The collected volume will now be flushed.");
        backgroundWorkerMainLoop.ReportProgress(0, "Emptying collected volume...");

        Valves.OpenValve1();
      }
      skip = false;
      backgroundWorkerMainLoop.ReportProgress(0, "displaySkipButton()");
      while ((collectedPercent > maxPercentFull) && !skip)
      {
        Thread.Sleep(300);
        //collectedPercent = COMMS.Instance.getCollectedLevelPercent();
        //backgroundWorkerMainLoop.ReportProgress(0, "displayVolumeLevels()");
        //backgroundWorkerMainLoop.ReportProgress(0,"display volume levels =" + reservoirPercent.ToString() + "=" + collectedPercent.ToString());//fix this, dont read twice
      }
      backgroundWorkerMainLoop.ReportProgress(0, "hideSkipButton()");
    }
    private void displaySkipButton()
    {
      buttonSkip.Visible = true;
    }
    private void hideSkipButton()
    {
      buttonSkip.Visible = false;
    }

    public void displayVolumeLevels()
    {
      groupBoxReservoir.Text = "Reservoir " + (Convert.ToInt32(reservoirPercent) * Convert.ToInt32(Properties.Settings.Default.MaxCapacityInML) / 100).ToString() + " mL";
      collectedVolume = Convert.ToInt32(collectedPercent) * Convert.ToInt32(Properties.Settings.Default.MaxCapacityInML) / 100;
      //mlCollected.Text = (collectedVolume).ToString() + " mL";
      groupBoxCollectedVolume.Text = "Collected Volume " + collectedVolume.ToString() + " mL";
      //MessageBox.Show("ReservoirPercent = " + ReservoirPercent + " CollectedPercent = " + CollectedPercent);
      try
      {
        verticalProgressBar1.Value = Convert.ToInt32(reservoirPercent);
      }
      catch (Exception)
      {

        if (Convert.ToInt32(reservoirPercent) >= 100)
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
        verticalProgressBar2.Value = Convert.ToInt32(collectedPercent);
      }
      catch (Exception)
      {
        if (Convert.ToInt32(collectedPercent) >= 100)
        {
          verticalProgressBar2.Value = 100;
        }
        else
        {
          verticalProgressBar2.Value = 0;
        }
      }
    }

    public void fillReservoir() //refill reservoir with the liquid from the collected volume
    {
      if (reservoirPercent < 80)
      {
        backgroundWorkerMainLoop.ReportProgress(0, "Reservoir is not full. Please add more volume to reservoir.");
        skip = false;
        backgroundWorkerMainLoop.ReportProgress(0, "displaySkipButton()");
        while ((reservoirPercent < 80) && !skip)
        {
          Thread.Sleep(300);
          //reservoirPercent = COMMS.Instance.getReservoirLevelPercent();
          //backgroundWorkerMainLoop.ReportProgress(0, "displayVolumeLevels()");
        }
        backgroundWorkerMainLoop.ReportProgress(0, "hideSkipButton()");
      }
      backgroundWorkerMainLoop.ReportProgress(0, "Done checking reservoir levels");
    }

    private void buttonReport_Click(object sender, EventArgs e) //this should be called automatically at end of test
    {
      this.Hide();
      Report rep = new Report(true);
      rep.ShowDialog();
      this.Show();
    }

    private void linkLabelOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("explorer.exe", System.IO.Directory.GetParent(Properties.Settings.Default.TestData).ToString());

    }

    private void buttonSkipSettingTemp_Click(object sender, EventArgs e)
    {

    }

    private void buttonSkipPressure_Click(object sender, EventArgs e)
    {

    }

    private void buttonSkip_Click(object sender, EventArgs e)
    {
      skip = true;
      buttonSkip.Visible = false;
    }

    private void backgroundWorkerReadAndDisplay_DoWork(object sender, DoWorkEventArgs e)
    {
      while (true)
      {
        //read reservoirs
        collectedPercent = COMMS.Instance.getCollectedLevelPercent();
        reservoirPercent = COMMS.Instance.getReservoirLevelPercent();

        //save current pressure (this will be used in pressure adjust loop)
        lastPressure = currentPressure;

        //read pressure gauge, convert to PSI (will need to * by conversion factor and set units label later)
        pressureCounts = Convert.ToDouble(COMMS.Instance.ReadPressureGauge(1));
        currentPressure = (pressureCounts - ground) * Properties.Settings.Default.p1Max / 60000;  //twoVolt is 60000

        //adjust pump for target pressure
        if ((currentPressure < targetPressure - 0.2) && (currentPressure < lastPressure + 0.1)) //dont't increase speed if pressure is already increasing
        {
          Pumps.IncreaseMainPump(0.1);
        }
        else if ((currentPressure < targetPressure - 10) && (currentPressure < lastPressure + 0.3)) //dont't increase speed if pressure is already increasing fast
        {
          Pumps.IncreaseMainPump(0.3);
        }
        else if (currentPressure > targetPressure)
        {
          Pumps.SetPump2(0);
        }
        if ((currentPressure > (targetPressure - 0.1)) && (currentPressure < targetPressure + 0.1))
        {
          pressureHasBeenReached = true;
        }

        //update UI
        backgroundWorkerReadAndDisplay.ReportProgress(0);

        Thread.Sleep(500);

        //cancel 
        if ((backgroundWorkerReadAndDisplay.CancellationPending == true))
        {
          e.Cancel = true;
          break;
        }
      }
    }

    private void backgroundWorkerReadAndDisplay_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      displayVolumeLevels();
      labelPressure.Text = "Pressure  =  " + String.Format("{0:0.0} PSI", currentPressure);
      pumpstate = Properties.Settings.Default.MainPumpStatePercent.ToString();
      labelPumpState.Text = "Pump Power  =  " + pumpstate + "%";
    }
  }
}
