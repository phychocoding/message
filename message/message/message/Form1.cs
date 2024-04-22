

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace message
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        UdpClient U;
        Thread Th;

        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            Th = new Thread(Listen);
            Th.Start();
            button1.Enabled = false;
        }
        private void Listen()
        {
            int Port = int.Parse(textBox1.Text);
            U = new UdpClient(Port);
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse("120.105.156.13"), Port);

            do
            {
                Byte[] B = U.Receive(ref EP);
                textBox2.Text = Encoding.Default.GetString(B);
            } while (true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string IP = textBox3.Text;
            int Port = int.Parse(textBox4.Text);
            Byte[] B = Encoding.Default.GetBytes(textBox5.Text);

            UdpClient S = new UdpClient();
            S.Send(B, B.Length, IP, Port);
            S.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Th.Abort();
                U.Close();
            }
            catch(Exception ex) 
            {

            }
        }
    }
}
