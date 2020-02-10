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
using System.IO.Ports;

namespace MultimeterMonitor
{
    public partial class monitor : Form
    {
        public monitor(SerialPort inPort)
        {
            InitializeComponent();

            communication = inPort;

            communication.Open();

            if (communication.IsOpen)
            {
                monitorThread = new Thread(readFromCOM);
                monitorThread.Start();
            }
            else
            {
                MessageBox.Show("Ошибка открытия порта! Проверьте доступность порта в диспетчере оборудования Windows!",
                                    "ERROR:SERIAL_PORT", MessageBoxButtons.OK);
            }
        }

        private Thread monitorThread;

        private SerialPort communication;

        private long stringCounter = 1;

        int errCounter = 0;

        private void readFromCOM()
        {
            byte[] buffer = new byte[11];
            string monitorBuffer = "";
            char[] symbolBuffer;
            while (true) {
                if (communication.Read(buffer, 0, 11) == 0)
                {
                    if (errCounter != 0)
                    {
                        monitorBox.AppendText(stringCounter.ToString() + " :CONNECTION RESTORED: ");
                        errCounter = 0;
                    }
                    else
                    {
                        monitorBuffer += stringCounter.ToString() + " :NORMAL: ";
                    }
                    symbolBuffer = new char[buffer.Length];
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        symbolBuffer[i] = Convert.ToChar(buffer[i]);
                    }
                    monitorBuffer += symbolBuffer.ToString() + "\n";
                    monitorBox.AppendText(monitorBuffer);
                    stringCounter++;
                }
                else
                {
                    monitorBox.AppendText(stringCounter.ToString() + " :ERROR: ОШИБКА ЧТЕНИЯ ИЗ ПОРТА! ПОВТОР: " + (errCounter+1).ToString() + "\n");
                    stringCounter++;
                    errCounter++;
                    if (errCounter > 10)
                    {
                        return;
                    }
                }
            }
        }

        private void monitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            monitorThread.Abort();
        }
    }
}
