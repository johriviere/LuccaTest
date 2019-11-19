using Application.ConsoleClient.Adapter;
using Application.ConsoleClient.Validation;
using Application.Validation.Console;
using Domain.PrimaryPort;
using Domain.SecondaryPort;
using Domain.Service;
using Infrastructure.Adapter;
using Infrastructure.Dijkstra;
using System.Collections.Generic;
using Unity;

namespace Application.ConsoleClient.Ioc
{
    public class Bootstrap
    {
        public static IUnityContainer Container;
        public static void Start()
        {
            Container = new UnityContainer();
            Register();
        }

        private static void Register()
        {
            Container.RegisterType<IOrchestrator, Orchestrator>();
            Container.RegisterType<IFileReader, FileReader>();
            Container.RegisterType<IWriter, ConsoleWriter>();
            Container.RegisterType<IValidationService<IEnumerable<string>>, ValidationService>();
            Container.RegisterType<IConsoleAdapter, ConsoleAdapter>();
            Container.RegisterType<IConversionService, ConversionService>();
            Container.RegisterType<IShortestPathService, DijkstraAdapter>();
            Container.RegisterType<IDijkstraService<string>, DijkstraService<string>>();
        }
    }
}
