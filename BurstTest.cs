using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace LukMachine
{
  class BurstTest
  {
    public delegate void UpdateTextCallback(string text);
    public delegate void ProgressEventHandler(string message);
    public event ProgressEventHandler Progress;
    public bool testPaused = false;
    public bool abort = false;

    private string runPumpCMD = "#RU" + (char)13;
    private string stopPumpCMD = "#ST" + (char)13;

    //global settings
    private double pConversion = Properties.Settings.Default.defaultPressureConversion;
    private double maxPressure = Convert.ToDouble(Properties.Settings.Default.maxPressure);
    private int p1Max = Properties.Settings.Default.p1Max;
    private string pUnits = Properties.Settings.Default.defaultPressureUnit;

    //test specific settings
    private string sampleID = Properties.Settings.Default.TestSampleID;
    private string lotNumber = Properties.Settings.Default.TestLotNumber;
    private double maxTestPressure = Convert.ToDouble(Properties.Settings.Default.TestMaximumPressure);
    private double pressureRate = Properties.Settings.Default.TestRate;
    private double dropPressure = Convert.ToDouble(Properties.Settings.Default.TestDetection);
    private string dataFile = Properties.Settings.Default.TestData;
    private int twoVolt = COMMS.Instance.get2v();
    private int ground = COMMS.Instance.getGround();
    private bool pumpRunning = false;
    //
    //Set up data file
    StreamWriter SR;

    private double currentTemp;
    private double targetTemp;
    private double athena1Temp; //temp of tank
    private double athena2Temp; //temp of pipes
    private double chamberTemp;

    private void PassThrough(char c, string toSend) //PassThrough('A', stopPumpCMD); stopPumpCMD = "#ST" + (char)13; runPumpCMD = "#RU" + (char)13;  
    {
      string chrs = toSend.Length.ToString("00");
      COMMS.Instance.Send(">" + c + chrs + toSend);
    }

    private void InitializeTest()
    {
      //set everything to proper running positions. 
      //make sure pump is stopped
      Progress("Initiliazing test...");
      PassThrough('A', stopPumpCMD);
      COMMS.Instance.MoveMotorValve(1, "C");
      //COMMS.Instance.Pause(30);
      //need to watch valve until it has stopped.
      bool notClosed = true; int mvPos;
      //while valve is not closed.
      Progress("Waiting for valve to close...");
      while (notClosed)
      {
        //get MV position
        mvPos = Convert.ToInt32(COMMS.Instance.MotorValvePosition(1));
        //remember it twice :/
        //wait a half a second
        COMMS.Instance.Pause(.5);
        //see if it is "closed"
        if (mvPos <= Convert.ToInt32(COMMS.Instance.MotorValveCloseLimit(1)))
        {
          //valve is closed.
          notClosed = false;
        }
      }
      Progress("Ready...");
      COMMS.Instance.Pause(5);
    }

    public void AbortTest()
    {
      try
      {


        Progress("Preparing to cancel the test, please wait...");
        testPaused = false;
        abort = true;
        //COMMS.Instance.Pause(1); //wait 1 second
        System.Windows.Forms.MessageBox.Show("Data saved to " + Properties.Settings.Default.TestData);

        /*
        //stop main pump
        Progress("Stopping pump...");
        COMMS.Instance.ZeroRegulator(1);
        pumpRunning = false;

        //open relief pressure valve
        Progress("Relieving pressure...");
        COMMS.Instance.MoveValve(2, "O");
        */

        /*
        //open pent valve so that it drains to reservoir
        COMMS.Instance.MoveValve(1, "O");

        //start refill pump
        Progress("Disposing collected volume...");
        COMMS.Instance.MoveMotorValve(1, "O");*/

        //COMMS.Instance.Pause(15);
        testPaused = false;
        Progress("You can close this window now");
      }
      catch { }
    }

    public void PauseTest()
    {
      if (testPaused)
      {
        //stop machine stuff.
        if (pumpRunning)
        {
          PassThrough('A', stopPumpCMD);
        }
        //stop pump incase it was running.
        //stop motor valve incase it was running.
        Progress("Test has been paused.");
        while (testPaused && !abort)
        {
          //hold up the thing unless aborted.
          if (abort)
          {
            testPaused = false;
            AbortTest();
            return;
          }
        }
        if (!abort)
        {
          Progress("Test resumed.");
          //convert pressure rate to pump speed
          //PassThrough('A', runPumpCMD);
          pumpRunning = true;
        }
      }
    }

    public void ResumeTest()
    {
      testPaused = false;
    }

    public void PumpCollectedVolumeToReservoir() //refill reservoir with the liquid from the collected volume
    {
      Progress("Read volume levels");
      int maxPercentFull = 5;
      if (COMMS.Instance.getCollectedLevelPercent() > maxPercentFull)
      {
        Valves.OpenValve1();
        Valves.CloseValve2();
        Valves.CloseValve3();
        //start refill pump 1
        Pumps.StartPump1();
      }
      int test =COMMS.Instance.getCollectedLevelPercent();
      while (COMMS.Instance.getCollectedLevelPercent() >= maxPercentFull) //if collected reservoir is more than 5% full empty it:
      {
        Progress("Read volume levels");
        Thread.Sleep(200);
        //wait until collected level is below minpercent
      }
      //stop refill pump 1
      Pumps.StopPump1();
      Valves.CloseValve1();
    }


    public void HeatMachineToTargetTemperature()
    {
      Progress("set label7 to targetTemp");
      try
      {
        targetTemp = Properties.Settings.Default.selectedTemp;
        while ((currentTemp < targetTemp - 2) || (currentTemp > targetTemp + 2))
        {
          //read tank temperature
          athena1Temp = COMMS.Instance.ReadAthenaTemp(1);
          //read pipes temperature
          athena2Temp = COMMS.Instance.ReadAthenaTemp(2);
          //read selected chamber temperature
          if (Properties.Settings.Default.Chamber == "Ring")
          {
            chamberTemp = COMMS.Instance.ReadAthenaTemp(3);
          }
          else if (Properties.Settings.Default.Chamber == "Disk")
          {
            chamberTemp = COMMS.Instance.ReadAthenaTemp(4);
          }
          //get average temp and display it
          currentTemp = (athena1Temp + athena2Temp + chamberTemp) / 3;
          Progress("set label5 to currentTemp=" + currentTemp.ToString());
          Thread.Sleep(2000);//wait before checking again
          //Console.WriteLine("athena1Temp " + athena1Temp);
          //Console.WriteLine("chamberTemp " + chamberTemp);
        }    
      }
      catch (Exception ex)
      {
        MessageBox.Show("Problem reading temperature from Athena ");
      }
    }

    public void RunBurstTest()
    {
      Progress("Checking reservoir levels...");
      PumpCollectedVolumeToReservoir();
      Thread.Sleep(1000);
      Progress("Done checking reservoir levels...");
      if (Properties.Settings.Default.useTemperature)
      {
        HeatMachineToTargetTemperature(); //this enters a while loop until the temperatue is right.
      }
      Progress("hide panel1");

      //Progress("disable stop button"); 

      //panel1.Visible = false;
      Progress("Test started...");

      //run main pump
      if (Properties.Settings.Default.SelectedFlowRate == "Low")
      {
        //run main pump at low setting
        COMMS.Instance.SetRegulator(1, (Convert.ToInt32(Properties.Settings.Default.LowPumpSetting)) * 4000 / 100);  //2600 is 1/3 of 4000 
        //System.Windows.Forms.MessageBox.Show(((Convert.ToInt32(Properties.Settings.Default.LowPumpSetting)) * 4000 / 100).ToString());
      }
      if (Properties.Settings.Default.SelectedFlowRate == "Medium")
      {
        //run main pump at medium setting
        COMMS.Instance.SetRegulator(1, (Convert.ToInt32(Properties.Settings.Default.MediumPumpSetting)) * 4000 / 100);  //2600 is 2/3 of 4000
      }
      if (Properties.Settings.Default.SelectedFlowRate == "High")
      {
        //run main pump at high setting
        COMMS.Instance.SetRegulator(1, (Convert.ToInt32(Properties.Settings.Default.HighPumpSetting)) * 4000 / 100);
      }

      SR = new StreamWriter(dataFile);
      //InitializeTest();
      WriteHeader();
      bool overVolume = false;
      double currentPressure; double startTime = 0;
      double currentTime = 0; double outputTime = 0; double outputPressure = 0; string counts = ""; double realCounts = 0;
      string volumeReading = ""; //is pent 3 reading.
      double volumeConverted;
      double ground = Properties.Settings.Default.ground;
      double twoVolt = Properties.Settings.Default.twoVolt;

      startTime = Convert.ToDouble(Environment.TickCount) + 500.0;
      maxPressure *= pConversion;
      maxTestPressure *= pConversion;
      while (!abort && !overVolume)
      {
        //read pressure
        counts = COMMS.Instance.ReadPressureGauge(1);
        realCounts = Convert.ToDouble(counts);
        currentPressure = (realCounts - ground) * Properties.Settings.Default.p1Max / twoVolt;  //twoVolt is 60000
        outputPressure = currentPressure * pConversion;

        //read penetrometer
        volumeReading = COMMS.Instance.MotorValvePosition(1); //this would be pent 3 (the testing pent)
        volumeConverted = Convert.ToInt32(volumeReading) * 1;

        //read current time
        currentTime = Convert.ToDouble(Environment.TickCount) - startTime;
        outputTime = currentTime / 1000;

        //keep rough track of how many mL we have pumped.
        double mlsMoved = (pressureRate / 60.0) * outputTime;

        //write data to file and report progress
        SR.WriteLine(outputTime.ToString("#.000") + "," + volumeConverted.ToString("###.000"));
        Progress("Reading:" + outputTime.ToString("#.000") + "," + volumeConverted.ToString("###.000"));


        //Stop if over max pressure.
        if (outputPressure > p1Max) //add to this if volume is empty or pent is full
        {
          abort = true;
          testPaused = true;
          //stop main pump
          COMMS.Instance.ZeroRegulator(1);
          //open relief pressure valve
          COMMS.Instance.MoveValve(2, "O");
          Progress("Machine has reached it's maximum pressure range! The test will be aborted.");
          Progress("Data saved to " + Properties.Settings.Default.TestData);
          SR.WriteLine("Machine has reached it's maximum pressure range! The test was aborted.");
          SR.Close();
          System.Windows.Forms.MessageBox.Show("Machine has reached it's maximum pressure. The test has been stopped. Data saved to " + Properties.Settings.Default.TestData);
          COMMS.Instance.Pause(1); //wait 1 second for other thread to finish
        }

        //Stop if over max volume.
        if (Convert.ToInt32(volumeReading) >= Convert.ToInt32(Properties.Settings.Default.MaxPent3Reading))
        {
          abort = true;
          testPaused = true;
          overVolume = true;
          //stop main pump
          COMMS.Instance.ZeroRegulator(1);
          //open relief pressure valve
          COMMS.Instance.MoveValve(2, "O");
          Progress("Machine has reached it's maximum volume range, the test has stopped.");
          SR.WriteLine("Machine has reached it's maximum volume range, the test was aborted.");
          SR.Close();
          System.Windows.Forms.MessageBox.Show("Machine has reached it's maximum volume range. The test has been stopped. Data saved to " + Properties.Settings.Default.TestData);
          COMMS.Instance.Pause(1); //wait 1 second for other thread to finish
        }
        //see if we need to pause, or abort.
        //PauseTest();
      }
      if (abort)
      {
        Progress("Test Stopped");
      }
      SR.Close();
      Progress("Test Ended");
    }

    public void RunBurstTest2()
    {
      double currentTime = 0; double outputTime = 0; double startTime = 0;
      startTime = Convert.ToDouble(Environment.TickCount);

      while (!abort)
      {
        currentTime = Convert.ToDouble(Environment.TickCount);
        outputTime = (currentTime - startTime) / 1000;
        Progress(outputTime.ToString());
      }
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
      SR.WriteLine("Sample Info");
      SR.WriteLine("");
      SR.WriteLine("Sample ID=" + sampleID);
      SR.WriteLine("Lot Number=" + lotNumber);
      //SR.WriteLine("Paper Sample=" + paper);
      //SR.WriteLine("Sheets=" + sheets);
      //SR.WriteLine("Grammage=" + grammage);
      SR.WriteLine("");
      SR.WriteLine("Test Details");
      SR.WriteLine("");
      SR.WriteLine(DateTime.Now.ToString("dd/MM/yyyy H:mm"));
      SR.WriteLine("Pressure Units=" + pUnits);
      //SR.WriteLine("Pressure Rate(mL / min)=" + pressureRate.ToString());
      SR.WriteLine("");
      SR.WriteLine("Data");
      SR.WriteLine("");
    }
  }
}
