using Application.ConsoleClient.Ioc;
using System;
using System.IO;
using Unity;

namespace Application.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (IsValidArgument(args))
            {
                Bootstrap.Start();
                var orchestrator = Bootstrap.Container.Resolve<Orchestrator>();
                orchestrator.Run(args[0]);
            }
            else
            {
                Console.WriteLine(ErrorMessage.InvalidArgument);
            }
            Console.ReadKey();
        }

        private static bool IsValidArgument(string[] args)
        {
            if (args.Length > 0)
            {
                var fileName = args[0];
                return File.Exists(fileName);
            }
            return false;
        }
    }
}
