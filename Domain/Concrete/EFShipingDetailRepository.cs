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
	/// Хранилище деталей заказа (Имя, Адрес, Телефон и пр.)
	/// </summary>
	public class EFShipingDetailRepository : IShipingDetailRepository
	{
		EFDbContext context = new EFDbContext();
		public IEnumerable<ShipingDetail> ShipingDetails
		{
			get { return context.ShipingDetails; }
		}

		/// <summary>
		/// Сохраняет сведения о заказе в базе данных
		/// </summary>
		/// <param name="shipingDetail">Объект типа ShipingDetails - детали заказа</param>
		/// <returns>Возвращает null, если запись прошла успешно, в противном случае - ошибку</returns>
		public string SaveShipingDetails(ShipingDetail shipingDetail)
		{
			try
			{
				context.ShipingDetails.Add(shipingDetail);
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
