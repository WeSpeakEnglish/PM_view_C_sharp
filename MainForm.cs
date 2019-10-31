/*
 * Created by SharpDevelop.
 * User: P1
 * Date: 24.09.2012
 * Time: 14:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Security.Permissions;

namespace Drive1
{
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]
	public partial class MainForm : Form
	{
		public string MessageInTextarea1 = null;
		public SerialPort MyPort;
		public MainForm()
		{
			
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			InitializeComponent();
   
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		

		private void MyPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			System.Threading.Thread.Sleep(100);
			if (MyPort.IsOpen) {
				int bytes = MyPort.BytesToRead;
				byte[] read_buf = new byte[bytes];
				MyPort.Read(read_buf, 0, bytes);
            
				this.BeginInvoke(new LineReceivedEvent(LineReceived), read_buf);
			}
		}

		private delegate void LineReceivedEvent(byte[] read_buf);
 
		private void LineReceived(byte[] read_buf)
		{

		
			Int32[] ValuesOfSource = new Int32[10];

			/// protection
			if(read_buf.Length == 5){
			Int32 PM = (read_buf[2]*255 + read_buf[3]);
			if(PM > 10 && PM < 27) label2.ForeColor = Color.DarkGoldenrod;
			if(PM < 11 ) label2.ForeColor = Color.DarkGreen;
			if(PM > 26 ) label2.ForeColor = Color.Red;
			label2.Text = PM.ToString();
			}
 
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			string[] ports = SerialPort.GetPortNames();
			bool HavePort = false;
			Array.Sort(ports);
			foreach (string port in ports) {
				comboBox1.Items.Add(port.ToString());
				HavePort = true;
			}
			if (HavePort)
				comboBox1.Text = comboBox1.Items[0].ToString();//Select(2,1);
		 else
				MessageBox.Show("Please, connect a COM adapter");
		}

 
		public void ConnectToCom(string COM_Name, int ConnetOrDisconnect)
		{
			if (ConnetOrDisconnect == 1) {
				MyPort = new SerialPort(COM_Name, 9600, Parity.None, 8, StopBits.One);
				MyPort.ReadTimeout = 100;
				MyPort.WriteTimeout = 100;
				MyPort.ReadBufferSize = 4096;
				MyPort.WriteBufferSize = 4096;
				MyPort.DataReceived += new SerialDataReceivedEventHandler(MyPort_DataReceived);
				MyPort.Encoding = System.Text.Encoding.GetEncoding("windows-1251");
				MyPort.Open();
				// MyPort.Write(data, 0, data.Length);	
			} else {
				MyPort.Close();
			}
		
		}
     
		
		void MainFormClosing(object sender, FormClosingEventArgs e)
		{
			//MyPort.Close();
		}
		
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}
		
		void TextBox2TextChanged(object sender, EventArgs e)
		{
			
		}
	}

}
