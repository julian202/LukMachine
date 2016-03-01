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
  public partial class DataSavedForm : Form
  {
    public DataSavedForm()
    {
      InitializeComponent();
    }

    private void DataSavedForm_Load(object sender, EventArgs e)
    {
      label1.Text = "Data saved to " + Properties.Settings.Default.TestData;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("explorer.exe", System.IO.Directory.GetParent(Properties.Settings.Default.TestData).ToString());
      
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("notepad.exe", System.IO.Directory.GetParent(Properties.Settings.Default.TestData).ToString());
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.Hide();
      Report rep = new Report(true);
      rep.ShowDialog();
      this.Show();
      
    }
  }
}
