using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
    int collectedPercent = -100;
    int collectedVolume;
    bool skip = false;
    double targetPressure = 0;
    double targetTemperature = 0;
    private double reservoirTemp;
    double targetTemperatureInF;
    double currentTemperature;
    double currentTemperatureInC;
    int stepCount;
    bool pressureHasBeenReached = false;
    bool temperatureHasBeenReached = false;
    double pressureCounts;
    double currentPressure;
    double lastPressure;
    private int twoVolt = COMMS.Instance.get2v();
    private int ground = COMMS.Instance.getGround();
    string pumpstate;
    bool stepTimeReached = false;
    Stopwatch stopwatch = new Stopwatch();
    Stopwatch stopwatchStep = new Stopwatch();
    TimeSpan timeSpanTotal;
    TimeSpan timeSpanStep;
    double stepTimeInMinutes;
    bool testFinished = false;
    string totalTime;
    double flow;
    double lastFlow = 0;
    int collectedCount;
    int lastCollectedCount;
    int collectedDifferenceInCounts;
    bool firstDataPoint;
    double timeDifferenceInMinutes;
    double totalTimeInMinutes;
    double lastTotalTimeInMinutes;
    StreamWriter SR;
    StreamWriter SR2;
    private string dataFile = Properties.Settings.Default.TestData;
    private string dataFilePathForCapRep;
    double convertedTemp = 0;
    private string sampleID = Properties.Settings.Default.TestSampleID;
    private string lotNumber = Properties.Settings.Default.TestLotNumber;
    bool abort = false;
    bool nowSettingTemp = false;
    private double chamberTemp;
    double pumpPowerAtEndOfLastStep=0;
    bool emptyingCollected = false;
    bool dontCountFirstDataPointAfterEmtpying = false;
    bool goingToTargetTemperature = false;
    bool goingToTargetPressure = false;

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
      goingToTargetTemperature = false;
      goingToTargetPressure = false;
      for (int i = 0; i < Properties.Settings.Default.CollectionPressure.Count; i++)
      {
        dataGridView2.Rows.Add(Properties.Settings.Default.CollectionPressure[i], Properties.Settings.Default.CollectionDuration[i], Properties.Settings.Default.CollectionTemperature[i]);
      }

      try
      {
        SR = new StreamWriter(dataFile);
        dataFilePathForCapRep = dataFile.Substring(0, dataFile.Length - 4) + "-forCapRep.txt";
        SR2 = new StreamWriter(dataFilePathForCapRep);
        WriteHeader();
        WriteHeaderForCapRep();
        SR.Close();
        SR2.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message + " Restart the application and select a different folder to save the data, or else run the application as an administrator");
        Application.Exit();
      }
      firstDataPoint = true;
      buttonReport.Visible = false;
      if (backgroundWorkerMainLoop.IsBusy != true)
      {
        backgroundWorkerMainLoop.RunWorkerAsync();
      }
      if (backgroundWorkerReadAndDisplay.IsBusy != true)
      {
        backgroundWorkerReadAndDisplay.RunWorkerAsync();
      }
      timerForStopWatch.Start();
      chart1.ChartAreas[0].AxisY.Title = "Flow (mL/min)";
      chart1.ChartAreas[0].AxisX.Title = "Time (mins)";
      chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
      chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
    }

    //-----------------------------------------------------------------------------------------------
    //-------------Main Loop----------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------

    private void backgroundWorkerMainLoop_DoWork(object sender, DoWorkEventArgs e)
    {
      emptyCollectedVolume();
      fillReservoir();

      for (int i = 0; i < Properties.Settings.Default.CollectionPressure.Count; i++)
      {
        stepCount = i;
        targetPressure = Convert.ToDouble(Properties.Settings.Default.CollectionPressure[stepCount]);
        //--MessageBox.Show("targetPressure is " + targetPressure.ToString());
        goToTargetTemperature();
        goToTargetPressure();     
        startTimeWriteToFileAndGraph();
        if (abort)
        {
          break;
        }
      }
      backgroundWorkerMainLoop.ReportProgress(0, "finished()");
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

          if (mystring == "addDataPointToTableAndGraph()")
          {
            addDataPointToTableAndGraph();
          }
          else if (mystring == "displayPanel1()")
          {
            displayPanel1();
          }
          else if (mystring == "hidePanel1()")
          {
            hidePanel1();
          }
          else if (mystring == "displaySkipButton()")
          {
            displaySkipButton();
          }
          else if (mystring == "hideSkipButton()")
          {
            hideSkipButton();
          }
          else if (mystring == "finished()")
          {
            finished();
          }
        }
        else //if it is not a function then just add the string to the listbox 1
        {
          addToListBox1(mystring);
          labelPanel.Text = mystring;
        }
      }
    }

    private void addToListBox1(string param)
    {
      listBox1.Items.Add(param);
      listBox1.TopIndex = listBox1.Items.Count - 1;
    }

    private void addDataPointToTableAndGraph()
    {
      if (firstDataPoint)
      {
        flow = lastFlow;
        lastCollectedCount = collectedCount;
        totalTimeInMinutes = Convert.ToDouble(timeSpanTotal.Minutes) + Convert.ToDouble(timeSpanTotal.Seconds) / 60;
        lastTotalTimeInMinutes = totalTimeInMinutes;
        firstDataPoint = false;
      }
      else
      {      
        collectedDifferenceInCounts = collectedCount - lastCollectedCount;
        if (dontCountFirstDataPointAfterEmtpying)
        {
          collectedDifferenceInCounts = 0;
          dontCountFirstDataPointAfterEmtpying = false;
        }
        totalTimeInMinutes = Convert.ToDouble(timeSpanTotal.Minutes) + Convert.ToDouble(timeSpanTotal.Seconds) / 60;
        timeDifferenceInMinutes = lastTotalTimeInMinutes - totalTimeInMinutes;
        if (timeDifferenceInMinutes == 0)
        {
          flow = 0;
        }
        else
        {
          flow = Convert.ToDouble(collectedDifferenceInCounts / timeDifferenceInMinutes); //this is flow in pent counts per minute
          flow = flow * Convert.ToDouble(Properties.Settings.Default.MaxCapacityInML) / 58000; //this converts flow to mL       
          lastFlow = flow; //this is to keep count of last flow during pressure adjust
        }
        //Debug.WriteLine("flow: "+ flow+ " totalTimeInMinutes: "+ totalTimeInMinutes+ " lastTotalTimeInMinutes: " + lastTotalTimeInMinutes+" timeDifferenceInMinutes: " +timeDifferenceInMinutes.ToString() + " collectedDifferenceInCounts: " + collectedDifferenceInCounts.ToString());
      }
      dataGridView1.Rows.Add(totalTime, flow.ToString("#0.00"));
      dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
      chart1.Series["Series1"].Points.AddXY(totalTimeInMinutes.ToString("#0.00"), flow.ToString("#0.00"));
      chart1.Series["SeriesPressure"].Points.AddXY(totalTimeInMinutes.ToString("#0.00"), currentPressure.ToString("#0.00"));
      chart1.Series["SeriesTemperature"].Points.AddXY(totalTimeInMinutes.ToString("#0.00"), currentTemperatureInC.ToString("#0.00"));
      if (checkBoxShowPressureGraph.Checked)
      {
        chart1.Series["SeriesPressure"].Enabled = true;
      }
      else
      {
        chart1.Series["SeriesPressure"].Enabled = false;
      }
      if (checkBoxShowTemperatureGraph.Checked)
      {
        chart1.Series["SeriesTemperature"].Enabled = true;
      }
      else
      {
        chart1.Series["SeriesTemperature"].Enabled = false;
      }

      lastCollectedCount = collectedCount;
      lastTotalTimeInMinutes = totalTimeInMinutes;
      SR = new StreamWriter(dataFile, true);
      SR2 = new StreamWriter(dataFilePathForCapRep, true);
      SR.WriteLine("{0,10}\t{1,10}\t{2,10}\t{3,10}", totalTimeInMinutes.ToString("#0.00"), flow.ToString("#0.00"), currentTemperatureInC.ToString("0.0"), currentPressure.ToString("0.000"));
      //Console.WriteLine("{0,10}\t{1,10}\t{2,10}\t{3,10}", outputTime.ToString("0.00"), Flow.ToString("0.00"), convertedTemp.ToString("0.0"), currentPressure.ToString("0.000"));
      SR2.WriteLine("{0,10}\t{1,10}", flow.ToString("0.00"), currentPressure.ToString("0.000"));
      SR.Close();
      SR2.Close();
    }

    private void displayPanel1()
    {
      panel1.Visible = true;
    }

    private void hidePanel1()
    {
      panel1.Visible = false;
    }

    private void finished()
    {
      testFinished = true;
      buttonReport.Visible = true;
      addToListBox1("Finished");
      labelPanel.Text = "Finished";
      addToListBox1("Data saved to " + Properties.Settings.Default.TestData);     
      stopButton();
      showReport();
    }
    private void startTimeWriteToFileAndGraph()
    {
      backgroundWorkerMainLoop.ReportProgress(0, "hidePanel1()");
      backgroundWorkerMainLoop.ReportProgress(0, "Running step " + (stepCount + 1));
      stopwatch.Start();
      stopwatchStep.Restart();
      stepTimeReached = false;
      firstDataPoint = true;
      while (!stepTimeReached)
      {
        backgroundWorkerMainLoop.ReportProgress(0, "addDataPointToTableAndGraph()");

        if (collectedPercent>95)
        {
          backgroundWorkerMainLoop.ReportProgress(0, "Collected volume is full. Flushing... (Please make sure you add more volume to the reservoir!)");
          backgroundWorkerMainLoop.ReportProgress(0, "displayPanel1()");
          emptyingCollected = true;
          stopwatch.Stop();
          stopwatchStep.Stop();
        }
        while (emptyingCollected) //wait for collected volume to drain
        {
          Thread.Sleep(200);
        }
        if (!stopwatchStep.IsRunning)
        {
          dontCountFirstDataPointAfterEmtpying = true; 
          backgroundWorkerMainLoop.ReportProgress(0, "hidePanel1()");
          stopwatch.Start();
          stopwatchStep.Start();
        }
        Thread.Sleep(1000 * Properties.Settings.Default.intervalBetweenTimePoints);
      }
      stopwatch.Stop();
      stopwatchStep.Stop();

      //MessageBox.Show(((stopwatch.ElapsedMilliseconds)/1000).ToString());
      //time.f "Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed
    }

    private void goToTargetTemperature()
    {
      goingToTargetTemperature = true;
      if (Properties.Settings.Default.CollectionTemperature[stepCount] != "-")
      {
        targetTemperature = Convert.ToDouble(Properties.Settings.Default.CollectionTemperature[stepCount]);
        backgroundWorkerMainLoop.ReportProgress(0, "Setting temperature to target temperature. Please wait...");
        backgroundWorkerMainLoop.ReportProgress(0, "displayPanel1()");
        temperatureHasBeenReached = false;
        skip = false;
        backgroundWorkerMainLoop.ReportProgress(0, "displaySkipButton()");
        nowSettingTemp = true;
        while (!temperatureHasBeenReached && !skip)
        {
          Thread.Sleep(100);//waits until pressure has been set.                 
        }
        nowSettingTemp = false;
        Thread.Sleep(700);
        backgroundWorkerMainLoop.ReportProgress(0, "Target temperature reached");
        backgroundWorkerMainLoop.ReportProgress(0, "hideSkipButton()");
      }
      else
      {

      }
      goingToTargetTemperature = false;
    }

    private void goToTargetPressure()
    {
      goingToTargetPressure = true;
      backgroundWorkerMainLoop.ReportProgress(0, "Setting pressure to target pressure. Please wait...");
      backgroundWorkerMainLoop.ReportProgress(0, "displayPanel1()");
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
      pumpPowerAtEndOfLastStep = Properties.Settings.Default.MainPumpStatePercent;
      goingToTargetPressure = false;
    }
    private void emptyCollectedVolume() //refill reservoir with the liquid from the collected volume
    {
      backgroundWorkerMainLoop.ReportProgress(0, "Checking reservoir levels...");

      //MessageBox.Show("ReservoirPercent "+ ReservoirPercent+ " CollectedPercent "+ CollectedPercent);
      //progress("display volume levels =" + reservoirPercent.ToString() + "=" + collectedPercent.ToString());//fix this, dont read twice     

      //displayVolumeLevels();
      //backgroundWorkerMainLoop.ReportProgress(0, "displayVolumeLevels()");
      int maxPercentFull = Properties.Settings.Default.maxEmptyCollectedPercentFull;
      while (collectedPercent == -100) //-1 is the default value of collectedPercent.
      {
        Thread.Sleep(100);//waits until collectedPercent has been set.
      }
      if (collectedPercent > maxPercentFull)
      {
        //MessageBox.Show("collectedPercent=" + collectedPercent + " maxPercentFull=" + maxPercentFull);
        MessageBox.Show("The collected penetrometer contains liquid. The collected volume will now be flushed.");
        backgroundWorkerMainLoop.ReportProgress(0, "Emptying collected volume...");

        Valves.OpenValve1();
      }
      skip = false;
      backgroundWorkerMainLoop.ReportProgress(0, "displaySkipButton()");
      while ((collectedPercent > maxPercentFull) && !skip)
      {
        //MessageBox.Show("collectedPercent=" + collectedPercent + " maxPercentFull=" + maxPercentFull);

        Thread.Sleep(300);
        //collectedPercent = COMMS.Instance.getCollectedLevelPercent();
        //backgroundWorkerMainLoop.ReportProgress(0, "displayVolumeLevels()");
        //backgroundWorkerMainLoop.ReportProgress(0,"display volume levels =" + reservoirPercent.ToString() + "=" + collectedPercent.ToString());//fix this, dont read twice
      }
      Valves.CloseValve1();
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
      showReport();
    }

    private void showReport()
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
        collectedCount = COMMS.CollectedLevelCount; //this is to have a more accurate (double intead of int) number to calcultate flow. (notice it's not a funct).

        if (emptyingCollected)
        {
          Valves.OpenValve1();
          if (collectedPercent< Properties.Settings.Default.maxEmptyCollectedPercentFull)
          {
            Valves.CloseValve1();
            emptyingCollected = false;
          }
        }

        //save current pressure (this will be used in pressure adjust loop)
        lastPressure = currentPressure;

        //read pressure gauge, convert to PSI (will need to * by conversion factor and set units label later)
        pressureCounts = Convert.ToDouble(COMMS.Instance.ReadPressureGauge(1));
        currentPressure = (pressureCounts - ground) * Properties.Settings.Default.p1Max / 60000;  //twoVolt is 60000


        if ((goingToTargetTemperature)||(goingToTargetPressure))
        {
          Pumps.SetPump2(0);
        }
        else
        {
          //adjust pump for target pressure
          if ((currentPressure < targetPressure - 0.2) && (currentPressure > targetPressure - 5)) //if pressure is between these two numbers
          {
            if (currentPressure < (lastPressure + 0.1)) //this is the target increase of pressure per step
            {
              Pumps.IncreaseMainPump(0.1);
            }
            else //decrease pump if it is going too fast
            {
              Pumps.DecreaseMainPump(0.1);
            }
          }
          if ((currentPressure <= targetPressure - 5) && (currentPressure > targetPressure - 15)) //if pressure is between these two numbers
          {
            if (currentPressure < (lastPressure + 0.3)) //this is the target increase of pressure per step
            {
              Pumps.IncreaseMainPump(0.2);
            }
            else //decrease pump if it is going too fast
            {
              Pumps.DecreaseMainPump(0.2);
            }
          }
          if ((currentPressure <= targetPressure - 15) && (currentPressure > targetPressure - 25)) //if pressure is between these two numbers
          {
            if (currentPressure < (lastPressure + 0.5)) //this is the target increase of pressure per step
            {
              Pumps.IncreaseMainPump(0.3);
            }
            else //decrease pump if it is going too fast
            {
              Pumps.DecreaseMainPump(0.3);
            }
          }
          if ((currentPressure <= targetPressure - 25)) //if pressure is between these two numbers
          {
            if (currentPressure < (lastPressure + 0.7)) //this is the target increase of pressure per step
            {
              Pumps.IncreaseMainPump(0.5);
            }
            else //decrease pump if it is going too fast
            {
              Pumps.DecreaseMainPump(0.3);
            }
          }

          if (currentPressure > targetPressure)
          {
            //Pumps.SetPump2(0);
            //Pumps.SetPump2((Properties.Settings.Default.MainPumpStatePercent)*0.8);
            if ((pumpPowerAtEndOfLastStep * 0.6)<4)
            {
              Pumps.SetPump2(4);
            }
            else
            {
              Pumps.SetPump2(pumpPowerAtEndOfLastStep * 0.6);
            }
            
          }

          if ((currentPressure > (targetPressure - 0.1)) && (currentPressure < targetPressure + 0.1))
          {
            pressureHasBeenReached = true;
          }
        }
        

        //read temperatures
        reservoirTemp = COMMS.Instance.ReadAthenaTemp(3);
        if (Properties.Settings.Default.Chamber == "Ring")
        {
          chamberTemp = COMMS.Instance.ReadAthenaTemp(1);
        }
        else if (Properties.Settings.Default.Chamber == "Disk")
        {
          chamberTemp = COMMS.Instance.ReadAthenaTemp(2);
        }
        //get average temp and display it
        currentTemperature = (reservoirTemp + chamberTemp) / 2;

        //adjust temperature to target
        if (nowSettingTemp)
        {
          double targetTempinF = (Math.Round(((targetTemperature) * 9 / 5 + 32)));
          //set temperatures:
          COMMS.Instance.SetAthenaTemp(3, targetTempinF);//reservoir
          if (Properties.Settings.Default.Chamber == "Ring")
          {
            COMMS.Instance.SetAthenaTemp(1, targetTempinF);//chamber2
          }
          else if (Properties.Settings.Default.Chamber == "Disk")
          {
            COMMS.Instance.SetAthenaTemp(2, targetTempinF);//chamber1
          }
          
          if (((currentTemperature > targetTempinF - 1))) //therefore you must always go in steps of increasing temperature
          {
            temperatureHasBeenReached = true;
          }
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

    public void WriteHeaderForCapRep()
    {
      SR2.WriteLine("EXTENDED");
      SR2.WriteLine(" 6");
      SR2.WriteLine("Operator=EWS");
      SR2.WriteLine("Lot Number=11121");
      SR2.WriteLine("Hardware Serial Number=09182012-2092");
      SR2.WriteLine("Type of Test=Liquid Perm - Elevated");
      SR2.WriteLine("Lohm Table=lohmtable.cal");
      SR2.WriteLine("Piston Compression Pressure = 90");
      SR2.WriteLine("12-17-2015");
      SR2.WriteLine("UBC");
      SR2.WriteLine("LP");
      SR2.WriteLine(Properties.Settings.Default.CurrentViscosity);
      SR2.WriteLine("0");
      SR2.WriteLine("UBC A2");
      SR2.WriteLine(Properties.Settings.Default.SampleDiameter);
      SR2.WriteLine(Properties.Settings.Default.SampleThickness);
      SR2.WriteLine("0"); //atmosphereric pressure (0 for our gauges).
    }
    public void WriteHeader()
    {
      string paper = null;
      string sheets = null;
      string grammage = null;
      if (Properties.Settings.Default.paper)
      {
        paper = "Y";
        sheets = Properties.Settings.Default.paperSheets.ToString();
        grammage = Properties.Settings.Default.grammage.ToString();
      }
      else
      {
        paper = "N";
        sheets = "N/A";
        grammage = "N/A";
      }
      //Make a hat for the data.
      SR.WriteLine("Liquid Permeability Test");
      SR.WriteLine("");
      SR.WriteLine("Sample Info:");
      SR.WriteLine("");
      SR.WriteLine("Sample ID=" + sampleID);
      SR.WriteLine("Lot Number=" + lotNumber);
      //SR.WriteLine("Paper Sample=" + paper);
      //SR.WriteLine("Sheets=" + sheets);
      //SR.WriteLine("Grammage=" + grammage);
      SR.WriteLine("");
      SR.WriteLine("Test Details:");
      SR.WriteLine("");
      SR.WriteLine("Date= " + DateTime.Now.ToString("dd/MM/yyyy H:mm"));
      SR.WriteLine("Time Units=" + "Minutes");
      SR.WriteLine("Flow Units=" + "mL/min");
      if (Properties.Settings.Default.TempCorF == "C")
      {
        SR.WriteLine("Temperature Units=" + "Celsius");
      }
      else
      {
        SR.WriteLine("Temperature Units=" + "Fahrenheit");
      }
      SR.WriteLine("Pressure Units=PSI");
      SR.WriteLine("Steps=" + Properties.Settings.Default.NumberOfSteps);


      /*SR.Write("Pressure=");
      foreach (string item in Properties.Settings.Default.CollectionPressure)
      {
        SR.Write(item + "; ");
      }*/
      string pressure;
      pressure = Properties.Settings.Default.CollectionPressure[0];
      for (int i = 1; i < Properties.Settings.Default.CollectionPressure.Count; i++)
      {
        pressure = pressure + "; " + Properties.Settings.Default.CollectionPressure[i];
      }
      SR.Write("Pressure=" + pressure);

      SR.WriteLine(""); //to end the line

      /*SR.Write("Duration=");
      foreach (string item in Properties.Settings.Default.CollectionDuration)
      {
        SR.Write(item + "; ");
      }*/

      string Duration;
      Duration = Properties.Settings.Default.CollectionDuration[0];
      for (int i = 1; i < Properties.Settings.Default.CollectionDuration.Count; i++)
      {
        Duration = Duration + "; " + Properties.Settings.Default.CollectionDuration[i];
      }
      SR.Write("Time=" + Duration);

      SR.WriteLine("");//to end the line

      /*SR.Write("Temperature=");
      foreach (string item in Properties.Settings.Default.CollectionTemperature)
      {
        SR.Write(item + "; ");
      }*/
      string Temperature;
      Temperature = Properties.Settings.Default.CollectionTemperature[0];
      for (int i = 1; i < Properties.Settings.Default.CollectionTemperature.Count; i++)
      {
        Temperature = Temperature + "; " + Properties.Settings.Default.CollectionTemperature[i];
      }
      SR.Write("Temperature=" + Temperature);

      SR.WriteLine("");//to end the line
      //SR.WriteLine("Pressure Rate(mL / min)=" + pressureRate.ToString());
      SR.WriteLine("");
      SR.WriteLine("Data:");
      SR.WriteLine("");
      //SR.WriteLine("Time,\tFlow,\tTemperature,\tPressure");
      SR.WriteLine("{0,10}\t{1,10}\t{2,10}\t{3,10}", "Time", "Flow", "Temperature", "Pressure");
      SR.WriteLine("");
    }

    private void backgroundWorkerReadAndDisplay_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      //temp
      currentTemperatureInC = (Convert.ToDouble((currentTemperature - 32) * 5 / 9));
      label6.Text = "Temperature  =  " + String.Format("{0:0}", currentTemperatureInC) + " C / " + String.Format("{0:0}", currentTemperature) + " F";

      displayVolumeLevels();
      labelPressure.Text = "Pressure  =  " + String.Format("{0:0.0} PSI", currentPressure);
      labelTargetPressure.Text = "Pressure  =  " + targetPressure + " (PSI)";
      pumpstate = Properties.Settings.Default.MainPumpStatePercent.ToString("#0.0");
      labelPumpState.Text = "Pump Power  =  " + pumpstate + "%";
      labelStepsTotal.Text = "Total Steps  =  " + Properties.Settings.Default.NumberOfSteps.ToString();
      if (testFinished)
      {
        labelStepCurrent.Text = "Step  =  End";
      }
      else
      {
        labelStepCurrent.Text = "Step  =  " + (stepCount + 1);
        //higlight the current row in the dataGridview:
        if (stepCount!=0)
        {
          dataGridView2.Rows[stepCount-1].Selected = false;
        }
        dataGridView2.Rows[stepCount].Selected = true;
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      stopButton();
    }

    private void stopButton()
    {
      Valves.OpenValve2();
      pressureHasBeenReached = true;
      stepTimeReached = true;
      abort = true;
      stopwatchStep.Stop();
      stopwatch.Stop();
      Thread.Sleep(1000); //wait for pressure adjust loop to finish before stopping pump or else pump might be turned on.
      Pumps.SetPump2(0);
      backgroundWorkerMainLoop.CancelAsync();
      Thread.Sleep(1000);
      backgroundWorkerReadAndDisplay.CancelAsync();
      button2.Enabled = false;
      timerForStopWatch.Stop();
    }

    private void AutoScrn_FormClosing(object sender, FormClosingEventArgs e)
    {
      panel1.Visible = true;
      labelPanel.Text = "Closing... Please Wait";
      panel1.Refresh();
      stepTimeReached = true;
      abort = true;
      stopwatchStep.Stop();
      stopwatch.Stop();
      Pumps.SetPump2(0);
      backgroundWorkerMainLoop.CancelAsync();
      backgroundWorkerReadAndDisplay.CancelAsync();
      timerForStopWatch.Stop();
      stopButton();
    }

    private void timerForStopWatch_Tick(object sender, EventArgs e)
    {
      timeSpanTotal = stopwatch.Elapsed;
      timeSpanStep = stopwatchStep.Elapsed;
      totalTime = String.Format("{0:00}:{1:00}", timeSpanTotal.Minutes, timeSpanTotal.Seconds);
      labelTotalTime.Text = "Total time = " + totalTime;
      labelStepTime.Text = "Step time = " + String.Format("{0:00}:{1:00}", timeSpanStep.Minutes, timeSpanStep.Seconds);
      stepTimeInMinutes = Convert.ToDouble(timeSpanStep.Minutes) + Convert.ToDouble(timeSpanStep.Seconds) / 60;
      if (stepTimeInMinutes >= Convert.ToDouble(Properties.Settings.Default.CollectionDuration[stepCount]))
      {
        stepTimeReached = true;
      }
    }

    private void checkBoxShowPressureGraph_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxShowPressureGraph.Checked)
      {
        chart1.Series["SeriesPressure"].Enabled = true;
        chart1.Series["SeriesPressure"].Points.AddXY(totalTimeInMinutes.ToString("#0.00"), currentPressure.ToString("#0.00"));
        chart1.Update();
        chart1.Refresh();
        chart1.Series["SeriesPressure"].Points.RemoveAt(chart1.Series["SeriesPressure"].Points.Count-1);
      }
      else
      {
        chart1.Series["SeriesPressure"].Enabled = false;
        chart1.Series["SeriesPressure"].Points.AddXY(totalTimeInMinutes.ToString("#0.00"), currentPressure.ToString("#0.00"));
        chart1.Update();
        chart1.Refresh();
        chart1.Series["SeriesPressure"].Points.RemoveAt(chart1.Series["SeriesPressure"].Points.Count-1);
      }
    }

    private void checkBoxShowTemperatureGraph_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBoxShowTemperatureGraph.Checked)
      {
        chart1.Series["SeriesTemperature"].Enabled = true;
        chart1.Series["SeriesTemperature"].Points.AddXY(totalTimeInMinutes.ToString("#0.00"), currentTemperature.ToString("#0.00"));
        chart1.Update();
        chart1.Refresh();
        chart1.Series["SeriesTemperature"].Points.RemoveAt(chart1.Series["SeriesTemperature"].Points.Count - 1);
      }
      else
      {
        chart1.Series["SeriesTemperature"].Enabled = false;
        chart1.Series["SeriesTemperature"].Points.AddXY(totalTimeInMinutes.ToString("#0.00"), currentTemperature.ToString("#0.00"));
        chart1.Update();
        chart1.Refresh();
        chart1.Series["SeriesTemperature"].Points.RemoveAt(chart1.Series["SeriesTemperature"].Points.Count - 1);
      }
    }
  }
}
