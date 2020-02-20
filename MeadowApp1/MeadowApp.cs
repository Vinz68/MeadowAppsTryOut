using System;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;

namespace MeadowApp1
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        const int blinkDuration = 3000;
        readonly RgbPwmLed rgbPwmLed;

        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            // use onboad RGB led(s)
            rgbPwmLed = new RgbPwmLed(Device,
               Device.Pins.OnboardLedRed,
               Device.Pins.OnboardLedGreen,
               Device.Pins.OnboardLedBlue);

            Console.WriteLine("Initialize completed.");
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


        protected void DisplayText(string text)
        {
            Console.WriteLine(text);
        }

    }
}
