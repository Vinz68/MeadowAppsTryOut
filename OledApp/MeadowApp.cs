using System;
using System.Threading;
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
        const int pulseDuration = 3000;
        readonly RgbPwmLed rgbPwmLed;

        Ssd1306 display;
        GraphicsLibrary graphics;
        Led led;


        public MeadowApp()
        {
            InitializePeripherals();

            rgbPwmLed = new RgbPwmLed(Device,
                       Device.Pins.OnboardLedRed,
                       Device.Pins.OnboardLedGreen,
                       Device.Pins.OnboardLedBlue);

            PulseRgbPwmLed();

            DisplayText($"Initialized ...");
        }

        void InitializePeripherals()
        {
            // enable the I2C bus, bit slower clock (Default = 1000)
            var i2CBus = Device.CreateI2cBus(800);

            // create display, which is a I2C device
            display = new Ssd1306(i2CBus, 60, Ssd1306.DisplayType.OLED128x32);
            graphics = new GraphicsLibrary(display)
            {
                Rotation = GraphicsLibrary.RotationType._180Degrees
            };

            // Led on pin D01
            led = new Led(Device.CreateDigitalOutputPort(Device.Pins.D01));

            // button on pin D03 with interrupts enabled
            var input = Device.CreateDigitalInputPort(
                Device.Pins.D03,
                InterruptMode.EdgeRising,
                ResistorMode.Disabled, 100);  // 100 msec debounce

            // add an event handler
            input.Changed += (s, e) =>
            {
                ToggleLed();
            };

        }


        void DisplayText(string text, int x = 12)
        {
            graphics.Clear();
            graphics.CurrentFont = new Font8x12();
            graphics.DrawRectangle(0, 0, 128, 32);
            graphics.DrawText(x, 12, text);
            graphics.Show();
        }

        protected void PulseRgbPwmLed()
        {
            while (true)
            {
                DisplayText("Pulsing RED");
                Pulse(Color.Red);
                DisplayText("Pulsing GREEN");
                Pulse(Color.Green);
                DisplayText("Pulsing BLUE");
                Pulse(Color.Blue);

                ToggleLed();
            }
        }

        private void ToggleLed()
        {
            DisplayText("Toggle LED");
            led.IsOn = (!led.IsOn);
        }

        protected void Pulse(Color color)
        {
            rgbPwmLed.StartPulse(color);
            Thread.Sleep(pulseDuration);
            rgbPwmLed.Stop();
        }
    }
}
