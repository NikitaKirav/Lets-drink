using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
	public class EFDbContext: DbContext
	{
		public EFDbContext()
			:base()
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ImageProduct> ImageProducts { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Shop> Shops { get; set; }
		public DbSet<CartLine> CartLines { get; set; }
		public DbSet<ShipingDetail> ShipingDetails { get; set; }
		public DbSet<CartLineSale> CartLineSales { get; set; }
	}
}
