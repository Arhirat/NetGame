using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Net
{
    public partial class Form1 : Form
    {
        Net mNet;

        public Form1()
        {
            InitializeComponent();

			mTimer = new Timer();
			this.DoubleBuffered = true;
			newGame();
        }

		void newGame()
		{
			mNet = new Net(
				delegate(String s) { this.Text = s; return true; },
				new ControllerPlayer(win("Blue player win!")), 
				new ControllerComp(win("Red player win!")), 
				9, 3, 3, this.Width, this.Height);
		}

		Del.None win(String msg)
		{
			return delegate()
			{
				MessageBox.Show(msg);
				newGame(); 
			};
		}

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
			mNet.getCurPlayerController().paint(new Drawer(e.Graphics, this.Width-15, this.Height-35));
        }

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			mNet.getCurPlayerController().mouseMove(e);
			Invalidate();
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			mNet.getCurPlayerController().mouseDown(e);
			Invalidate();
		}

		private void Form1_MouseUp(object sender, MouseEventArgs e)
		{
			mNet.getCurPlayerController().mouseUp(e);
			Invalidate();
		}

		private void mTimer_Tick(object sender, EventArgs e)
		{
			mNet.getCurPlayerController().inc(0.1f);
			Invalidate();
		}
    }
}
