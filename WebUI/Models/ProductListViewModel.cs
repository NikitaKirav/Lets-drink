using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
	public class ProductListViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public PageInfo PageInfo { get; set; }
		public IEnumerable<CartLine> Carts { get; set; }
		public ShipingDetail ShipingDetails { get; set; }
		public Product Product { get; set; }
	}
}