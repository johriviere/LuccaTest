using Application.Validation;
using Application.Validation.Console;
using Domain.Service;
using Domain.Service.Dijkstra;
using System.Collections.Generic;
using Unity;

namespace Ioc
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
            Container.RegisterType<IConversionService, ConversionService>();
            Container.RegisterType<IValidationService<IEnumerable<string>>, ValidationService>();
            Container.RegisterType<IDijkstraService, DijkstraService>();
        }
    }
}
