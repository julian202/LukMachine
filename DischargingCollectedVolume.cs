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
  public partial class DischargingCollectedVolume : Form
  {
    public DischargingCollectedVolume()
    {
      InitializeComponent();
    }

    private void DischargingCollectedVolume_Load(object sender, EventArgs e)
    {

    }
    int CollectedPercent;
    private void timer1_Tick(object sender, EventArgs e)
    {
      CollectedPercent = COMMS.Instance.getCollectedLevelPercent();
      labelFormPercent.Text = CollectedPercent + "%";
      if (CollectedPercent < 5)
      {
        Close();
      }

    }
  }
}
