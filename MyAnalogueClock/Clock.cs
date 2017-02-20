using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Diagnostics; 



namespace MyClock
{


    // Types of clock hands. 
    public enum HandType {
        Milliseconds = 0, Seconds = 1,
        Minutes = 2, Hours = 3
    };



    // Class to hold time for the clock. 
    public class ClockTime
    {

        protected float milliseconds; 
        protected Int32 seconds;
        protected Int32 minutes;
        protected Int32 hours;


        public float Milliseconds
        {
            get { return milliseconds; }
            set { milliseconds = value; }
        }

        public Int32 Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }

        public Int32 Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }

        public Int32 Hours
        {
            get { return hours; }
            set { hours = value; }
        }


        public ClockTime()
        {
            Seconds = DateTime.Now.Second;
            Minutes = DateTime.Now.Minute;
            Hours = DateTime.Now.Hour;
        }


    }


    // Class to hold the coordinates, 
    // of the centre location of the clock. 
    public class ClockCentreCoordinates
    {

        protected Int32 x;
        protected Int32 y;

        public Int32 X {
            get { return x; } set { x = value; } }

        public Int32 Y {
            get { return y; } set { y = value; } }

        public ClockCentreCoordinates(Int32 CoordinateX, Int32 CoordinateY){
            X = CoordinateX;
            Y = CoordinateY;
        }

    }



    // Class to contain Hand properties and methods. 
    public class Hand{

        protected float angle;
        protected Int32 length;
        protected Int32 coordinateStartX;
        protected Int32 coordinateStartY;
        protected Int32 coordinateEndX;
        protected Int32 coordinateEndY;
        protected Color colour;
        protected float thickness; 

        protected float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        protected Int32 Length
        {
            get { return length; }
            set { length = value; }
        }

        protected Int32 CoordinateStartX
        {
            get { return coordinateStartX; }
            set { coordinateStartX = value; }
        }

        protected Int32 CoordinateStartY
        {
            get { return coordinateStartY; }
            set { coordinateStartY = value; }
        }
        protected Int32 CoordinateEndX
        {
            get { return coordinateEndX; }
            set { coordinateEndX = value; }
        }

        protected Int32 CoordinateEndY
        {
            get { return coordinateEndY; }
            set { coordinateEndY = value; }
        }

        protected Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        protected float Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }


        protected void GetEndCoordinates(ClockCentreCoordinates Coordinates)
        {
            if(Angle >= 0 && Angle <= 180)
            {
                CoordinateEndX = (int)(((float)Coordinates.X) + ((float)(Length) * Math.Sin(Math.PI * Angle / 180))); 
                CoordinateEndY = (int)(((float)Coordinates.Y) - ((float)(Length) * Math.Cos(Math.PI * Angle / 180))); 
            }
            else
            {
                CoordinateEndX = (int)(((float)(Coordinates.X) - (float)(Length) * -Math.Sin(Math.PI * Angle / 180)));
                CoordinateEndY = (int)(((float)(Coordinates.Y) - (float)(Length) * Math.Cos(Math.PI * Angle / 180)));
            }
        }


        protected void DrawHand(ref Graphics GraphicsInterface) {

            // Graphics Interface test. 
            //GraphicsInterface.DrawLine(
            //    new Pen(Color.Black, 1.0F),
            //    new Point(400/2, 390/2),
            //    new Point(250, 250));


            // Set line/hand characteristics.  
            Pen Line = new Pen(Colour, Thickness);
            Line.SetLineCap(LineCap.Round, LineCap.DiamondAnchor, DashCap.Flat); 


            GraphicsInterface.DrawLine(
                Line, 
                new Point(CoordinateStartX, CoordinateStartY),
                new Point(CoordinateEndX, CoordinateEndY));

        }


        public Hand(
            ref Graphics GraphicsInterface, 
            HandType TypeOfHand, 
            ClockTime Time, 
            ClockCentreCoordinates Coordinates, 
            Int32 HandLength, 
            Color HandColour, 
            float HandThickness){


            // Set specific hand properties. 
            CoordinateStartX = Coordinates.X; 
            CoordinateStartY = Coordinates.Y;
            Length = HandLength;
            Colour = HandColour;
            Thickness = HandThickness; 



            // What type of clock hand to create? 
            switch (TypeOfHand)
            {


                case HandType.Milliseconds:
                    {
                        // 1 millisecond = 0.006 degrees. 
                        Angle = Time.Milliseconds * 0.006F; 
                        break;
                    } 


                case HandType.Seconds:
                    {
                        // 1 second = 6 degrees. 
                        Angle = Time.Seconds * 6;
                        break;
                    } 

                case HandType.Minutes:
                    {
                        // 1 minute = 6 degrees. 
                        Angle = Time.Minutes * 6;
                        break;
                    }

                case HandType.Hours:
                    {
                        // 1 hour = 30 degrees + 0.5 degrees for each minute. 
                        Angle = Time.Hours * 30 + (Int32) (Time.Minutes * 0.5);
                        break; 
                    }


                default:
                    break;


            }



            GetEndCoordinates(Coordinates);
            DrawHand(ref GraphicsInterface);


        }

    }



    // Main clock class. 
    public class Clock
    {

        protected Int32 HandLengthSeconds = 150; // 100;
        protected Int32 HandLengthMinutes = 125; // 75;
        protected Int32 HandLengthHours = 90; // 60;

        protected float HandThicknessSeconds = 2.0F; // 1.0F;
        protected float HandThicknessMinutes = 4.0F; // 2.0F;
        protected float HandThicknessHours = 6.0F; // 3.0F;

        protected Color HandColourSeconds = Color.FromArgb(132, 42, 247); 
        protected Color HandColourMinutes = Color.FromArgb(63, 35, 99);
        protected Color HandColourHours = Color.FromArgb(15, 0, 33);

        

        public Clock(
            ref Graphics GraphicsInterface, 
            Int32 CoordinateCentreX, 
            Int32 CoordinateCentreY) {

            ClockCentreCoordinates CentreCoordinates =
                    new ClockCentreCoordinates(
                        CoordinateCentreX, CoordinateCentreY); 

            ClockTime Time = new ClockTime();

            Hand HandHours = new Hand(
                ref GraphicsInterface,
                HandType.Hours, 
                Time,
                CentreCoordinates,
                HandLengthHours,
                HandColourHours,
                HandThicknessHours); 

            Hand HandMinutes = new Hand(
                ref GraphicsInterface,
                HandType.Minutes,
                Time,
                CentreCoordinates,
                HandLengthMinutes,
                HandColourMinutes,
                HandThicknessMinutes);

            Hand HandSeconds = new Hand(
                ref GraphicsInterface,
                HandType.Seconds,
                Time,
                CentreCoordinates,
                HandLengthSeconds,
                HandColourSeconds,
                HandThicknessSeconds); 


        }


        


    }





    // Main stopwatch class. 
    // Inherits Clock class. 
    public class StopWatch : Clock 
    {


        protected static Stopwatch StopwatchTime = new Stopwatch();

        public static Boolean Running
        {
            get { return StopWatch.StopwatchTime.IsRunning; }
        }

        public static void Start()
        {
            StopWatch.StopwatchTime.Start();
            //StopWatch.StopwatchTime.Restart();

        }

        public static void Stop()
        {
            StopWatch.StopwatchTime.Stop();
        } 

        public static void Reset()
        {
            StopWatch.StopwatchTime.Reset(); 
        }


        // Set specific swopwatch properties. 
        protected Int32 HandLengthMilliseconds = 80; 
        protected float HandThicknessMilliseconds = 3.1F; // 2.5F; 
        //protected Color HandColourMilliseconds = Color.FromArgb(43, 255, 59);
        protected Color HandColourMilliseconds = Color.FromArgb(234, 39, 85); 






        public StopWatch(
            ref Graphics GraphicsInterface,
            Int32 CoordinateCentreX,
            Int32 CoordinateCentreY): 
            base ( ref GraphicsInterface, CoordinateCentreX, CoordinateCentreY )
        {



            ClockCentreCoordinates CentreCoordinates =
                    new ClockCentreCoordinates(
                        CoordinateCentreX, CoordinateCentreY);


            // Take modulus 60000  of milliseconds time. 
            //ClockTime Time = new ClockTime(); 
            //Time.Milliseconds = StopWatch.StopwatchTime.Elapsed.Seconds % 60;  

            // Take modulus 60000 of milliseconds time. 
            ClockTime Time = new ClockTime();
            //Time.Milliseconds = StopWatch.StopwatchTime.Elapsed.Milliseconds % 60000; 
            //Time.Milliseconds =
            //    (StopWatch.StopwatchTime.Elapsed.Seconds * 1000) +
            //    (StopWatch.StopwatchTime.Elapsed.Milliseconds); 

            Time.Milliseconds = (float)(StopWatch.StopwatchTime.Elapsed.TotalMilliseconds % 60000); 



            Hand HandMilliseconds = new Hand(
                ref GraphicsInterface,
                HandType.Milliseconds,
                Time,
                CentreCoordinates,
                HandLengthMilliseconds,
                HandColourMilliseconds,
                HandThicknessMilliseconds);


        }





    }






}
