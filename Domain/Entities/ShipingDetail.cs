using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ShipingDetail
	{
		public int Id { get; set; }

		public string CustomerId { get; set; }

		public int OrderId { get; set; }

		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
		[Required(ErrorMessage = "Укажите Email")]
		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Укажите телефон")]
		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Укажите имя")]
		[StringLength(100, ErrorMessage = "Не может превышать 100 символов")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите регион доставки")]
		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string State { get; set; }

		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string City { get; set; }

		[StringLength(20, ErrorMessage = "Не может превышать 20 символов")]
		public string TypeDelivery { get; set; }

		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string Address { get; set; }

		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string Apartment { get; set; }

		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string Entrance { get; set; }

		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string Doorphone { get; set; }

		[StringLength(50, ErrorMessage = "Не может превышать 50 символов")]
		public string Floor { get; set; }

		[StringLength(100, ErrorMessage = "Не может превышать 100 символов")]
		public string Comment { get; set; }

		[StringLength(30, ErrorMessage = "Не может превышать 30 символов")]
		public string Indecs { get; set; }
	}
}
