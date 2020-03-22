using System;
using System.Threading;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Sensors.Buttons;
using Meadow.Hardware;


namespace OledApp
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        const int blinkDuration = 3000; 
        
        RgbPwmLed rgbPwmLed;            // Using the on board RGB led
        Led led;                        // Using a LED on pin D01
        IDigitalInputPort inputBtn;     // Button on pin D03
        Ssd1306 display;                // Using a 128x32 OLED display, SDA on pin D07, CLK on pin D08
        GraphicsLibrary graphics;
        readonly Font8x12 font = new Font8x12();


        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            InitializePeripherals();

            Console.WriteLine("Initialize completed.");

            MainLoop();
        }

        protected void InitializePeripherals()
        {
            Console.WriteLine("Initialize LED on pin D01...");

            // led on pin D01
            led = new Led(Device.CreateDigitalOutputPort(Device.Pins.D01));

            
            Console.WriteLine("Initialize RgbPwmLed...");
            // on-board RGB led(s)
            rgbPwmLed = new RgbPwmLed(
               Device,
               Device.Pins.OnboardLedRed,
               Device.Pins.OnboardLedGreen,
               Device.Pins.OnboardLedBlue);


            Console.WriteLine("Initialize I2C bus and Oled Display...");
            // enable the I2C bus
            var i2CBus = Device.CreateI2cBus();

            // create display, which is a I2C device
            display = new Ssd1306(i2CBus, 60, Ssd1306.DisplayType.OLED128x32);
            graphics = new GraphicsLibrary(display)
            {
                // Flip the screen 
                Rotation = GraphicsLibrary.RotationType._180Degrees
            };


        }

        private void PushButton_PressEnded(object sender, EventArgs e)
        {
            Console.WriteLine("Press Started...");
        }

        private void PushButton_PressStarted(object sender, EventArgs e)
        {
            Console.WriteLine("Press Stopped...");
        }

        private void Button_Changed(object sender, DigitalInputPortEventArgs e)
        {
            // Console.WriteLine("Changed: " + e.Value.ToString() + ", Time: " + e.Time.ToString());
            Console.WriteLine("Event Changed => ToggleLed.");
            //ToggleLed(led);
        }



        private static void ToggleLed(Led led)
        {
            led.IsOn = (!led.IsOn);
            Console.WriteLine($"BtnPessed => Toggle Led to: {led.IsOn}");
        }

        protected void DisplayText(string text, int x = 12, int y =12)
        {
            graphics.Clear();
            graphics.CurrentFont = font;
            graphics.DrawRectangle(0, 0, 128, 32);
            graphics.DrawText(x, y, text);
            graphics.Show();
        }

        protected void MainLoop()
        {

            Console.WriteLine("Initialize a PushButton and its events handler on pin D03...");

            inputBtn = Device.CreateDigitalInputPort(Device.Pins.D03, InterruptMode.EdgeBoth, ResistorMode.PullUp, 50);


            // TODO: Some reason the events PressStarted / Ended and also the inputBtn.Changed seems not to work anymore....
            // but the  "Console.WriteLine($"InputBtn.State = {inputBtn.State}"); "  in the loop seems to work !.
            // STRANGE....

            var pushButton = new PushButton(inputBtn);
            pushButton.PressStarted += PushButton_PressStarted;
            pushButton.PressEnded += PushButton_PressEnded;


            // add an event handler
            inputBtn.Changed += (s, o) =>
            {
                Console.WriteLine("Event Changed => ToggleLed.");
                //ToggleLed(led);
            };



            Console.WriteLine("Starting MainLoop...");

            while (true)
            {
                Console.WriteLine($"InputBtn.State = {inputBtn.State}");

                DisplayText("Blink RED");
                Blink(Color.Red);

                Console.WriteLine($"InputBtn.State = {inputBtn.State}");

                DisplayText("Blink GREEN");
                Blink(Color.Green);

                Console.WriteLine($"InputBtn.State = {inputBtn.State}");

                DisplayText("Blink BLUE");
                Blink(Color.Blue);

                Console.WriteLine($"InputBtn.State = {inputBtn.State}");

            }
        }

        protected void Blink(Color color)
        {
            rgbPwmLed.StartBlink(color);
            Thread.Sleep(blinkDuration);
            rgbPwmLed.Stop();
        }
    }
}
