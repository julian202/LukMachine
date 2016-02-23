using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Windows.Forms;

//Update to use singleton pattern. 2-21-2014 
//PMI Communications by Andy Williams & Ben Ink - 12/20/2013


namespace LukMachine
{
  public sealed class COMMS
  {
    private static readonly COMMS instance = new COMMS();

    private COMMS()
    {
    }
    public static COMMS Instance
    {
      get
      {
        return instance;
      }
    }

    //declare a new serial port object to be used throughout class.
    SerialPort _serialPort = new SerialPort("something", 115200, Parity.None, 8, StopBits.One);

    public bool demoMode = false;
    public string comPort = null;
    public string version = "1.0.0";
    public string appName = "PMI Software";
    public string currentGroupName = "Default";
    public bool connected = false; //all comm code should look at this value to make sure communication is established.
    public Boolean commBusy; // if comms are busy, come back later.
    public int[] RegulatorPosition = new int[4];
    public bool closeDialog = false;
    public bool loadReport = false;
    string[] valvePosition = new string[24]; // to remember valve position
    public string inputReturn = null;
    public bool continueExport = false;

    // SEND, SEND/RECEIVE, OPEN PORT, CLOSE PORT, LIST PORTS.
    #region COMM Controls

    public Boolean OpenPort(String portNum)
    {
      //open up a new port.

      //set the comport number to a string variable
      //String thePort = "COM1"
      //create the new serial port object
      if (connected)
      {
        ClosePort();
      }
      if (portNum == "Demo")
      {
        connected = true;
        demoMode = true;
        System.Diagnostics.Debug.WriteLine("Demo Mode Started");
        comPort = portNum;
        return true;
      }
      try
      {
        _serialPort.PortName = portNum;
        //if the port ain't open it, open it.
        if (!(_serialPort.IsOpen))
          _serialPort.Open();
        commBusy = false;
        System.Diagnostics.Debug.Write("COMM Port: " + portNum + " Opened");
        comPort = portNum;
        connected = true;
        demoMode = false;
        return true;
      }
      catch (Exception ex)
      {
        //I'm sorry Davey, you can't do that.
        System.Diagnostics.Debug.Write("COMM Port: " + portNum + " ERROR Opening");
        MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
        commBusy = false;
        connected = false;
        return false;
      }


    }

    public void ClosePort()
    {
      try
      {
        //if the port ain't open it, then we're all good..
        //else, close it.
        if (_serialPort.IsOpen)
          _serialPort.Close();
        System.Diagnostics.Debug.Write("COMM Port Closed!");
      }
      catch (Exception ex)
      {
        //I'm sorry Davey, you can't do that.
        System.Diagnostics.Debug.Write("Error closing COMM Port?  How can we have an error closing the COMM port?  WTF?");
        MessageBox.Show("Error closing the COMM port!! :: " + ex.Message, "Error!");
      }
    }

