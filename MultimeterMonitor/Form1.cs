using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace MultimeterMonitor
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Main form constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Buffer label for subform transit
        /// </summary>
        public System.Windows.Forms.Label bufferLabel = new Label();
        /// <summary>
        /// Main communication port for multimeter
        /// </summary>
        public SerialPort communicationPort = new SerialPort();
        /// <summary>
        /// Serial port activation flag (if disabled = false)
        /// </summary>
        public bool communicationEnabled = false;
        
        /// <summary>
        /// Form with port selector
        /// </summary>
        portSelector settingsForm;
        /// <summary>
        /// Form information out of port
        /// </summary>
        monitor monitorForm;
        /// <summary>
        /// Initializing settings from settings form
        /// </summary>
        public void initSettings()
        {
            if (!File.Exists(Application.StartupPath + "\\settings.bin"))
            {
                File.Create(Application.StartupPath + "\\settings.bin");
            }
            FileStream settingsStream = new FileStream(Application.StartupPath + "\\settings.bin", FileMode.Open, FileAccess.Write);
            byte[] buffer = new byte[bufferLabel.Text.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(bufferLabel.Text[i]);
            }
            settingsStream.Write(buffer, 0, buffer.Length);
            settingsStream.Close();
            initPort(bufferLabel.Text);
            if (communicationPort.IsOpen)
            {
                communicationEnabled = true;
            }
            else
            {
                communicationEnabled = false;
                COM_Name.Text = "Порт не задан!";
                MessageBox.Show("Ошибка открытия порта! Проверьте доступность порта в диспетчере оборудования Windows!",
                                "ERROR:SERIAL_PORT", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// Initialization of port with name
        /// </summary>
        /// <param name="inputPortName">Name of COM port</param>
        public void initPort(string inputPortName)
        {
            //Serial port installations
            if (communicationPort.IsOpen)
            {
                communicationPort.Close();
            }
            communicationPort.PortName = inputPortName;
            communicationPort.BaudRate = 2400;
            communicationPort.DataBits = 8;
            communicationPort.StopBits = StopBits.Two;
            communicationPort.Parity = Parity.None;
            communicationPort.ReadTimeout = 1000;

            try
            {
                communicationPort.Open();
            }
            catch { }
            //Open port check
            if (communicationPort.IsOpen)
            {
                communicationEnabled = true;
            }
            else
            {
                communicationEnabled = false;
                /*
                COM_Name.Text = "Порт не задан!";
                MessageBox.Show("Ошибка открытия порта! Проверьте доступность порта в диспетчере оборудования Windows!",
                                "ERROR:SERIAL_PORT", MessageBoxButtons.OK);*/
            }
        }
        //Start communication session
        private void startMonBtn_Click(object sender, EventArgs e)
        {
            if (startMonBtn.Text == "Запустить монитор")
            {
                startMonBtn.Text = "Остановить монитор";
                monitorForm.Show(this);
            }
            else
            {
                monitorForm.Close();
                startMonBtn.Text = "Запустить монитор";
            }
        }
        //Open serial port setter
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm = new portSelector();
            settingsForm.Show(this);
        }
        //Save active session
        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //Load main form procedure
        private void Form1_Load(object sender, EventArgs e)
        {
            startMonBtn.Enabled = false;
            //Settings file check
            if (!File.Exists(Application.StartupPath + "\\settings.bin"))
            {
                File.Create(Application.StartupPath + "\\settings.bin");
            }
            byte[] fileBuffer;
            while (true)
            {
                try
                {
                    fileBuffer = File.ReadAllBytes(Application.StartupPath + "\\settings.bin");
                    break;
                }
                catch { }
            }
            //Serial port name installations
            if (fileBuffer.Length != 0)
            {
                char[] charBuffer = new char[fileBuffer.Length];
                for (int i = 0; i < fileBuffer.Length; i++)
                {
                    charBuffer[i] = Convert.ToChar(fileBuffer[i]);
                }
                string comName = "";
                for (int i = 0; i < charBuffer.Length; i++)
                {
                    comName += charBuffer[i].ToString();
                }
                initPort(comName);
                if (communicationEnabled)
                {
                    COM_Name.Text = comName;
                    startMonBtn.Enabled = true;
                }
                else
                {
                    COM_Name.Text = "Порт не задан!";
                    MessageBox.Show("Ошибка открытия порта! Проверьте доступность порта в диспетчере оборудования Windows!",
                                    "ERROR:SERIAL_PORT", MessageBoxButtons.OK);
                }
            }
            else
            {
                COM_Name.Text = "Порт не задан!";
            }
        }
    }
}
