using System;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;

namespace MeadowApp
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        const int blinkDuration = 3000;
        readonly RgbPwmLed rgbPwmLed;

        public MeadowApp()
        {
            rgbPwmLed = new RgbPwmLed(Device,
                       Device.Pins.OnboardLedRed,
                       Device.Pins.OnboardLedGreen,
                       Device.Pins.OnboardLedBlue);

            BlinkRgbPwmLeds();
        }

        protected void BlinkRgbPwmLeds()
        {
            while (true)
            {
                Blink(Color.Red);
                Blink(Color.Green);
                Blink(Color.Blue);
            }
        }

        protected void Blink(Color color)
        {
            rgbPwmLed.StartBlink(color);
            Console.WriteLine($"Blink {color}");
            Thread.Sleep(blinkDuration);
            rgbPwmLed.Stop();
        }
    }
}
