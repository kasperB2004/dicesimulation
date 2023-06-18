using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;


namespace dicesimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //distpachttimer var
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //random var
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            //set up distpachttimer and hook it up , start and set interval
            dispatcherTimer.Tick += new EventHandler(ChooseNumber);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(3);
            dispatcherTimer.Start();
        }

        private void ChooseNumber(object? sender, EventArgs e)
        {
            int Number = rnd.Next(0, 7);            
            int[] list = { 0, 90, 180, 270 };
            bool varationOne =   rnd.Next(1, 3) == 1 ? true : false;
            //Send Angles to the fuction that handles moving the dice based on selected number
            switch(Number)
            {
                case 1:
                    MoveCube((varationOne ? 0 : 180), (varationOne ? 0 : 180));
                    break;
                case 2:
                    MoveCube(list[rnd.Next(0, list.Length)], 90);
                    break;
                case 3:
                    MoveCube((varationOne ? 270 : 90), (varationOne  ? 0 : 180));
                    break;
                case 4:
                    MoveCube((varationOne ? 90 : 270), (varationOne  ? 0 : 180));
                    break;
                case 5:
                    MoveCube(list[rnd.Next(0, list.Length)], 270);
                    break;
                case 6:
                    MoveCube((varationOne ? 180 : 0), (varationOne  ? 0 : 180));
                    break;
            }
        }
        private async void MoveCube(int NewAngle1, int NewAngle2)
        {
            //stop timer
            dispatcherTimer.Stop();
            
            //variables for counting up or down logic
            bool CountUp1;
            bool CountUp2;


            //get current angles
            double CurrentAngle1 = Angle1.Angle;
            double CurrentAngle2 = Angle2.Angle;

            //spin the cube a bit before showing a number
            int RepeatxTimes = rnd.Next(1, 4);
            for (int i = 0; i <= RepeatxTimes;i++)
            {
                //logic if should count from 0 to 360 or oposite
                CountUp1 = (rnd.Next(1, 3) == 1) ? true : false;
                CountUp2 = (rnd.Next(1, 3) == 1) ? true : false;

                int angle = rnd.Next(160, 365);
                for(int u = 0; u <= angle; u++)
                {
                    //if at angle stop otherwise count up or down
                    CurrentAngle1 = (CountUp1) ? CurrentAngle1 + 1 : CurrentAngle1 - 1;
                    CurrentAngle2 = (CountUp2) ? CurrentAngle2 + 1 : CurrentAngle2 - 1;

                    //if out of bounds move to inside bounds
                    CurrentAngle1 = (CurrentAngle1 > 360) ? 0 : CurrentAngle1;
                    CurrentAngle2 = (CurrentAngle2 > 360) ? 0 : CurrentAngle2;
                    CurrentAngle1 = (CurrentAngle1 < 0) ? 360 : CurrentAngle1;
                    CurrentAngle2 = (CurrentAngle2 < 0) ? 360 : CurrentAngle2;

                    //set angles
                    Angle1.Angle = CurrentAngle1;
                    Angle2.Angle = CurrentAngle2;

                    //add a delay so it rotates instead of teleporting to chosen number
                    await Task.Delay(1);
                }

            }
            //logic if should count from 0 to 360 or oposite
            CountUp1 = (rnd.Next(1, 3) == 1) ? true : false;
            CountUp2 = (rnd.Next(1, 3) == 1) ? true : false;

            //loop till reached angles has been reached
            while (CurrentAngle1 != NewAngle1 || CurrentAngle2 != NewAngle2)
            {
                //if at angle stop otherwise count up or down
                CurrentAngle1 = (CurrentAngle1 == NewAngle1) ? CurrentAngle1 : (CountUp1) ? CurrentAngle1+1 : CurrentAngle1-1;
                CurrentAngle2 = (CurrentAngle2 == NewAngle2) ? CurrentAngle2 : (CountUp2) ? CurrentAngle2+1 : CurrentAngle2-1;

                //if out of bounds move to inside bounds
                CurrentAngle1 = (CurrentAngle1 > 360) ?  0 : CurrentAngle1;
                CurrentAngle2 = (CurrentAngle2 > 360) ?  0 : CurrentAngle2;
                CurrentAngle1 = (CurrentAngle1 < 0) ? 360 : CurrentAngle1;
                CurrentAngle2 = (CurrentAngle2 < 0) ? 360 : CurrentAngle2;

                //set angles
                Angle1.Angle = CurrentAngle1;
                Angle2.Angle = CurrentAngle2;

                //add a delay so it rotates instead of teleporting to chosen number
                await Task.Delay(1);
            }
            //start timer
            dispatcherTimer.Start();
        }
    }
}
