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
	/// Хранилище товаров интернет магазина
	/// </summary>
	public class EFProductRepository : IProductRepository
	{
		EFDbContext context = new EFDbContext();

		public IEnumerable<Product> Products
		{
			get { return context.Products; }
		}


		/// <summary>
		/// Находит в базе данных Товар с нужным ID
		/// </summary>
		/// <param name="id">Id номер товара(int)</param>
		/// <returns>Product</returns>
		public Product FindProduct(int id)
		{
			return context.Products
				.Where(x => x.ProductId == id).First();
		}

		/// <summary>
		/// Возвращает список товаров на указанной странице
		/// </summary>
		/// <param name="page">номер страницы</param>
		/// <param name="pageSize">кол-во товаров на странице</param>
		/// <returns>IEnumerable/<Product/></returns>
		public IEnumerable<Product> ProductsOnPage(int page, int pageSize) 
		{
			return context.Products
						.OrderBy(g => g.ProductId)
						.Skip((page - 1) * pageSize)
						.Take(pageSize).ToArray();
		}

		/// <summary>
		/// Возвращает кол-во продукции в базе данных
		/// </summary>
		/// <returns>int</returns>
		public int ProductsCount()
		{
			return context.Products.Count();
		}
	}
}
