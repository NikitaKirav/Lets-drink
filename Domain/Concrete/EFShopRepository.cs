using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
	/// <summary>
	/// Хранилище точек продаж магазина Lets-drink
	/// </summary>
	public class EFShopRepository : IShopRepository
	{
		EFDbContext context = new EFDbContext();

		public IEnumerable<Shop> Shops
		{
			get { return context.Shops; }
		}
	}
}
