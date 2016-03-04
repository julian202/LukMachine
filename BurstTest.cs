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
    bool overVolume = false;
    double currentPressure; double startTime = 0; double stoppedTime = 0; double startStoppedTime = 0; double endStoppedTime = 0;
    double currentTime = 0; double outputTime = 0; double outputPressure = 0; string counts = ""; double realCounts = 0;
    //double ground = Properties.Settings.Default.ground;
    //double twoVolt = Properties.Settings.Default.twoVolt;
    int ReservoirPercent;
    int CollectedPercent;

    //Set up data file
    StreamWriter SR;

    private double currentTemp;
    private double targetTemp;
    private double athena1Temp; //temp of tank
    private double athena2Temp; //temp of pipes
    private double chamberTemp;

    double p1Psi;
    double rawP1;
    double targetPressure = 0;
    double currentDuration;

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

      //COMMS.Instance.MoveMotorValve(1, "C");
      Pumps.StopPump1();

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
        Progress("Preparing to stop the test, please wait...");
        testPaused = false;
        abort = true;
        //COMMS.Instance.Pause(1); //wait 1 second
        //System.Windows.Forms.MessageBox.Show("Data saved to " + Properties.Settings.Default.TestData);

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
        //testPaused = false;
        //Progress("You can close this window now");
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

    public void ReadPressureAndSetLabel()
    {
      //read pressure gauge, convert to PSI (will need to * by conversion factor and set units label later)
      counts = COMMS.Instance.ReadPressureGauge(1);
      realCounts = Convert.ToDouble(counts);
      currentPressure = (realCounts - ground) * Properties.Settings.Default.p1Max / twoVolt;  //twoVolt is 60000
      outputPressure = currentPressure * pConversion;
      p1Psi = outputPressure;
      rawP1 = realCounts;
      if (p1Psi < 0)
      {
        p1Psi = 0;
      }
      Progress("display pressure =" + outputPressure.ToString()); //this also displays Pump % state.
    }

    public void SetTargetPressureLabel()
    {
      Progress("display target pressure =" + targetPressure.ToString());
    }

    public void SetDurationLabel()
    {
      Progress("display duration =" + currentDuration.ToString());
    }

    public void GoToTargetPressure()
    {
      ReadPressureAndSetLabel();
      SetTargetPressureLabel();
      SetDurationLabel();
 
        while (((currentPressure < (targetPressure - 0.1)) || (currentPressure > targetPressure + 0.1)) && !abort)
      {
        System.Diagnostics.Debug.WriteLine("currentPressure is " + currentPressure.ToString() + ",  targetPressure is " + targetPressure.ToString());

        ReadPressureAndSetLabel();
        Thread.Sleep(500);//wait before checking again
                          //Console.WriteLine("athena1Temp " + athena1Temp);
                          //Console.WriteLine("chamberTemp " + chamberTemp);
        if (targetPressure > currentPressure)
        {
          Pumps.IncreaseMainPump(1);
        }
        else if (targetPressure < currentPressure)
        {
          Pumps.DecreaseMainPump(1);
        }
      }
    }

    public void PumpCollectedVolumeToReservoir() //refill reservoir with the liquid from the collected volume
    {
      //Progress("Read volume levels");
      int ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
      int CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
      //MessageBox.Show("ReservoirPercent "+ ReservoirPercent+ " CollectedPercent "+ CollectedPercent);
      Progress("display volume levels =" + ReservoirPercent.ToString() + "=" + CollectedPercent.ToString());//fix this, dont read twice

      int maxPercentFull = Properties.Settings.Default.maxEmptyCollectedPercentFull;
      if (CollectedPercent > maxPercentFull)
      {
        Progress("Emptying collected volume...");
        Valves.OpenValve1();
        Valves.CloseValve2();
        Valves.CloseValve3();
        //start refill pump 1
        Pumps.StartPump1();
      }
      int test = COMMS.Instance.getCollectedLevelPercent();
      while (CollectedPercent >= maxPercentFull) //if collected reservoir is more than 5% full empty it:
      {
        //Progress("Read volume levels");
        ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
        CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
        Progress("display volume levels =" + ReservoirPercent.ToString() + "=" + CollectedPercent.ToString());//fix this, dont read twice

        Thread.Sleep(200);
        //wait until collected level is below minpercent
      }
      //stop refill pump 1
      Pumps.StopPump1();
      Valves.CloseValve1();
    }

    public void HeatMachineToTargetTemperature()
    {
      Progress("set label7 to targetTemp="+targetTemp.ToString());
      try
      {
        //targetTemp = Properties.Settings.Default.selectedTemp;


        //set heaters to target temp:
        COMMS.Instance.SetAthenaTemp(1, (Math.Round(((targetTemp) * 9 / 5 + 32))));
        COMMS.Instance.SetAthenaTemp(2, (Math.Round(((targetTemp) * 9 / 5 + 32))));
        COMMS.Instance.SetAthenaTemp(3, (Math.Round(((targetTemp) * 9 / 5 + 32))));
        COMMS.Instance.SetAthenaTemp(4, (Math.Round(((targetTemp) * 9 / 5 + 32))));

        while ((currentTemp < targetTemp - 2) || (currentTemp > targetTemp + 2))
        {
          ReadTemperatureAndSetLabel();
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

    public void ReadTemperatureAndSetLabel()
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
    }
    public bool IsNumeric(string input)
    {
      double test;
      return double.TryParse(input, out test);
    }

    int stepCount;
    ////////////////////////////////////////////////////////////////////////////////////////////
    //RunBurstTest()////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////

    public void RunBurstTest()
    {
      //Check reservoir levels
      Progress("Checking reservoir levels...");
      PumpCollectedVolumeToReservoir();
      Thread.Sleep(1000);
      Progress("Done checking reservoir levels");

      //Check temperature levels
      string nextTemperature= Properties.Settings.Default.CollectionTemperature[0];    
      if (IsNumeric(nextTemperature))  //Properties.Settings.Default.useTemperature
      {
        targetTemp = Convert.ToDouble(nextTemperature);
        Progress("Heating machine to target temperature. Please wait...");
        HeatMachineToTargetTemperature(); //this enters a while loop until the temperatue is right.
        Progress("Done Heating machine to target temperature");
      }

      //set presurre to target pressure
      Progress("Setting pressure to target pressure. Please wait...");
      stepCount = 0;
      targetPressure = Convert.ToDouble(Properties.Settings.Default.CollectionPressure[stepCount]);
      currentDuration = Convert.ToDouble(Properties.Settings.Default.CollectionDuration[stepCount]);
      GoToTargetPressure();
      Progress("Target pressure reached");


      //Hide "please wait" panel
      Progress("hide panel1");
      //Progress("disable stop button"); 

      //Write data to file
      //InitializeTest();
      SR = new StreamWriter(dataFile);
      WriteHeader();
      SR.Close();
      

      startTime = Convert.ToDouble(Environment.TickCount); //+ 500.0
      maxPressure *= pConversion;
      maxTestPressure *= pConversion;

      //Main loop
      
      Progress("Now running");
      string collectedLevelCount;
      while (!abort && !overVolume)
      {
        //Read pressure
        ReadPressureAndSetLabel();

        //If pressure is too low increase pump by 1% and viceversa
        if (((currentPressure < (targetPressure - 0.1)) || (currentPressure > targetPressure + 0.1)) && !abort)
        {
          System.Diagnostics.Debug.WriteLine("currentPressure is " + currentPressure.ToString() + ",  targetPressure is " + targetPressure.ToString());
          if (targetPressure > currentPressure)
          {
            Pumps.IncreaseMainPump(1);
          }
          else if (targetPressure < currentPressure)
          {
            Pumps.DecreaseMainPump(1);
          }
        }

        //read penetrometers
        ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
        CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
        Progress("display volume levels =" + ReservoirPercent.ToString() + "=" + CollectedPercent.ToString());//fix this, dont read twice

        //read temperature
        ReadTemperatureAndSetLabel();


        //read current time
        currentTime = Convert.ToDouble(Environment.TickCount) - startTime - stoppedTime;
        outputTime = currentTime / 1000;

        //keep rough track of how many mL we have pumped.
        double mlsMoved = (pressureRate / 60.0) * outputTime;

        //write data to file and report progress
        SR = new StreamWriter(dataFile, true);
        collectedLevelCount = COMMS.CollectedLevelCount.ToString();
        SR.WriteLine(outputTime.ToString("0.00") + "," + collectedLevelCount + "," + currentTemp.ToString("0.0") + "," + currentPressure.ToString("0.000"));
        SR.Close();
        Progress("Reading:" + outputTime.ToString("0.00") + "," + collectedLevelCount + "," + currentTemp.ToString("0.0") + "," + currentPressure.ToString("0.000"));
        Thread.Sleep(500);// this is the main sleep of the thread between reading so that it doesn't go too fast.
        if (outputTime / 60 > currentDuration)
        {
          //save current time
          startStoppedTime = Convert.ToDouble(Environment.TickCount);
          Progress("Ended Period");
          //set presurre to target pressure
          Progress("Setting pressure to target pressure for next period. Please wait...");
          stepCount++;
          
          //end if stepCount is bigger than the number of steps (i.e. periods)
          if (stepCount>= Properties.Settings.Default.CollectionPressure.Count)
          {
            abort = true;
            Progress("end Test");
          }
          else
          {
            targetPressure = Convert.ToDouble(Properties.Settings.Default.CollectionPressure[stepCount]);
            currentDuration = Convert.ToDouble(Properties.Settings.Default.CollectionDuration[stepCount]);
            GoToTargetPressure();
            Progress("Target pressure reached");

            nextTemperature = Properties.Settings.Default.CollectionTemperature[stepCount];
            if (IsNumeric(nextTemperature))  //if it's not numeric then it is "-" and should be ignored
            {
              targetTemp = Convert.ToDouble(nextTemperature);
              Progress("Heating machine to target temperature. Please wait...");
              HeatMachineToTargetTemperature(); //this enters a while loop until the temperatue is right.
              Progress("Done Heating machine to target temperature");
            }


            Progress("Starting next Period");
            endStoppedTime = Convert.ToDouble(Environment.TickCount);
            stoppedTime = stoppedTime + (endStoppedTime - startStoppedTime);
            //set time that next period should stop (by getting current time in minutes and adding the duration of the next period)
            currentTime = Convert.ToDouble(Environment.TickCount) - startTime - stoppedTime;
            currentDuration = (currentTime / 1000) / 60 + (currentDuration);
          }
          
        }

        //Stop if over max pressure.
        if (outputPressure > p1Max) //add to this if volume is empty or pent is full
        {
          abort = true;
          testPaused = true;
          //stop main pump
          Pumps.SetPump2(0);
          //open relief pressure valve
          Valves.OpenValve2();
          Progress("disable stop button");
          Progress("Machine has reached it's maximum pressure range! The test will be aborted.");
          Progress("Data saved to " + Properties.Settings.Default.TestData);
          SR = new StreamWriter(dataFile, true);
          SR.WriteLine("Machine has reached it's maximum pressure range! The test was aborted. Pressure was " + outputPressure.ToString() + ", max is " + p1Max.ToString());
          SR.Close();

          MessageBox.Show("Machine has reached it's maximum pressure. The test has been stopped. Data saved to " + Properties.Settings.Default.TestData);
          //COMMS.Instance.Pause(1); //wait 1 second for other thread to finish      
        }

        //Stop if over max volume.
        if (CollectedPercent >= Properties.Settings.Default.MaxPent3PercentFull)
        {
          System.Diagnostics.Debug.WriteLine("volumeReading is " + COMMS.CollectedLevelCount);
          System.Diagnostics.Debug.WriteLine("MaxPent3Reading is " + Properties.Settings.Default.MaxPent3PercentFull);
          abort = true;
          testPaused = true;
          overVolume = true;
          //stop main pump
          Pumps.SetPump2(0);
          //open relief pressure valve
          Valves.OpenValve2();
          Progress("disable stop button");
          Progress("Machine has reached it's maximum volume range, the test has stopped.");
          SR = new StreamWriter(dataFile, true);
          SR.WriteLine("Machine has reached it's maximum volume range, the test was aborted.");
          SR.Close();
          System.Windows.Forms.MessageBox.Show("Machine has reached it's maximum volume range. The test has been stopped. Data saved to " + Properties.Settings.Default.TestData);
          //COMMS.Instance.Pause(1); //wait 1 second for other thread to finish
        }
        //see if we need to pause, or abort.
        //PauseTest();      
      }
      if (abort)
      {
        //Progress("Test Stopped");
        System.Diagnostics.Debug.WriteLine("Test aborted successfully becuase this message is sent from BurstTest thread");
      }
      //SR.Close();
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
      SR.WriteLine(DateTime.Now.ToString("dd/MM/yyyy H:mm"));
      SR.WriteLine("Pressure Units=" + pUnits);
      //SR.WriteLine("Pressure Rate(mL / min)=" + pressureRate.ToString());
      SR.WriteLine("");
      SR.WriteLine("Data:");
      SR.WriteLine("");
      SR.WriteLine("Time, Collected Volume, Temperature, Pressure");
      SR.WriteLine("");
    }
  }
}
