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
  public partial class Temperature : Form
  {
    public Temperature()
    {
      InitializeComponent();
    }
    private double currentTemp;
    private double targetTemp;
    private double athena1Temp; //temp of tank
    private double athena2Temp; //temp of pipes
    private double chamberTemp;

    private void Temperature_Load(object sender, EventArgs e)
    {
      targetTemp = Properties.Settings.Default.selectedTemp;
      label3.Text = targetTemp.ToString();

      //start piping heaters
      COMMS.Instance.SetAthenaTemp(1, targetTemp);
      COMMS.Instance.SetAthenaTemp(2, targetTemp);

      //start selected chamber heater 
      if (Properties.Settings.Default.Chamber == "Ring")
      {
        COMMS.Instance.SetAthenaTemp(3, targetTemp);
      }
      else if (Properties.Settings.Default.Chamber == "Disk")
      {
        COMMS.Instance.SetAthenaTemp(4, targetTemp);
      }
      
      timer1.Enabled=true;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      try
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
        currentTemp = (athena1Temp + athena2Temp+chamberTemp) / 3;
        label4.Text = currentTemp.ToString();
        //Console.WriteLine("athena1Temp " + athena1Temp);
        //Console.WriteLine("chamberTemp " + chamberTemp);
      }
      catch (Exception ex)
      {
        timer1.Enabled = false;
        var result = MessageBox.Show("Problem reading temperature from Athena " , "Error!", MessageBoxButtons.RetryCancel); //+ ex.Message
        if (result == DialogResult.Retry)
        {
          timer1.Enabled = true;
        }
        else
        {
          this.Close();
        }

      }
      //MessageBox.Show("currentTemp " + currentTemp+ "targetTemp " + targetTemp);
      if ((currentTemp>targetTemp -2) && (currentTemp < targetTemp + 2))
      {
        this.Close();
        //MessageBox.Show("temperature has stabilized");
      }
      



    }
  }
}
