using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Drawing;

namespace game1
{
    public partial class Form1 : Form
    {
        private NetworkStream stream;
        private TcpClient tcpClient = new TcpClient();
        public Form1()
        {
            InitializeComponent();
            try
            {
                //向指定的IP地址的服务器发出连接请求
                tcpClient.Connect("10.1.230.74",3900);
                listBox1.Items.Add("连接成功！");
                stream = tcpClient.GetStream();
                byte[] data = new byte[1024];
                //判断网络流是否可读            
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    //Encoding ToEncoding = Encoding.GetEncoding("UTF-8");
                    Encoding FromEncoding = Encoding.GetEncoding("GB2312");
                    //data=Encoding.Convert(FromEncoding, ToEncoding, data);
                    //string msg = Encoding.UTF8.GetString(data, 0, data.Length);
                    string msg = Encoding.Default.GetString(data, 0, data.Length);

                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg1 = msg.Split(str1);
                    for (int j = 0; j < msg1.Length; j++)
                    {
                        listBox1.Items.Add(msg1[j]);
                    }
                }
            }
            catch
            {
                listBox1.Items.Add("服务器未启动！");
            }
        }


        private void button12_Click(object sender, EventArgs e)
        {

            //判断连接是否断开
            if (tcpClient.Connected)
            {
                //向服务器发送数据
                string msg = textBox1.Text;
                Byte[] outbytes = System.Text.Encoding.Default.GetBytes(msg + "\n");
                stream.Write(outbytes, 0, outbytes.Length);
                byte[] data = new byte[1024];
                //接收服务器回复数据
                if (stream.CanRead)
                {
                    int len = stream.Read(data, 0, data.Length);
                    string msg1 = Encoding.Default.GetString(data, 0, data.Length);
                    string str = "\r\n";
                    char[] str1 = str.ToCharArray();
                    string[] msg2 = msg1.Split(str1);
                    for (int j = 0; j < msg2.Length; j++)
                    {
                        listBox1.Items.Add(msg2[j]);
                    }
                }
                textBox1.Clear();
            }
            else
            {
                listBox1.Items.Add("连接已断开");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string s = @"D:\baidu\test.mp3";
            axWindowsMediaPlayer1.URL = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }
        int flag = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (flag == 0)
            {
                string picturePath = @"D:\baidu\test1.jpg";
                pictureBox1.Image = Image.FromFile(picturePath);
                flag = 1;
            }
            else
            {
                string picturePath = @"D:\baidu\test2.jpg";
                pictureBox1.Image = Image.FromFile(picturePath);
                flag = 0;
            }
        }

    }
}
