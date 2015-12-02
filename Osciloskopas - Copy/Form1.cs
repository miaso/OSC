using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using Osciloskopas.ChatService;
using System.Threading.Tasks;

namespace Osciloskopas
{
    public partial class frmClient : Form
    {
        ReceiveClient rc = null;
        string myName;
        string sender_name;

        int i = 0;
        string[] queue_receive = new string[4096];
        string[] queue_send = new string[4096];

        float[] queue_sample_received = new float[4096];
        float[] queue_sample_to_send = new float[4096];

        public FixedSizedQueue<double> queue_main = new FixedSizedQueue<double> { Limit = 4096 };
        public FixedSizedQueue<double> queue_received = new FixedSizedQueue<double> { Limit = 4096 };


        public frmClient()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmClient_FormClosing);

        }

        void txtSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyValue = (int)e.KeyChar;

            if (keyValue == 13)
                SendMessage();

        }

        private void frmClient_FormClosing(object sender, EventArgs e)
        {
            rc.Stop(myName);
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            myName = "Receiver"; 
                //Environment.UserName + "@" + System.Environment.MachineName;
            txtUserName.Text = myName;
            txtUserName.Enabled = false;

            txtMsgs.Enabled = true;
            btnSend.Enabled = true;



            rc = new ReceiveClient();
            rc.Start(rc, myName);

            rc.NewNames += new GotNames(rc_NewNames);
            rc.ReceiveMsg += new ReceviedMessage(rc_ReceiveMsg);
            queue_received.Limit = queue_main.Limit;

        }

        void rc_ReceiveMsg(string sender, string msg)
        {
            sender_name = sender;
            i++;
            queue_received.Enqueue(Double.Parse(msg));

            if (i == 4096)
            {
                load_btn.Enabled = true;
                txtMsgs.Text += "Samples received: " + i.ToString() + " from " + sender_name;
            }
            
        }

        void rc_NewNames(object sender, List<string> names)
        {
            lstClients.Items.Clear();
            foreach (string name in names)
            {
                if (name != myName)
                    lstClients.Items.Add(name);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            for (int i = 0; i < queue_main.Limit; i++)
            {
                if (lstClients.Items.Count != 0)
                {
                    //txtMsgs.Text += Environment.NewLine + myName + ">" + queue_main.Get(i).ToString();
                    if (lstClients.SelectedItems.Count == 0)
                        rc.SendMessage(queue_main.Get(i).ToString(), myName, lstClients.Items[0].ToString());
                    else
                        if (!string.IsNullOrEmpty(lstClients.SelectedItem.ToString()))
                            rc.SendMessage(queue_main.Get(i).ToString(), myName, lstClients.SelectedItem.ToString());

                }
            }
        }

        private void load_btn_Click(object sender, EventArgs e)
        {
            FormMain f3 = new FormMain();
            f3.queue = queue_received;
            f3.queue.Limit = queue_received.Limit;
            f3.Show();
            f3.fill_the_charts();
        }

    }

}
