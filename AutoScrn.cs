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
    public AutoScrn()
    {
      InitializeComponent();
      backgroundWorkerMainLoop.WorkerReportsProgress = true;
      backgroundWorkerMainLoop.WorkerSupportsCancellation = true;
    }
    private void AutoScrn_Load(object sender, EventArgs e)
    {
      if (backgroundWorkerMainLoop.IsBusy != true)
      {
        backgroundWorkerMainLoop.RunWorkerAsync();
      }
    }
    private void buttonReport_Click(object sender, EventArgs e) //this should be called automatically at end of test
    {
      this.Hide();
      Report rep = new Report(true);
      rep.ShowDialog();
      this.Show();
    }

    private void linkLabelOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("explorer.exe", System.IO.Directory.GetParent(Properties.Settings.Default.TestData).ToString());

    }

    private void buttonSkipSettingTemp_Click(object sender, EventArgs e)
    {

    }

    private void buttonSkipPressure_Click(object sender, EventArgs e)
    {

    }

    private void backgroundWorkerMainLoop_DoWork(object sender, DoWorkEventArgs e)
    {

    }


  }
}
