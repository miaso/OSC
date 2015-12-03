using Osciloskopas;
using System;
//using Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ServiceModel;
using Osciloskopas.Properties;
//using Client.ChatService;


namespace Osciloskopas
{
    public partial class FormMain : Form
    {
        AnalysisForm f1 = new AnalysisForm();
        frmClient f2 = new frmClient();
        SerialHandler mySerial;

        double offset = 0;
        double vDivAmp = 1;
        int dummy;
        Thread readThread;

        public FormMain()
        {
            InitializeComponent();
            mySerial = new SerialHandler("COM6");
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].BorderWidth = 2;
            if (Settings.Default.LastCom != null)
            {
                textBox1.Text = Settings.Default.LastCom;
            }
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (stop)
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && int.TryParse(textBox1.Text, out dummy))
                {
                    mySerial = new SerialHandler("COM" + textBox1.Text);
                    mySerial.Open();
                    if (mySerial._continue == true)
                    {
                        buttonStart.Text = "Stop";
                        stop = false;
                        timerChart.Enabled = true;
                        timer1.Enabled = true;
                        readThread = new Thread(DoRead);
                        readThread.Start();
                        Settings.Default.LastCom = textBox1.Text;
                    }
                }
                else
                {
                    MessageBox.Show("Empty or non numeric value", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                stop = true;
                Thread.Sleep(200);
                mySerial.Close();
                buttonStart.Text = "Start";
                timerChart.Enabled = false;
            }
        }

        static int sampleCounter = 0;
        public FixedSizedQueue<double> queue = new FixedSizedQueue<double> { Limit = 4096 };
        bool stop = true;

        public void DoRead()
        {
            while (!stop)
            {
                sampleCounter++;
                double value = mySerial.ReadData();

                queue.Enqueue(value);

            }
        }

        private void timerChart_Tick(object sender, EventArgs e)
        {

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i < queue.Limit; ++i)
            {
                double value = queue.Get(i);
                chart1.Series[0].Points.AddXY(i, value * vDivAmp + offset);
                chart1.Series[1].Points.AddXY(i, offset);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (f2.readyFlag == true)
            {

                chart1.Series[2].Points.Clear();

                queue.Limit = 4096;
                chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 4096;
                for (int i = 0; i < f2.queue_received.Limit; ++i)
                {
                    double value = f2.queue_received.Get(i);

                    chart1.Series[2].Points.AddXY(i, value);
                }
                f2.readyFlag = false;
            }
            if (!stop)
            {
                label1.Text = "Samples per second: " + sampleCounter.ToString();
                sampleCounter = 0;
                double Max = queue.GetMax();
                double Min = queue.GetMin();
                label2.Text = "Vpp " + (Max - Min);
                label3.Text = "Max: " + Max;
                label4.Text = "Min: " + Min;
            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            queue.Limit = trackBar1.Value;

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            timerChart.Interval = trackBar2.Value;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            offset = (Double)trackBar5.Value / 10;
            label5.Text = "Offset " + offset + " V";
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            vDivAmp = (Double)trackBar3.Value / 2;
        }

        private void fft_btn_Click(object sender, EventArgs e)
        {
            //stabdom viska
            buttonStart.Text = "Start";
            stop = true;
            //timer1.Enabled = false;
            timerChart.Enabled = false;
            mySerial.Close();
            f1.qLimit = queue.Limit;
            f1.queue1 = queue;
            f1.sampleRate = sampleCounter;
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f2.TopLevel = false;
            f2.AutoScroll = true;
            f2.FormBorderStyle = FormBorderStyle.None;
            timer1.Enabled = true;
            panel1.Controls.Add(f2);
            f2.Show();
            f2.queue_main = queue;
            f2.queue_main.Limit = queue.Limit;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox1.Text);
            switch (comboBox1.Text)
            {
                case "1s":
                    queue.Limit = 12600 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 12600D;
                    break;
                case "500ms":
                    queue.Limit = 6300 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 6300D;
                    break;
                case "200ms":
                    queue.Limit = 2520 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 2520D;
                    break;
                case "100ms":
                    queue.Limit = 1260 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1260D;
                    break;
                case "50ms":
                    queue.Limit = 630 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 630D;
                    break;
                case "20ms":
                    queue.Limit = 252 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 252D;
                    break;
                case "10ms":
                    queue.Limit = 126 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 126D;
                    break;
                case "5ms":
                    queue.Limit = 63 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 63D;
                    break;
                case "2ms":
                    queue.Limit = 25 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 25D;
                    break;
                case "1ms":
                    queue.Limit = 12 * 6;
                    chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 12D;
                    break;
            }
            if (comboBox1.SelectedIndex == -1)
            {
            }
            else
            {
                comboBox2.SelectedIndex = -1;
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
            }
            else
            {
                queue.Limit = Convert.ToInt32(comboBox2.Text);
                chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Convert.ToInt32(comboBox2.Text) / 6;
                comboBox1.SelectedIndex = -1;
            }


        }




    }
}
