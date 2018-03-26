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
	/// Хранилище проданных товаров
	/// </summary>
	public class EFCartLineSaleRepository : ICartSellRepository
	{
		EFDbContext context = new EFDbContext();

		public IEnumerable<CartLineSale> CartLineSales
		{
			get { return context.CartLineSales; }
		}
		/// <summary>
		/// Получает Id номер последнего заказа в базе данных
		/// </summary>
		/// <returns>int (Id номер последнего заказа)</returns>
		public int LastOrderId()
		{
			//int test = context.CartLineSales.DefaultIfEmpty
			int orderId = context.CartLineSales.Select(x => x.OrderId).DefaultIfEmpty(0).Max();
			//if (orderId == null) { orderId = 0; }
			return (int)orderId;
		}

		/// <summary>
		/// Сохраняет строчку заказанной продукции в базе данных
		/// </summary>
		/// <param name="cartLineSale">Объект типа CartLineSale</param>
		/// <returns>Возвращает null или ошибку при записи</returns>
		public string SaveOrderLine(CartLineSale cartLineSale)
		{
			try
			{
				Product productTable = context.Products.FirstOrDefault(c => c.ProductId == cartLineSale.Product.ProductId);
				context.CartLineSales.Add(new CartLineSale
				{
					OrderId = cartLineSale.OrderId,
					CustomerId = cartLineSale.CustomerId,
					Quantity = cartLineSale.Quantity,
					Product = productTable,
					Date = DateTime.Now
				});
				context.SaveChanges();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return null;
		}
	}
}
