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
    public delegate void ProgressEventHandler(string message);
    BurstTest test;
    private Thread testThread;

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
        if (message.Contains("Reading:"))
        {
          // split the reading string into usable doubles
          // for the graph display
          string[] splitString = message.Split(':');
          string[] splitString2 = splitString[1].Split(',');
          string X = splitString2[1];
          string Y = splitString2[0];
          chart1.Series["Series1"].Points.AddXY(Y, X);
          try
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
          }
          
          dataGridView1.Rows.Add(Y.ToString(), X.ToString());
          System.Diagnostics.Debug.WriteLine("X: " + X.ToString() + " Y: " + Y.ToString());
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
      }
    }
    private void AutoScrn_Load(object sender, EventArgs e)
    {
      Text = "Auto test [" + Properties.Settings.Default.TestSampleID + "]";
      if (Properties.Settings.Default.promptSafety)
      {
        MessageBox.Show("Please verify that your sample is centered and that the clamping mechanism is safely securing the sample.");
      }
      chart1.ChartAreas[0].AxisY.Title = "Volume (mL)";
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
      button2.Enabled = false;
      test.AbortTest();
      //testThread.Abort();
      testThread.Join();
      //stop main pump
      //Progress("Stopping pump...");
      COMMS.Instance.ZeroRegulator(1);
      //pumpRunning = false;

      //open relief pressure valve
      //Progress("Relieving pressure...");
      COMMS.Instance.MoveValve(2, "O");
      
    }

    private void button3_Click(object sender, EventArgs e)
    {
      Manual manScrn = new Manual();
      this.Hide();
      manScrn.ShowDialog();
      this.Show();
    }
  }
}
