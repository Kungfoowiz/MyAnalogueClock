using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D; 

using MyClock; 

namespace MyApp
{
    public partial class Form1 : Form
    {

        // Offset for form control dimensions in pixels. 
        private const Int32 FormOffsetWidthPx = 20;
        private const Int32 FormOffsetHeightPx = 40;

        // Drawing variables. 
        private Background MyBackground;
        protected Graphics GraphicsInterface;
        private Bitmap MyBitmap = null;

        private Int32 WidthPx;
        private Int32 HeightPx;

        private Timer MyTimer;
        private const float RefreshSeconds = 1.0F;
        private Timer MyTimer2;
        private const int RefreshMilliseconds = 100;


        public Form1()
        {
            InitializeComponent();


            // Setup refresh time. 
            MyTimer = new Timer(); 
            MyTimer.Interval = (int)RefreshSeconds * 1000;
            MyTimer.Tick += new EventHandler(this.TimerRefresh);
            MyTimer.Start(); 


            // Test Setup Stopwatch. 
            //if (!StopWatch.Running)
            //    StopWatch.Start(); 

            MyTimer2 = new Timer();
            MyTimer2.Interval = RefreshMilliseconds;
            MyTimer2.Tick += new EventHandler(this.TimerRefresh);
            MyTimer2.Start(); 


            RefreshGraphics();

            // Graphics Interface test. 
            //GraphicsInterface.DrawLine(
            //    new Pen(Color.Black, 1.0F),
            //    new Point(400 / 2, 390 / 2),
            //    new Point(250, 250)); 

            // Set form dimensions (incl. form control dimensions). 
            this.Width = WidthPx + FormOffsetWidthPx;
            this.Height = HeightPx + FormOffsetHeightPx;

            // Set picture box dimensions (excl. form control dimensions).
            MyFormPictureBox.Width = WidthPx;
            MyFormPictureBox.Height = HeightPx;

            



        } 



        // Event trigger to repaint form / refresh the clock. 
        private void TimerRefresh( object sender, EventArgs e)
        {
            //MessageBox.Show("Timer executed!"); 
            this.Refresh(); 
        }


        // Form repaint event. 
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            // Refresh graphics/background. 
            RefreshGraphics();

            // Refresh the clock.  
            Clock MyClock = new Clock(
                ref GraphicsInterface,
                WidthPx / 2,
                HeightPx / 2); 

            // Refresh the stopwatch.  
            StopWatch MyStopwatch = new StopWatch(
                ref GraphicsInterface,
                WidthPx / 2,
                HeightPx / 2); 


            MyFormPictureBox.Image = MyBitmap;


        }



        protected void RefreshGraphics()
        {

            if( GraphicsInterface != null )
                GraphicsInterface.Clear(Color.White);

            
            //MyBitmap = new Bitmap(WidthPx = 400, HeightPx = 390);

            // Set background image. 
            MyBackground = new Background(
                ref MyBitmap, out WidthPx, out HeightPx);


            // Create graphics to draw. 
            GraphicsInterface = Graphics.FromImage(MyBitmap); 
            // Set antialias for smooth lines. 
            GraphicsInterface.SmoothingMode = SmoothingMode.HighQuality; 

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (StopWatch.Running)
                StopWatch.Stop();
            else
                StopWatch.Start(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopWatch.Reset(); 
        }


    }
}
