using System;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;

using Meadow.Hardware;


namespace OledApp
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        const int blinkDuration = 3000; 
        
        RgbPwmLed rgbPwmLed;        // Using the on board RGB led
        Led led;                    // Using a LED on pin D01
        Ssd1306 display;            // Using a 128x32 OLED display, SDA on pin D07, CLK on pin D08
        GraphicsLibrary graphics;
        readonly Font8x12 font = new Font8x12();


        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            InitializePeripherals();

            Console.WriteLine("Initialize completed.");
        }

        void InitializePeripherals()
        {
            // onboad RGB led(s)
            rgbPwmLed = new RgbPwmLed(
               Device,
               Device.Pins.OnboardLedRed,
               Device.Pins.OnboardLedGreen,
               Device.Pins.OnboardLedBlue);

            // led on pin D01
            led = new Led(Device.CreateDigitalOutputPort(Device.Pins.D01));

            // enable the I2C bus
            var i2CBus = Device.CreateI2cBus();

            // create display, which is a I2C device
            display = new Ssd1306(i2CBus, 60, Ssd1306.DisplayType.OLED128x32);
            graphics = new GraphicsLibrary(display)
            {
                // Flip the screen 
                Rotation = GraphicsLibrary.RotationType._180Degrees
            };


            // button on pin D03 with interrupts enabled
            var input = Device.CreateDigitalInputPort(
                Device.Pins.D03,
                InterruptMode.EdgeRising,
                ResistorMode.PullUp, 100);  // 100 msec debounce

            // add an event handler
            input.Changed += (s, e) =>
            {
                ToggleLed(led);
            };

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

        public void Run()
        {
            Console.WriteLine("Starting...");

            while (true)
            {
                DisplayText("Blink RED");
                Blink(Color.Red);
                DisplayText("Blink GREEN");
                Blink(Color.Green);
                DisplayText("Blink BLUE");
                Blink(Color.Blue);

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
