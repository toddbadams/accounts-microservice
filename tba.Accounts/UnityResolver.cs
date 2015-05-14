using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using tba.Accounts.Context;
using tba.Accounts.Entities;
using tba.accounts.Services;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;

namespace tba.Accounts
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer Container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            Container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<DbContext, AccountsDbContext>();
            container.RegisterType<IHashProvider, HashProvider>();
            container.RegisterType<ITimeProvider, DefaultTimeProvider>();
            container.RegisterType<IRepository<Account>, EFPersistence.EfRepository<Account>>();
            container.RegisterType<IAccountsService, AccountsService>();
            config.DependencyResolver = new UnityResolver(container);

            // Other Web API configuration not shown.
        }
    }
}
