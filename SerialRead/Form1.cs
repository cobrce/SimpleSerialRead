using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace SerialRead
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			serialPort1.BaudRate = 9600;
			serialPort1.DataReceived += SerialPort1_DataReceived;
		}

		private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			string data = "";

			while (serialPort1.IsOpen && serialPort1.BytesToRead > 0)
			{
				if (!serialPort1.IsOpen)
					return;
				data += ((char)serialPort1.ReadByte()).ToString();
			}
			textBox1.BeginInvoke(new Action<string>((s)=> textBox1.AppendText(s)),new object[] { data });
		}

		private void comboBox1_DropDown(object sender, EventArgs e)
		{
			comboBox1.Items.Clear();
			comboBox1.Items.AddRange(SerialPort.GetPortNames());
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				if (serialPort1.IsOpen)
					serialPort1.Close();
				serialPort1.PortName = comboBox1.Text;
				serialPort1.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
