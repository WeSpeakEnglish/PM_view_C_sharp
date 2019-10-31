/*
 * Created by SharpDevelop.
 * User: P1
 * Date: 24.09.2012
 * Time: 14:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

//using System.Collections;
using System.Windows;
namespace Drive1
{
	using System;
	using System.Windows.Forms;
	using System.Collections.Generic;
	using System.Drawing;

	using System.IO;
	using System.Text;
	using System.IO.Ports;
	using System.Threading;
	using System.Security.Permissions;
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private bool ConnectDisconnect = true;
		private int CounterTick = 0;

		private long TimeAfterMsg = 0;
		//	private int TickCommand=0;
		private string DataFromCom = "";
		//		int Index=0;SerialPort port = new SerialPort( ″COM1″ , 9600, Parity.None, 8, StopBits.One);

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// serialPort1
			// 
			this.serialPort1.ReadTimeout = 100;
			this.serialPort1.ReceivedBytesThreshold = 30;
			this.serialPort1.WriteTimeout = 100;
			// 
			// comboBox1
			// 
			resources.ApplyResources(this.comboBox1, "comboBox1");
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Sorted = true;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.comboBox1);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new System.EventHandler(this.GroupBox1Enter);
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// textBox1
			// 
			resources.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox2.Controls.Add(this.label2);
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			this.groupBox2.Enter += new System.EventHandler(this.GroupBox2Enter);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.label2, "label2");
			this.label2.ForeColor = System.Drawing.Color.ForestGreen;
			this.label2.Name = "label2";
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Info;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Timer timer1;
		private string DataHead;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
	

		void Timer1Tick(object sender, System.EventArgs e)
		{
			TimeAfterMsg++;
			if (TimeAfterMsg == 5 && DataFromCom.ToString().Length > 5 && DataFromCom.ToCharArray()[0] == 'B') {

				TimeAfterMsg = 0;
			}
			DataHead = DateTime.Now.ToString();
			DataHead = DataHead.Remove(0, 10);
			this.Text = "PM sensor       " + DataHead;
			CounterTick++;
		}
		
		void GroupBox1Enter(object sender, EventArgs e)
		{
			
		}

		void Button1Click(object sender, EventArgs e)
		{
			if (ConnectDisconnect == true) {
				string strComb1 = this.comboBox1.SelectedItem.ToString();
				ConnectToCom(this.comboBox1.SelectedItem.ToString(), 1);
				this.button1.Text = "Отключить";
				ConnectDisconnect = false;
			} else {
				ConnectToCom(this.comboBox1.SelectedItem.ToString(), 0);
				ConnectDisconnect = true;
				this.button1.Text = "Подключить";
			}
		}
		
		void Label4Click(object sender, EventArgs e)
		{
			
		}
		
		///
		
		void Label5Click(object sender, EventArgs e)
		{
			
		}
		
		void Label6Click(object sender, EventArgs e)
		{
			
		}
		
		void Label7Click(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown6ValueChanged(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown5ValueChanged(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown4ValueChanged(object sender, EventArgs e)
		{
			
		}
		
		void GroupBox4Enter(object sender, EventArgs e)
		{
			
		}
		
		void Label14Click(object sender, EventArgs e)
		{
			
		}
		
		void Label15Click(object sender, EventArgs e)
		{
			
		}
		
		void Label17Click(object sender, EventArgs e)
		{
			
		}
		
		void Label19Click(object sender, EventArgs e)
		{
			
		}
		

		void Button6Click(object sender, EventArgs e)
		{
			
		}
		
		void Label26Click(object sender, EventArgs e)
		{
			
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			
		}
		
		void GroupBox2Enter(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown15ValueChanged(object sender, EventArgs e)
		{
			
		}
		
		void Label3Click(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown3ValueChanged(object sender, EventArgs e)
		{
			
		}
		
		void GroupBox3Enter(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown14ValueChanged(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown13ValueChanged(object sender, EventArgs e)
		{
			
		}
		

		
		void RadioButton2CheckedChanged(object sender, EventArgs e)
		{
			
		}
		
		void NumericUpDown9ValueChanged(object sender, EventArgs e)
		{
			
		}
		
		void Label16Click(object sender, EventArgs e)
		{
			
		}
		
		void RadioButton3CheckedChanged(object sender, EventArgs e)
		{
			
		}

	}
}
