using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
	public interface IShipingDetailRepository
	{
		IEnumerable<ShipingDetail> ShipingDetails { get; }

		/// <summary>
		/// Сохраняет сведения о заказе в базе данных
		/// </summary>
		/// <param name="shipingDetail">Объект типа ShipingDetails - детали заказа</param>
		/// <returns>Возвращает null, если запись прошла успешно, в противном случае - ошибку</returns>
		string SaveShipingDetails(ShipingDetail shipingDetail);
	}
}
