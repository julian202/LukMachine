using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace LukMachine
{
  class BurstTest
  {
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
      Progress("Preparing to cancel the test, please wait...");
      //stop pump
      //
      testPaused = false;
      abort = true;
      //COMMS.Instance.Pause(2);
      Progress("Stopping pump...");
      PassThrough('A', stopPumpCMD);
      pumpRunning = false;
      Progress("relieving pressure...");
      COMMS.Instance.MoveMotorValve(1, "O");
      COMMS.Instance.Pause(15);
      COMMS.Instance.MoveMotorValve(1, "S");
      Progress("resetting machine...");
      COMMS.Instance.Pause(2);
      Progress("closing data file...");
      testPaused = false;
      Progress("Ending test process");
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
          PassThrough('A', runPumpCMD);
          pumpRunning = true;
        }
      }
    }

    public void ResumeTest()
    {
      testPaused = false;
    }

    public void RunBurstTest()
    {
      //open manifold valves and run main pump
      if (Properties.Settings.Default.SelectedFlowRate == "Low")
      {
        COMMS.Instance.MoveValve(4, "O");
        COMMS.Instance.MoveValve(5, "C");
        COMMS.Instance.MoveValve(6, "C");
        //run main pump at low setting
        COMMS.Instance.SetRegulator(1, (Convert.ToInt32(Properties.Settings.Default.LowPumpSetting))*4000/100);  //2600 is 1/3 of 4000 
        //System.Windows.Forms.MessageBox.Show(((Convert.ToInt32(Properties.Settings.Default.LowPumpSetting)) * 4000 / 100).ToString());
      }
      if (Properties.Settings.Default.SelectedFlowRate == "Medium")
      {
        COMMS.Instance.MoveValve(4, "C");
        COMMS.Instance.MoveValve(5, "O");
        COMMS.Instance.MoveValve(6, "C");
        //run main pump at medium setting
        COMMS.Instance.SetRegulator(1, (Convert.ToInt32(Properties.Settings.Default.MediumPumpSetting)) * 4000 / 100);  //2600 is 2/3 of 4000
      }
      if (Properties.Settings.Default.SelectedFlowRate == "High")
      {
        COMMS.Instance.MoveValve(4, "C");
        COMMS.Instance.MoveValve(5, "C");
        COMMS.Instance.MoveValve(6, "O");
        //run main pump at high setting
        COMMS.Instance.SetRegulator(1, (Convert.ToInt32(Properties.Settings.Default.HighPumpSetting)) * 4000 / 100);
      }


      //open stream
      SR = new StreamWriter(dataFile);
      InitializeTest();
      //Write out header
      WriteHeader();
      bool overVolume = false;
      bool hasBurst = false; bool overPressure = false; double currentPressure; double lastPressure = 0; double startTime = 0;
      double currentTime = 0; double outputTime = 0; double outputPressure = 0; string counts = ""; double realCounts = 0;
      //convert pressure rate to pump speed
      double pumpSpeed = pressureRate * 100;
      //max pump speed = 9999
      if (pumpSpeed > 9999) pumpSpeed = 9999;
      //string it.
      string fixPumpSpeed = pumpSpeed.ToString("0000");
      //send it.
      PassThrough('A', "#FL" + fixPumpSpeed + (char)13);
      pumpRunning = true;
      //start pump
      PassThrough('A', runPumpCMD);
      Progress("Building pressure under sample...");
      counts = COMMS.Instance.ReadPressureGauge(1);
      realCounts = Convert.ToDouble(counts);
      currentPressure = (realCounts - ground) * Properties.Settings.Default.p1Max / 62000; //pconv

      startTime = Convert.ToDouble(Environment.TickCount) + 500.0;
      maxPressure *= pConversion;
      maxTestPressure *= pConversion;
      while (!abort && !overPressure && !hasBurst && !overPressure && !overVolume)
      {

        counts = COMMS.Instance.ReadPressureGauge(1);
        realCounts = Convert.ToDouble(counts);
        currentPressure = (realCounts - ground) * Properties.Settings.Default.p1Max / 62000;

        outputPressure = currentPressure * pConversion;
        currentTime = Convert.ToDouble(Environment.TickCount) - startTime;
        outputTime = currentTime / 1000;

        //keep rough track of how many mL we have pumped.
        double mlsMoved = (pressureRate / 60.0) * outputTime;


        if (outputPressure != lastPressure && outputPressure >= 0)
        {
          if (Properties.Settings.Default.SubtractExpansion)
          {
            //if they have selected 
            outputPressure -= (Properties.Settings.Default.ExpansionPressure * pConversion);
          }
          Progress("Reading:" + outputPressure.ToString("###.000") + "," + outputTime.ToString("#.000"));
          SR.WriteLine(outputTime.ToString("#.000") + "," + outputPressure.ToString("###.000"));
        }

        //if this, then it burst
        if (lastPressure > 0)
        {
          //Progress("Entered first statement, lastPressure > 0");
          double p = outputPressure - lastPressure;
          //Progress(p.ToString() + " >? " +  dropPressure.ToString());
          if ((lastPressure - outputPressure) >= dropPressure)
          {
            PassThrough('A', stopPumpCMD);
            hasBurst = true;
            Progress("Sample burst detected.");
            COMMS.Instance.MoveMotorValve(1, "O");
            COMMS.Instance.Pause(10);
            COMMS.Instance.MoveMotorValve(1, "C");
            Progress("B=" + lastPressure.ToString("#.000"));
            SR.WriteLine("");
            SR.WriteLine("Burst Pressure=" + lastPressure.ToString("#.000"));
            SR.WriteLine("Burst Volume=" + mlsMoved.ToString("#.0000"));
            SR.Close();
          }
        }
        lastPressure = outputPressure;
        //if this then it is over pressure.
        if ((outputPressure >= maxTestPressure || outputPressure >= maxPressure || mlsMoved >= Properties.Settings.Default.MaxPumpVolume) && !hasBurst)
        {
          //send stop pump command.
          PassThrough('A', stopPumpCMD);
          //open motor valve to relieve pressure
          COMMS.Instance.MoveMotorValve(1, "O");
          //over max test pressure
          if (outputPressure >= maxTestPressure)
          {
            overPressure = true;
            Progress("Machine has reached maximum test pressure! The test will be aborted.");
            Progress("current Pressure: " + outputPressure.ToString() + " Max test pressure: " + maxTestPressure.ToString());
            SR.WriteLine("Machine has reached maximum test pressure! The test was aborted.");
            SR.Close();
            abort = true;
            testPaused = true;

          }
          //over safe operating pressure
          if (outputPressure >= maxPressure && !hasBurst)
          {
            overPressure = true;
            Progress("Machine has reached it's maximum safe pressure range! The test will be aborted.");
            SR.WriteLine("Machine has reached it's maximum safe pressure range! The test was aborted.");
            SR.Close();
            abort = true;
            testPaused = true;

          }
          if (mlsMoved >= Properties.Settings.Default.MaxPumpVolume)
          {
            overVolume = true;
            Progress("Machine has reached it's maximum safe volume range, the test will be aborted.");
            SR.WriteLine("Machine has reached it's maximum safe volume range, the test was aborted.");
            SR.Close();
            abort = true;
            testPaused = true;
          }
        }
        //see if we need to pause, or abort.
        PauseTest();
      }
      if (abort)
      {
        Progress("Test Aborted");
      }
      SR.Close();
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
      SR.WriteLine("Burst Pressure Analysis");
      SR.WriteLine("");
      SR.WriteLine("Sample Info");
      SR.WriteLine("");
      SR.WriteLine("Sample ID=" + sampleID);
      SR.WriteLine("Lot Number=" + lotNumber);
      SR.WriteLine("Paper Sample=" + paper);
      SR.WriteLine("Sheets=" + sheets);
      SR.WriteLine("Grammage=" + grammage);
      SR.WriteLine("");
      SR.WriteLine("Test Details");
      SR.WriteLine("");
      SR.WriteLine(DateTime.Now.ToString("dd/MM/yyyy H:mm"));
      SR.WriteLine("Pressure Units=" + pUnits);
      SR.WriteLine("Pressure Rate(mL / min)=" + pressureRate.ToString());
      SR.WriteLine("");
      SR.WriteLine("Data");
      SR.WriteLine("");
    }
  }
}
