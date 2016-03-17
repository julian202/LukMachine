using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LukMachine
{
  public partial class main : Form
  {
    public main()
    {
      InitializeComponent();

    }

    private void button1_Click(object sender, EventArgs e)
    {
      //NotImplemented();
      DialogResult asdf;
      while ((asdf = openCOM()) == DialogResult.Retry)
      {

      }
      if (asdf == DialogResult.Abort)
      {
        return;
      }

      this.Hide();

      Setup setScrn = new Setup();
      DialogResult setupResult = setScrn.ShowDialog();

      if (setupResult != DialogResult.Cancel)
      {
        /* //this is now in burst test
        if (Properties.Settings.Default.useTemperature)
        {
          Temperature temp = new Temperature();
          temp.ShowDialog();
        }*/


        AutoScrn auto = new AutoScrn();
        auto.ShowDialog();

        /*if (Properties.Settings.Default.mustRunReport)
        {
          Report rep = new Report();
          rep.ShowDialog();
        }*/
      }
      this.Show();
    }

    private DialogResult openCOM()
    {
      DialogResult asdf = DialogResult.OK;
      bool openIt = COMMS.Instance.OpenPort(Properties.Settings.Default.COMM);
      COMMS.Instance.Pause(.5);
      if (!openIt)
      {
        asdf = MessageBox.Show("Error opening COM port: " + Properties.Settings.Default.COMM + " Please check your COM port settings. Press 'Ignore' to ignore this error and enter demo mode.", "Burst Tester", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        if (asdf == DialogResult.Ignore)
        {
          Properties.Settings.Default.COMM = "Demo";
          Properties.Settings.Default.Save();
        }
        else if (asdf == DialogResult.Abort)
        {
          return asdf;
        }
        else
        {
          return DialogResult.Retry;
        }
      }
      //COMMS.Instance.Send("ER" + Properties.Settings.Default.skipReadings.ToString());
      //COMMS.Instance.Send("EI" + Properties.Settings.Default.averageReadings.ToString());
      return DialogResult.OK;
    }

    private void NotImplemented()
    {
      MessageBox.Show("Feature not yet implemented!", COMMS.Instance.appName, MessageBoxButtons.OK, MessageBoxIcon.Information);

    }

    private void main_Load(object sender, EventArgs e)
    {
      Properties.Settings.Default.ground = COMMS.Instance.getGround();
      button2.Select();
      stopPumpCloseValves();
      //this.Text = String.Format("PMI Liquid Permeability [Luk] Version {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

      if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
      {
        this.Text = String.Format("PMI Liquid Permeability [Luk] Version {0}", System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
      }


    }

    private void button2_Click(object sender, EventArgs e)
    {
      bool success;
      success = COMMS.Instance.OpenPort(Properties.Settings.Default.COMM);
      if (success)
      {
        Manual manctrl = new Manual();
        Hide();
        manctrl.ShowDialog();
        Show();
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      settings setScrn = new settings();
      Hide();
      setScrn.ShowDialog();
      Show();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      this.Hide();
      Report rep = new Report();
      rep.ShowDialog();
      this.Show();
    }

    private void main_FormClosed(object sender, FormClosedEventArgs e)
    {
      stopPumpCloseValves();
    }


    public void stopPumpCloseValves() //this looks like it's not working
    {
      /*
      //stop main pump
      Pumps.SetPump2(0);
      //stop refill pump
      COMMS.Instance.MoveMotorValve(1, "S");  
      //close all 3 valves?
      
      //close drain valve
      Valves.CloseValve3();
      //close relief pressure valve
      Valves.CloseValve2();
      //close pent valve so that it wont drain
      Valves.CloseValve1();
*/
    }

    private void button5_Click(object sender, EventArgs e)
    {
      DialogResult asdf;
      while ((asdf = openCOM()) == DialogResult.Retry)
      {

      }
      if (asdf == DialogResult.Abort)
      {
        return;
      }
      //Console.WriteLine ( COMMS.Instance.rsEcho("jk"));
    }
  }
}
