using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Moq;
using Ninject;
using Domain.Concrete;
using System.Configuration;

namespace WebUI.Infrastucture
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		IKernel kernel;

		public NinjectDependencyResolver(IKernel kernelParameters)
		{
			kernel = kernelParameters;
			AddBindings();
		}

		private void AddBindings()
		{
			kernel.Bind<IProductRepository>().To<EFProductRepository>();

			kernel.Bind<IShopRepository>().To<EFShopRepository>();

			kernel.Bind<ICartRepository>().To<EFCartLineRepository>();

			kernel.Bind<ICartSellRepository>().To<EFCartLineSaleRepository>();

			kernel.Bind<IShipingDetailRepository>().To<EFShipingDetailRepository>();

			EmailSettings emailSettings = new EmailSettings
			{
				WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
			};

			kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
				.WithConstructorArgument("settings", emailSettings);
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}
	}
}