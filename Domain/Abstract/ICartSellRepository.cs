using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
	public interface ICartSellRepository
	{
		IEnumerable<CartLineSale> CartLineSales { get; }

		/// <summary>
		/// Получает Id номер последнего заказа в базе данных
		/// </summary>
		/// <returns>int (Id номер последнего заказа)</returns>
		int LastOrderId();

		/// <summary>
		/// Сохраняет строчку заказанной продукции в базе данных
		/// </summary>
		/// <param name="cartLineSale">Объект типа CartLineSale</param>
		/// <returns>Возвращает null или ошибку при записи</returns>
		string SaveOrderLine(CartLineSale cartLineSale);
	}
}
