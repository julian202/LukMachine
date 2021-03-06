﻿using System;
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
    private string dataFilePathForCapRep;
    private int twoVolt = COMMS.Instance.get2v();
    private int ground = COMMS.Instance.getGround();
    private bool pumpRunning = false;
    //
    bool overVolume = false;
    double currentPressure; double startTime = 0; double stoppedTime = 0; double startStoppedTime = 0; double endStoppedTime = 0;
    double currentTime = 0; double outputTime = 0; double outputPressure = 0; string counts = ""; double realCounts = 0;
    double lastTime = 0; double timeDifferenceInMinutes;
    //double ground = Properties.Settings.Default.ground;
    //double twoVolt = Properties.Settings.Default.twoVolt;
    int ReservoirPercent;
    int CollectedPercent;
    double CollectedCount;
    double LastCollectedCount;
    double CollectedDifferenceInCounts;
    double Flow;
    double FlowInMl;
    bool mustNotCountFlowBecausePressureIsAdjusting;

    //Set up data file
    StreamWriter SR;
    StreamWriter SR2;

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
      currentPressure = (realCounts - ground) * Properties.Settings.Default.p1Max / 60000;  //twoVolt is 60000
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
      Progress("panel label setting pressure");
      ReadPressureAndSetLabel();
      SetTargetPressureLabel();
      SetDurationLabel();
      pressureAdjustLoop();
      //repeat so that it goes down if it overshot:
      Thread.Sleep(1000);
      pressureAdjustLoop();
    }
    public void pressureAdjustLoop()
    {
      while ((((currentPressure < (targetPressure - 0.1)) || (currentPressure > targetPressure + 0.1)) && !abort)&& !COMMS.skipPressure)
      {
        System.Diagnostics.Debug.WriteLine("currentPressure is " + currentPressure.ToString() + ",  targetPressure is " + targetPressure.ToString());

        ReadPressureAndSetLabel();
        Thread.Sleep(7000);//wait before checking again
        if (currentPressure<targetPressure)
        {
          Pumps.IncreaseMainPump(1);
        }
        else if (currentPressure > targetPressure )
        {
          //Pumps.DecreaseMainPump(1);
          Pumps.SetPump2(0);
        }
        //change at faster speed if we are far from target
        if (currentPressure < (targetPressure-10))
        {
          Pumps.IncreaseMainPump(1);
        }
        else if (currentPressure > (targetPressure + 10))
        {
          Pumps.DecreaseMainPump(1);
        }
        //change at faster speed if we are far from target
        if (currentPressure < (targetPressure - 40))
        {
          Pumps.IncreaseMainPump(1);
        }
        else if (currentPressure > (targetPressure + 40))
        {
          Pumps.DecreaseMainPump(1);
        }

      }
    }


    public void EmptyCollectedVolume() //refill reservoir with the liquid from the collected volume
    {
      CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
      ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
      //MessageBox.Show("ReservoirPercent "+ ReservoirPercent+ " CollectedPercent "+ CollectedPercent);
      Progress("display volume levels =" + ReservoirPercent.ToString() + "=" + CollectedPercent.ToString());//fix this, dont read twice

      int maxPercentFull = Properties.Settings.Default.maxEmptyCollectedPercentFull;
      if (CollectedPercent > maxPercentFull)
      {
        Progress("Emptying collected volume...");
        Valves.OpenValve1();
      }
      while (CollectedPercent > maxPercentFull)
      {
        Thread.Sleep(300);
        CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
        Progress("display volume levels =" + ReservoirPercent.ToString() + "=" + CollectedPercent.ToString());//fix this, dont read twice
      }
    }
    public void FillReservoir() //refill reservoir with the liquid from the collected volume
    {
      if (ReservoirPercent < 80)
      {
        Progress("Reservoir is not full. Please add more volume to reservoir.");
        while (ReservoirPercent < 80)
        {
          Thread.Sleep(300);
          System.Diagnostics.Debug.WriteLine("waiting for volume to fill... ");
          ReservoirPercent = COMMS.Instance.getReservoirLevelPercent();
          Progress("display volume levels =" + ReservoirPercent.ToString() + "=" + CollectedPercent.ToString());//fix this, dont read twice

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
      Progress("set label7 to targetTemp=" + targetTemp.ToString());
      try
      {
        //targetTemp = Properties.Settings.Default.selectedTemp;

        //set heaters to target temp:
        double targetTempinF = (Math.Round(((targetTemp) * 9 / 5 + 32)));
        COMMS.Instance.SetAthenaTemp(1, targetTempinF);
        COMMS.Instance.SetAthenaTemp(2, targetTempinF);
        COMMS.Instance.SetAthenaTemp(3, targetTempinF);
        //COMMS.Instance.SetAthenaTemp(4, targetTempinF);

        while (((currentTemp < targetTempinF - 1) || (currentTemp > targetTempinF + 1)) && !COMMS.skipTemp)
        {
          ReadTemperatureAndSetLabel();
          Thread.Sleep(1500);//wait before checking again
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
      //athena2Temp = COMMS.Instance.ReadAthenaTemp(2);
      //read selected chamber temperature
      if (Properties.Settings.Default.Chamber == "Ring")
      {
        chamberTemp = COMMS.Instance.ReadAthenaTemp(3);
      }
      else if (Properties.Settings.Default.Chamber == "Disk")
      {
        chamberTemp = COMMS.Instance.ReadAthenaTemp(2);
      }
      //get average temp and display it
      currentTemp = (athena1Temp + chamberTemp) / 2;
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
    ////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////

    public void RunBurstTest()
    {
      //Check reservoir levels
      Progress("Checking reservoir levels...");
      //PumpCollectedVolumeToReservoir();
      EmptyCollectedVolume();
      FillReservoir();
      Thread.Sleep(500);
      Progress("Done checking reservoir levels");

      //Check temperature levels
      string nextTemperature = Properties.Settings.Default.CollectionTemperature[0];
      if (IsNumeric(nextTemperature))  //Properties.Settings.Default.useTemperature
      {
        targetTemp = Convert.ToDouble(nextTemperature);
        Progress("Heating machine to target temperature. Please wait...");
        COMMS.skipTemp = false; //this is used by the button in the panel that skips waiting for temperature
        HeatMachineToTargetTemperature(); //this enters a while loop until the temperatue is right.
        Progress("Done Heating machine to target temperature");
      }

      //set presurre to target pressure

      Progress("Setting pressure to target pressure. Please wait...");
      stepCount = 0;
      targetPressure = Convert.ToDouble(Properties.Settings.Default.CollectionPressure[stepCount]);
      currentDuration = Convert.ToDouble(Properties.Settings.Default.CollectionDuration[stepCount]);
      //start pump at one third of targetPressure (as an initial estimate):
      /*int initialPressure = Convert.ToInt32(targetPressure / 4);
      if (initialPressure<10)
      {
        initialPressure = 3;
      }
      Pumps.IncreaseMainPump(initialPressure);*/
      Progress("display current step time=" + (currentDuration).ToString("0.00"));
      
      GoToTargetPressure();
      Progress("Target pressure reached");


      //Hide "please wait" panel
      Progress("hide panel1");
      //Progress("disable stop button"); 

      //Write data to file
      //InitializeTest();
      try
      {
        SR = new StreamWriter(dataFile);       
        dataFilePathForCapRep = dataFile.Substring(0, dataFile.Length - 4)+"-forCapRep.txt";
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



      startTime = Convert.ToDouble(Environment.TickCount); //+ 500.0
      maxPressure *= pConversion;
      maxTestPressure *= pConversion;

      //Main loop

      Progress("Now running");
      string collectedLevelCount;
      bool firstRound = true;
      //Main loop
      while (!abort && !overVolume)
      {
        Progress("hide panel 1");
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
        Progress("display volume levels =" + ReservoirPercent.ToString() + "=" + CollectedPercent.ToString());
        CollectedCount = COMMS.CollectedLevelCount;
        if (firstRound)
        {
          LastCollectedCount = CollectedCount;
          firstRound = false;
        }

        CollectedDifferenceInCounts = -(CollectedCount - LastCollectedCount); //It has to be -ve because penetrometer is inverted (max counts is empty penetrometer)

        //read temperature
        ReadTemperatureAndSetLabel();

        //read current time
        currentTime = Convert.ToDouble(Environment.TickCount) - startTime - stoppedTime;
        outputTime = currentTime / 1000;
        timeDifferenceInMinutes = ((currentTime - lastTime) / 1000) / 60;


        if (!mustNotCountFlowBecausePressureIsAdjusting)
        {
          //(this is so that the first time point after adjusting pressure doesnt count the flow accumulated during that period)

          //Calculate flow
          Flow = CollectedDifferenceInCounts / timeDifferenceInMinutes; //this is flow in pent counts per minute
          Flow = Flow * Convert.ToDouble(Properties.Settings.Default.MaxCapacityInML) / 58000; //this converts flow to mL       
        }


        //keep rough track of how many mL we have pumped.
        double mlsMoved = (pressureRate / 60.0) * outputTime;

        //write data to file and report progress

        SR = new StreamWriter(dataFile, true);
        SR2 = new StreamWriter(dataFilePathForCapRep, true);
        collectedLevelCount = COMMS.CollectedLevelCount.ToString();
        //SR.WriteLine(outputTime.ToString("0.00") + "," + collectedLevelCount + "," + currentTemp.ToString("0.0") + "," + currentPressure.ToString("0.000"));
        //SR.WriteLine(outputTime.ToString("0.00") + "\t" + Flow.ToString("0.000") + "\t" + currentTemp.ToString("0.0") + "\t" + currentPressure.ToString("0.000"));
        double convertedTemp;
        if (Properties.Settings.Default.TempCorF == "C")
        {
          convertedTemp = Math.Round((currentTemp - 32) * 5 / 9);
        }
        else
        {
          convertedTemp = currentTemp;
        }
        SR.WriteLine("{0,10}\t{1,10}\t{2,10}\t{3,10}", outputTime.ToString("0.00"), Flow.ToString("0.00"), convertedTemp.ToString("0.0"), currentPressure.ToString("0.000"));
        //Console.WriteLine("{0,10}\t{1,10}\t{2,10}\t{3,10}", outputTime.ToString("0.00"), Flow.ToString("0.00"), convertedTemp.ToString("0.0"), currentPressure.ToString("0.000"));
        SR2.WriteLine("{0,10}\t{1,10}", Flow.ToString("0.00"), currentPressure.ToString("0.000"));
        SR.Close();
        SR2.Close();
        //Progress("Reading:" + outputTime.ToString("0.00") + "," + collectedLevelCount + "," + convertedTemp.ToString("0.0") + "," + currentPressure.ToString("0.000"));
        Progress("Reading:" + outputTime.ToString("0.00") + "," + Flow.ToString("0.000") + "," + convertedTemp.ToString("0.0") + "," + currentPressure.ToString("0.000"));
        Progress("display stepCount=" + (stepCount + 1).ToString());
        Thread.Sleep(10000);// this is the main sleep of the thread between reading so that it doesn't go too fast.
        if (outputTime / 60 > currentDuration)
        {
          Progress("display current step time=" + (currentDuration).ToString("0.00"));
          //save current time
          startStoppedTime = Convert.ToDouble(Environment.TickCount);
          Progress("Ended Period");
          //set presurre to target pressure
          Progress("Setting pressure to target pressure for next period. Please wait...");
          stepCount++;
          if (stepCount >= Properties.Settings.Default.CollectionPressure.Count)
          {
            Progress("display stepCount=" + " End");
          }
          else
          {
            Progress("display stepCount=" + (stepCount + 1).ToString());
          }



          //end if stepCount is bigger than the number of steps (i.e. periods)
          if (stepCount >= Properties.Settings.Default.CollectionPressure.Count)
          {
            abort = true;
            Progress("end Test");
          }
          else
          {
            Progress("display duration=" + Properties.Settings.Default.CollectionDuration[stepCount]);
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
            mustNotCountFlowBecausePressureIsAdjusting = true;
          }
        }
        else  //if it is not the end of the period...
        {
          mustNotCountFlowBecausePressureIsAdjusting = false;
        }


        //Stop if over max pressure.
        if (outputPressure > p1Max) //add to this if volume is empty or pent is full
        {
          abort = true;
          testPaused = true;
          //stop main pump
          Pumps.SetPump2(0);
          //open relief pressure valve
          //Valves.OpenValve2();
          Progress("disable stop button");
          Progress("Machine has reached it's maximum pressure range! The test will be aborted.");
          Progress("Data saved to " + Properties.Settings.Default.TestData);
          SR = new StreamWriter(dataFile, true);
          SR.WriteLine("Machine has reached it's maximum pressure range! The test was aborted. Pressure was " + outputPressure.ToString() + ", max is " + p1Max.ToString());
          SR.Close();

          MessageBox.Show("Machine has reached it's maximum pressure. The test has been stopped. Data saved to " + Properties.Settings.Default.TestData);
          //COMMS.Instance.Pause(1); //wait 1 second for other thread to finish      
        }
        //Open discharge valve when collected volume gets to 100%
        //Stop if over max volume.
        if (CollectedPercent > Properties.Settings.Default.MaxPent3PercentFull)
        {
          Valves.OpenValve1();
          DischargingCollectedVolume form = new DischargingCollectedVolume();
          form.ShowDialog();
          Valves.CloseValve1();

          /*
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
          */
        }
        //see if we need to pause, or abort.
        //PauseTest();     
        lastTime = currentTime;
        LastCollectedCount = CollectedCount; ;
      }
      if (abort)
      {
        //Progress("Test Stopped");
        System.Diagnostics.Debug.WriteLine("Test aborted successfully becuase this message is sent from BurstTest thread");
      }
      //SR.Close();

      /*
      SR2 = new StreamWriter(dataFilePathForCapRep, true);
      SR2.WriteLine("0,0");
      SR2.Close();*/

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
      SR.WriteLine("Time Units=" + "Seconds");
      SR.WriteLine("Flow Units=" + "mL/min");
      if (Properties.Settings.Default.TempCorF == "C")
      {
        SR.WriteLine("Temperature Units=" + "Celsius");
      }
      else
      {
        SR.WriteLine("Temperature Units=" + "Fahrenheit");
      }
      SR.WriteLine("Pressure Units=" + pUnits);
      SR.WriteLine("Steps=" + Properties.Settings.Default.StepCount);


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
  }
}
