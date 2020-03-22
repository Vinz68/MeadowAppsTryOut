using System;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Motors.Stepper;


namespace Uln2003App
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        private  Uln2003 stepperController;

        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            InitializePeripherals();

            Console.WriteLine("Initialize completed.");

            MainLoop();
        }


        protected void InitializePeripherals()
        {
            Console.WriteLine("Initialize the stepperController.");

            stepperController = new Uln2003(
                device: Device,
                pin1: Device.Pins.D01,
                pin2: Device.Pins.D02,
                pin3: Device.Pins.D03,
                pin4: Device.Pins.D04);

        }

        protected void MainLoop()
        {
            Console.WriteLine("MainLoop - drive the stepper 100 iterations CW and 100 CCW.");
            stepperController.Step(1024);


            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Step forward {i}");
                stepperController.Step(50);
                Thread.Sleep(10);
            }


            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Step backwards {i}");
                stepperController.Step(-50);
                Thread.Sleep(10);
            }
        }

    }
}
