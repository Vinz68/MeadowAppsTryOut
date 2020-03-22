using System;
using System.Threading.Tasks;
using Meadow;
using Meadow.Hardware;
using Meadow.Devices;
using Meadow.Foundation.Sensors.Buttons;

namespace InputValueChanged
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        IDigitalInputPort inputBtn1;     // Button on pin D01 - using event handler "Changed"
        IDigitalInputPort inputBtn2;     // Button on pin D02 - using IObservable / FilterableObserver 
        IDigitalInputPort inputBtn3;     // PushButton on pin D03 - using event handlers "PushedBegin" and "PushedEnd" 

        PushButton pushButton;

        IChangeResult<bool> changeResult;

        public MeadowApp()
        {
            Console.WriteLine("Initializing...");

            //---------------------------------------------------------------------------------
            // Using the "Changed" event handler
            // Use D01 as input (using a pull-down register ~ 5K-ohm (to ground))
            
            inputBtn1 = Device.CreateDigitalInputPort(
                Device.Pins.D01,
                InterruptMode.EdgeRising,
                ResistorMode.PullDown,          
                50);  // Debounche set to 50msec


            // add the Changed event handler
            inputBtn1.Changed += (s, e) =>
            {
                Console.WriteLine($"Interrupt occurred. D04 changed to {inputBtn1.State}");
                Task.Delay(100);
            };
            Console.WriteLine("D01 - Changed Event. DONE.");
            //-----------------------------------------------------------------------------------


            // TODO: Button on pin D02 - using IObservable / FilterableObserver 
            //-----------------------------------------------------------------------------------


            //---------------------------------------------------------------------------------
            // Using a PushButton and its handlers
            // Use D03 as input 
            pushButton = new PushButton(Device, Device.Pins.D03);
            pushButton.PressStarted += PushButton_PressStarted;
            pushButton.PressEnded += PushButton_PressEnded;
            pushButton.Clicked += PushButton_Clicked;
            pushButton.LongPressClicked += PushButton_LongPressClicked;
            Console.WriteLine("D03 - PushButton DONE.");
            //-----------------------------------------------------------------------------------


        }

        private void PushButton_LongPressClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Long pressed!");
        }

        private void PushButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button Clicked");
        }

        private void PushButton_PressEnded(object sender, EventArgs e)
        {
            Console.WriteLine("Press ended");
        }

        private void PushButton_PressStarted(object sender, EventArgs e)
        {
            Console.WriteLine("Press started");
        }
    }
}
