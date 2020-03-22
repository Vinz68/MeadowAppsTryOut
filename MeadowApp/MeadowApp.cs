using System;
using System.Threading;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Buttons;
using Meadow.Hardware;

namespace MeadowApp
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        const int blinkDuration = 3000;
        readonly RgbPwmLed rgbPwmLed;

        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            rgbPwmLed = new RgbPwmLed(Device,
                       Device.Pins.OnboardLedRed,
                       Device.Pins.OnboardLedGreen,
                       Device.Pins.OnboardLedBlue);

            MainLoop();
        }

        protected void MainLoop()
        {
            Console.WriteLine("Main loop entered...");
            while (true)
            {
                Blink(Color.Red);
                Blink(Color.Green);
                Blink(Color.Blue);
            }
        }

        protected void Blink(Color color)
        {
            Console.WriteLine($"Blink {color}");
            rgbPwmLed.StartBlink(color);
            Thread.Sleep(blinkDuration);
            rgbPwmLed.Stop();
        }
    }
}
