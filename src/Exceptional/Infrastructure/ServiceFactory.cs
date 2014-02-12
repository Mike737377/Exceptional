using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Exceptional.Infrastructure
{
    public static class ServiceFactory
    {
        private static bool initialised = false;
        private static object initLock = new object();

        public static void Setup()
        {
            if (initialised) return;
            lock (initLock)
            {
                if (initialised) return;
                ObjectFactory.Initialize(x =>
                {
                    x.Scan(scanner =>
                    {
                        //scanner.TheCallingAssembly();
                        scanner.AssemblyContainingType(typeof(ServiceFactory));
                        //scanner.AddAllTypesOf(typeof(IHandler<>));

                        scanner.WithDefaultConventions();
                        scanner.ConnectImplementationsToTypesClosing(typeof(IHandler<>));
                        //scanner.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                        //scanner.RegisterConcreteTypesAgainstTheFirstInterface();
                    });
                    //x.For(typeof(IRepository<>)).Singleton().Use(typeof(PoorMansRepository<>));
                    //x.For(typeof(IDataContext)).HybridHttpOrThreadLocalScoped();
                    x.For<IApplicationBus>().Singleton().Use<ApplicationBus>();
                    x.For<HttpContextBase>().HttpContextScoped().Use(() => new HttpContextWrapper(HttpContext.Current));
                });

                initialised = true;
            }
        }

        public static void ReleaseAndDisposeAllHttpScopedObjects()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        public static T GetInstance<T>()
        {
            if (!initialised) { Setup(); }
            return ObjectFactory.GetInstance<T>();
        }
    }
}