    public void Send(String toSend)
    {
      string received;
      Char newLineChar = (char)13;
      string newLine = (newLineChar.ToString());

      if (!connected || demoMode)
      {
        return;
      }
      //boolean to say comms are busy.  Will be used in que list.
      commBusy = true;
      try
      {
        //if the port ain't open, open it.
        //if (!(_serialPort.IsOpen))
        //{
        //    _serialPort.Open();

        //}

        //clear input and output buffer first. 
        _serialPort.DiscardInBuffer();
        _serialPort.DiscardOutBuffer();

        //send out string and echo it to console
        //for troubleshooting
        _serialPort.Write(toSend);
        //get the first respose, which will be the echo command
        //character
        received = _serialPort.ReadTo(newLine);
        System.Diagnostics.Debug.Write("Echo: " + received + Environment.NewLine);

        System.Diagnostics.Debug.Write("Send Command: " + toSend + Environment.NewLine);
      }
      catch (IndexOutOfRangeException kj)
      {
        ClosePort();
        OpenPort("Demo");
        DialogResult stuff = MessageBox.Show("Error: " + kj.Message + Environment.NewLine + "Demo mode started.", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

        //return;
      }
      catch (InvalidOperationException qT)
      {
        //ERROR HERE
        ClosePort();
        OpenPort("Demo");
        DialogResult stuff = MessageBox.Show("Error: " + qT.Message + Environment.NewLine + "Demo mode started.", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

        //return;
      }
      catch (IOException ex)
      {
        ClosePort();
        OpenPort("Demo");
        DialogResult stuff = MessageBox.Show("Error: " + ex.Message + Environment.NewLine + "Demo mode started.", Application.ProductName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

        //return;
      }
      commBusy = false;
    }

    public string[] GetAvailablePorts()
    {
      //return all available serial ports on computer

      //load all available into an array
      string[] ports = SerialPort.GetPortNames();
      //return string array to caller
      return ports;
    }

    public string rsEcho(string toSend)
    {
      //set up needed variables
      string received;
      Char newLineChar = (char)13;
      string newLine = (newLineChar.ToString());
      if (!connected || demoMode)
      {
        return null;
      }
      //send the initial command out
      Send(toSend);
      //if we're not connected to a machine, Send can handle it's own error, we just need
      //to clean up an error that would come from waiting for a response.

      //next response should be the value we need
      try
      {
        //received = _serialPort.ReadExisting();
        received = _serialPort.ReadTo(newLine);
        System.Diagnostics.Debug.Write("Return Value: " + received + Environment.NewLine);
        return received;
      }
      catch (IOException ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return null;
      }
      catch (InvalidOperationException ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return null;
      }
    }

    public void PassThrough(char channel, string toSend)
    {
      //pass characters through to specified COM port. (BCAD)
      //receive no response.
      string chrs = toSend.Length.ToString("00");
      COMMS.Instance.Send(">" + channel + chrs + toSend);
      Output(">" + channel + chrs + toSend);
    }

    #endregion

    // MISC UTILITIES. CONVERSIONS, PAUSE, 
    #region UTILITIES

    //public 
    public void Pause(double seconds)
    {
      //convert double to int, this allows us any range from .0001 to whole seconds.
      //multiply by 1000 to get milliseconds.
      int milliSeconds = Convert.ToInt32(seconds) * 1000;
      try
      {
        Thread.Sleep(milliSeconds);
      }
      catch (System.Threading.ThreadInterruptedException ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
      }

    }

    public double ConvertCountsToX(double countsRead, double minCounts, double maxValue, double countsSpan)
    {
      //convert counts to any value such as PSI or %. 
      //countsRead: count value returned from rabbit.
      //minCounts: minimum count value possible
      //countsSpan: range of counts (max - min)
      //maxValue: the maximum value that would represent 
      double X = (countsRead - minCounts) * maxValue / countsSpan;
      return X;
    }

    private double GetFlow(double lastPressure, double currentPressure, double currentTime, double lastTime, double volume)
    {
      //calculate flow from pressure time.
      double dpdt = (lastPressure - currentPressure) / (currentTime - lastTime);
      double holdFlow = (volume / currentPressure) * (dpdt * 1000);

      return holdFlow;
    }

    private double GetNormalPerm(double currentPressure, double lastPressure, double currentTime, double lastTime, double diameter, double volume)
    {
      //calculate permeaility by area & pressure
      double holdFlow = GetFlow(lastPressure, currentPressure, currentTime, lastTime, volume);

      double holdAP = holdFlow / ((currentPressure * Math.PI) * ((diameter * diameter) * 250));

      return holdAP;
    }

    private double GetDarcyPerm(double thickness, double diameter, double pressure, double atm, double lastPressure, double currentPressure, double lastTime, double currentTime, double volume)
    {
      //calculate permeability using darcy method.
      double flow = GetFlow(lastPressure, currentPressure, currentTime, lastTime, volume);
      double darcy = (8 * flow * thickness * 0.0185 * (1 / Math.PI) / (diameter / diameter) / ((pressure / atm) * (pressure / atm - 1)));

      return darcy;
    }

    public void Output(string s)
    {
      System.Diagnostics.Debug.Write(s);
    }
    #endregion

    // CONTROLS FOR DEVICES IN PMI MACHINES
    #region Hardware Controls

    public string ReadPressureGauge(int channel)
    {
      //convert channel number to an ascii code
      int realChan = channel + 64;
      //convert ascii code to a character
      Char chanName = (char)realChan;
      //send R command and gauge channel, return to a string
      string returnValue = rsEcho("R" + chanName.ToString());
      //double toPSI = countsToPsi(returnValue, maxPressure);
      //return toPSI.ToString("F2");
      return returnValue;
    }

    public void MoveValve(int valveNum, string vPos)
    {
      //no 0 offset any longer.  Valve 1 = A and so on.

      //convert valve number to an ascii code
      int realValve = valveNum + 64;
      //convert ascii code to a character
      Char valveName = (char)realValve;
      //append ascii code to valve position
      //ie. open valve 1 would be "OA"
      //MessageBox.Show(vPos + valveName.ToString());
      Send(vPos + valveName.ToString());
      //keep track of valve position.
      valvePosition[valveNum] = vPos;
    }

    public string GetValvePosition(int vNum)
    {
      int vCharNum = 64 + vNum;
      char vChar = (char)vCharNum;
      string pos = rsEcho("V" + vChar.ToString());
      return pos;
    }
    public void MoveMotorValve(int valveNum, string vPos)
    {
      string command = vPos + valveNum.ToString();
      Send(command);
    }

    public Int32 getReservoirLevelPercent()
    {
      int minCount = Properties.Settings.Default.MinReservoirCount;
      int maxCount = Properties.Settings.Default.MaxReservoirCount;
      return (Convert.ToInt32(getReservoirLevelCount())-minCount)/(maxCount- minCount);
    }
    public Int32 getCollectedLevelPercent()
    {
      int minCount = Properties.Settings.Default.MinCollectedCount;
      int maxCount = Properties.Settings.Default.MaxCollectedCount;
      return (Convert.ToInt32(getCollectedLevelCount()) - minCount) / (maxCount - minCount);
    }

    public string getReservoirLevelCount()
    {
      return COMMS.Instance.MotorValvePosition(1);
    }
    public string getCollectedLevelCount()
    {
      return COMMS.Instance.MotorValvePosition(2);
    }

    public string MotorValvePosition(int valveNum)
    {
      char valveChannel = 'Z';
      string response;
      System.Diagnostics.Debug.Write("Valve Number: " + valveNum.ToString() + Environment.NewLine);
      if (valveNum == 1 || valveNum == 2 || valveNum == 3 || valveNum == 4)
      {
        switch (valveNum)
        {
          case 1:
            valveChannel = (char)81;
            break;
          case 2:
            valveChannel = (char)84;
            break;
          case 3:
            valveChannel = (char)87;
            break;
          case 4:
            valveChannel = (char)90;
            break;
        }
        response = rsEcho("R" + valveChannel.ToString());
        return response;
      }
      return null;
    }

    public bool BackPressureToPosition(int channel, int pos)
    {
      //move motor driven backpressure regulator to position.

      //get current position
      int currentPosition = Convert.ToInt32(MotorValvePosition(channel));
      if (currentPosition > pos)
      {
        //close to position
        MoveMotorValve(channel, "C");
        while (currentPosition > pos)
        {
          currentPosition = Convert.ToInt32(MotorValvePosition(channel));
        }
        //stop
        MoveMotorValve(1, "S");
      }
      else if (currentPosition < pos)
      {
        //open to pos
        MoveMotorValve(1, "O");
        while (currentPosition < pos)
        {
          currentPosition = Convert.ToInt32(MotorValvePosition(1));
        }
        //stop
        MoveMotorValve(1, "S");
      }

      return true;
    }

    public string MotorValveCloseLimit(int valveNum)
    {

      char valveChannel = 'Z';
      string response;
      System.Diagnostics.Debug.Write("Valve Number: " + valveNum.ToString() + Environment.NewLine);
      if (valveNum == 1 || valveNum == 2 || valveNum == 3 || valveNum == 4)
      {
        switch (valveNum)
        {
          case 1:
            valveChannel = (char)83;
            break;
          case 2:
            valveChannel = (char)86;
            break;
          case 3:
            valveChannel = (char)89;
            break;
          case 4:
            valveChannel = (char)92;
            break;
        }
        response = rsEcho("R" + valveChannel.ToString());
        return response;
      }
      return null;
    }

    public string MotorValveOpenLimit(int valveNum)
    {
      char valveChannel = 'Z';
      string response;
      System.Diagnostics.Debug.Write("Valve Number: " + valveNum.ToString() + Environment.NewLine);
      if (valveNum == 1 || valveNum == 2 || valveNum == 3 || valveNum == 4)
      {
        switch (valveNum)
        {
          case 1:
            valveChannel = (char)82;
            break;
          case 2:
            valveChannel = (char)85;
            break;
          case 3:
            valveChannel = (char)88;
            break;
          case 4:
            valveChannel = (char)91;
            break;
        }
        response = rsEcho("R" + valveChannel.ToString());
        return response;
      }
      return null;
    }

    public void SetAthenaTemp(int channel, double temp)
    {
      double changedTemp = temp * 10;
      string finalTemp = changedTemp.ToString("0000");

      Send("TS" + channel.ToString() + finalTemp);
      //MessageBox.Show("TS" + channel.ToString() + finalTemp);

    }

    public double ReadAthenaTemp(int channel)
    {
      string returnValue = rsEcho("TT" + channel.ToString());
      double fixReturn = double.Parse(returnValue) / 10;

      return fixReturn;
    }

    public void MoveGenerator(int genChannel, int speed)
    {
      //gen channel is channel for machines with more than one
      //pressure generator.

      int absoluteSpeed = Math.Abs(speed);
      string speedFormat = absoluteSpeed.ToString("0000");

      if (speed > 0)
      {
        // Send foreword command with speed
        Send("GF" + genChannel.ToString() + speedFormat);
        //System.Diagnostics.Debug.Write("F" + genChannel.ToString() + speedFormat + Environment.NewLine);
      }
      if (speed < 0)
      {
        //send reverse command with speed
        Send("GB" + genChannel.ToString() + speedFormat);
        //System.Diagnostics.Debug.Write("B" + genChannel.ToString() + speedFormat + Environment.NewLine);
      }
      if (speed == 0)
      {
        //send stop command.
        Send("GS" + genChannel.ToString());
        //System.Diagnostics.Debug.Write("L" + genChannel.ToString() + Environment.NewLine);
      }
    }

    public string GetGeneratorLimit(int genChannel)
    {
      //genChannel is used for systems with multiple generators.  
      //in one generator system 
      //genChannel is used for systems with multiple generators.  
      //in one generator system 
      string asdf;
      string asdf2;
      try
      {
        if (genChannel == 1)
        {
          asdf = rsEcho("RL");
          asdf2 = rsEcho("RM");

          if (int.Parse(asdf) < 10000)
          {
            System.Diagnostics.Debug.Write("Gen: 1 upper limit counts: " + asdf + Environment.NewLine);
            return "U";
          }
          if (int.Parse(asdf2) < 10000)
          {
            System.Diagnostics.Debug.Write("Gen: 1 lower limit counts: " + asdf2 + Environment.NewLine);
            return "D";
          }
        }
        if (genChannel == 2)
        {
          asdf = rsEcho("RN");
          asdf2 = rsEcho("RO");
          if (int.Parse(asdf) < 10000)
          {
            System.Diagnostics.Debug.Write("Gen: 2 Limit counts: " + asdf + Environment.NewLine);
            return "U";
          }
          if (int.Parse(asdf2) < 10000)
          {
            System.Diagnostics.Debug.Write("Gen: 2 Limit counts: " + asdf2 + Environment.NewLine);
            return "D";
          }
        }
        if (genChannel == 3)
        {
          asdf = rsEcho("RP");
          asdf2 = rsEcho("R]");
          if (int.Parse(asdf) < 10000)
          {
            System.Diagnostics.Debug.Write("Gen: 3 Limit counts: " + asdf + Environment.NewLine);
            return "U";
          }
          else if (int.Parse(asdf2) < 10000)
          {
            System.Diagnostics.Debug.Write("Gen: 3 Limit counts: " + asdf2 + Environment.NewLine);
            return "D";
          }
        }
        System.Diagnostics.Debug.Write("Not at a limit" + Environment.NewLine);
        return "0";
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return "0";
      }
    }

    public bool GenUpLimit(int genChannel)
    {
      //returns true if generator has hit up limit.
      string returnValue;

      if (genChannel == 1)
      {
        returnValue = rsEcho("RL");
        if (int.Parse(returnValue) < 10000)
        {
          return false;
        }
      }
      if (genChannel == 2)
      {
        returnValue = rsEcho("RN");
        if (int.Parse(returnValue) < 10000)
        {
          return true;
        }
      }
      if (genChannel == 3)
      {
        returnValue = rsEcho("RP");
        if (int.Parse(returnValue) < 10000)
        {
          return true;
        }
      }
      return false;

    }

    public bool GenDownLimit(int genChannel)
    {
      if (!connected || demoMode)
      {
        return false;
      }
      //returns true if generator has reached down limit.
      string returnValue;

      if (genChannel == 1)
      {
        returnValue = rsEcho("RM");
        if (int.Parse(returnValue) < 10000)
        {
          return false;
        }
      }
      if (genChannel == 2)
      {
        returnValue = rsEcho("RO");
        if (int.Parse(returnValue) < 10000)
        {
          return true;
        }
      }
      if (genChannel == 3)
      {
        returnValue = rsEcho("R]");
        if (int.Parse(returnValue) < 10000)
        {
          return true;
        }
      }
      return false;
    }

    public void SetRegulator(int channel, int amount)
    {
      RegulatorPosition[channel - 1] = amount;
      Send("A" + channel.ToString() + amount.ToString("0000"));
    }

    public void IncreaseRegulator(int channel, int amount)
    {

      if (RegulatorPosition[channel - 1] + amount > 4000)
      {
        amount = 4000 - RegulatorPosition[channel - 1];
      }
      if (RegulatorPosition[channel - 1] >= 4000)
      {
        return;
      }
      amount = Math.Abs(amount) + RegulatorPosition[channel - 1];

      // MessageBox.Show("A" + channel.ToString() + amount.ToString("0000"));
      Send("A" + channel.ToString() + amount.ToString("0000"));
      RegulatorPosition[channel - 1] = amount;
      //MessageBox.Show(Convert.ToString(RegulatorPosition[channel - 1]));
    }

    public void DecreaseRegulator(int channel, int amount)
    {
      amount = Math.Abs(amount);
      if (amount > RegulatorPosition[channel - 1])
      {
        Send("A" + channel.ToString() + "0000");
        RegulatorPosition[channel - 1] = 0;
      }
      else
      {
        int newValue = RegulatorPosition[channel - 1] - amount;
        Send("A" + channel.ToString() + newValue.ToString("0000"));
        RegulatorPosition[channel - 1] = newValue;
      }
    }

    public void ZeroRegulator(int channel)
    {
      Send("A" + channel.ToString() + "0000");
      RegulatorPosition[channel - 1] = 0;
    }

    public int getRegulatorCounts(int channel)
    {
      return RegulatorPosition[channel - 1];
    }

    public int getGround()
    {
      char channel = (Char)95;
      string asdf = rsEcho("R" + channel.ToString());

      try
      {
        return Convert.ToInt32(asdf);
      }
      catch (FormatException ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return 0;
      }
    }

    public int get2v()
    {
      char channel = (Char)96;
      string asdf = rsEcho("R`");
      try
      {
        return Convert.ToInt32(asdf);
      }
      catch (FormatException ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return 0;
      }
    }

    public void StartPump(char channel, int speed)
    {

      PassThrough('C', "#FL" + speed + (char)13);
      PassThrough(channel, "#RU" + (char)13);
    }

    public void StopPump(char channel)
    {
      PassThrough(channel, "#ST" + (char)13);
    }

    public double ReadScale(int scale)
    {
      string b = rsEcho("MR" + scale.ToString());
      try
      {
        double d = Convert.ToDouble(b);
        return d;
      }
      catch (FormatException ex)
      {
        Output("Error in response from scale " + ex.Message);
        return 0.0;
      }
    }

    public void ZeroScale(int scale)
    {
      COMMS.instance.Send("MZ" + scale.ToString());
    }
    #endregion
  }
}


