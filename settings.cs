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
  public partial class settings : Form
  {
    public settings()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {

      Properties.Settings.Default.LowPumpSetting = textBox1.Text;
      Properties.Settings.Default.MediumPumpSetting = textBox2.Text;
      Properties.Settings.Default.HighPumpSetting = textBox3.Text;




      System.Collections.Specialized.StringCollection pressures = new System.Collections.Specialized.StringCollection();
      System.Collections.Specialized.StringCollection fluids = new System.Collections.Specialized.StringCollection();

      //look at each item and see if there is something there to save
      foreach (DataGridViewRow dRow in dataGridView2.Rows)
      {
        try
        {
          if (dRow.Cells[0].Value.ToString() != "" && dRow.Cells[1].Value.ToString() != "")
            pressures.Add(dRow.Cells[0].Value.ToString() + ":" + dRow.Cells[1].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
          //show error and return
          //MessageBox.Show("Invalid entry for Pressure Unit on line " + (dRow.Index + 1).ToString() + "!", COMMS.Instance.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
          //return;
        }
      }
      //make sure we save any good values user has entered here too...
      foreach (DataGridViewRow dRow in dataGridView3.Rows)
      {
        try
        {
          if (dRow.Cells[0].Value.ToString() != "" && dRow.Cells[1].Value.ToString() != "")
            fluids.Add(dRow.Cells[0].Value.ToString() + ":" + dRow.Cells[1].Value.ToString());
        }
        catch (NullReferenceException ex)
        {
          //show error and return
          //MessageBox.Show("Invalid entry for Fluid/viscosity on line " + (dRow.Index + 1).ToString() + "!", COMMS.Instance.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
          //return;
        }
      }

      Properties.Settings.Default.pressureUnits = pressures;
      Properties.Settings.Default.fluids = fluids;
      Properties.Settings.Default.COMM = comboBox1.Text;
      Properties.Settings.Default.Save();
      Close();
    }

    private void settings_Load(object sender, EventArgs e)
    {

      textBox1.Text = Properties.Settings.Default.LowPumpSetting;
      textBox2.Text = Properties.Settings.Default.MediumPumpSetting;
      textBox3.Text = Properties.Settings.Default.HighPumpSetting;

      label6.Text = "Software Version: " + COMMS.Instance.version;
      //add a selection for demo mode
      comboBox1.Items.Add("Demo");
      foreach (string port in COMMS.Instance.GetAvailablePorts())
      {
        //iterate through available ports and add them for selection.
        comboBox1.Items.Add(port);
        //if we have found our last selected port, set it as the selected item.
        if (port == Properties.Settings.Default.COMM)
        {
          comboBox1.SelectedIndex = comboBox1.FindStringExact(port);
        }
      }

      //load settings for pressure units
      foreach (string s in Properties.Settings.Default.pressureUnits)
      {
        string[] split = s.Split(':');
        dataGridView2.Rows.Add(split[0], split[1]);
      }
      //load fluids & viscosities
      foreach (string s in Properties.Settings.Default.fluids)
      {
        string[] split = s.Split(':');
        dataGridView3.Rows.Add(split[0], split[1]);
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      //reset to default settings, need to ask the user, "Fo Realz bro?"
      Properties.Settings.Default.Reset();
    }
  }
}
