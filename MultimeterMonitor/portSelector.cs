using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace MultimeterMonitor
{
    public partial class portSelector : Form
    {
        public portSelector()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            listBox1.Items.AddRange(ports);
        }

        private System.Windows.Forms.Label bufferThis = new Label();

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            bufferThis.Text = listBox1.SelectedItem.ToString();
            Form1 form = this.Owner as Form1;
            form.bufferLabel.Text = bufferThis.Text;
            form.initSettings();
            form.Show();
            this.Close();
        }
    }
}
