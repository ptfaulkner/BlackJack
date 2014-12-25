using Microsoft.Practices.Unity;
using System.Web.Http;
using Blackjack.Web.WebSockets;
using Unity.WebApi;

namespace Blackjack.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ISocketService, BlackjackSocketService>();
            container.RegisterType<Handler>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}