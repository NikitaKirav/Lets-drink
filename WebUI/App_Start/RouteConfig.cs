﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				null,
				"",
				new { controller = "Products", action = "List", page = 1 }
			);


			routes.MapRoute(
				name: null,
				url: "page{page}",
				defaults: new { controller = "Products", action = "List" },
				constraints: new { page = @"\d+" }
            );

			routes.MapRoute(
				null,
				"{controller}/{action}"
			);
		}
    }
}
