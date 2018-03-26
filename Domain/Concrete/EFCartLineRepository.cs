using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
	/// <summary>
	/// Временное хранилище корзин пользователей (p.s: Должно очищаться ~ раз в месяц)
	/// </summary>
	public class EFCartLineRepository : ICartRepository
	{
		EFDbContext context = new EFDbContext();

		public IEnumerable<CartLine> CartLines
		{
			get { return context.CartLines; }
		}

		/// <summary>
		/// Записывает изменения в Корзине в таблицу базы данных
		/// </summary>
		/// <param name="cartLine">Объект типа CartLine - строка в списке корзины</param>
		public void SaveCart(CartLine cartLine)
		{
			CartLine line = context.CartLines.FirstOrDefault(c => c.Product.ProductId == cartLine.Product.ProductId
																	&& c.CustomerId == cartLine.CustomerId);
			if (line == null)
			{
				Product productTable = context.Products.FirstOrDefault(c => c.ProductId == cartLine.Product.ProductId);
				context.CartLines.Add(new CartLine
				{
					CustomerId = cartLine.CustomerId,
					Quantity = 1,
					Product = productTable,
					Date = DateTime.Now
				});
			}
			else
			{
				if (cartLine.Quantity == -1) // Если (Quantity = -1), то (Quantity += 1)
				{
					line.Quantity += 1;
				}
				else
				{
					line.Quantity = cartLine.Quantity;
				}
			}
			context.SaveChanges();
		}

		/// <summary>
		/// Записывает изменения в базу данных о удалении строки товара из Корзины
		/// </summary>
		/// <param name="cartLine">Объект типа CartLine</param>
		public void RemoveCart(CartLine cartLine)
		{
			CartLine line = context.CartLines.FirstOrDefault(c => c.Product.ProductId == cartLine.Product.ProductId
																	&& c.CustomerId == cartLine.CustomerId);
			if (line != null)
			{
				context.CartLines.Remove(line);
			}
			context.SaveChanges();
		}

		/// <summary>
		/// Ищет корзину клиента по его уникальному GUID номеру
		/// </summary>
		/// <param name="cartUserID">GUID номер клиента (string)</param>
		/// <returns>IEnumerable/<CartLine/></returns>
		public IEnumerable<CartLine> FindCartCustomer(string cartUserID)
		{
			return CartLines.Where(c => c.CustomerId == (cartUserID == null ? "0" : cartUserID)).ToArray();
		}
	}
}
