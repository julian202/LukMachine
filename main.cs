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
            NotImplemented();
        }

        private void NotImplemented()
        {
            MessageBox.Show("Feature not yet implemented!", COMMS.Instance.appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            COMMS.Instance.OpenPort(Properties.Settings.Default.COMM);
            Manual manctrl = new Manual();
            Hide();
            manctrl.ShowDialog();
            Show();
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
            NotImplemented();
        }
    }
}
