using Meadow;
using System.Threading;

namespace OledApp
{
    class Program
    {

        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--exitOnDebug") return;

            // instantiate and run new meadow app
            var app = new MeadowApp();
            app.Run();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
