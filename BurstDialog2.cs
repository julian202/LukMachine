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
    
    public partial class BurstDialog2 : Form
    {
        private double burstPressure;
        public BurstDialog2(double burst)
        {
            burstPressure = burst;
            
            InitializeComponent();
        }

        private void BurstDialog2_Load(object sender, EventArgs e)
        {
            label2.Text = "Burst Pressure: " + burstPressure.ToString() + " " + Properties.Settings.Default.defaultPressureUnit;
        }
    }
}
