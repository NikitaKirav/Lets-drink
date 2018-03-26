using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class CartLineSale
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public string CustomerId { get; set; }
		public virtual Product Product { get; set; }
		public int Quantity { get; set; }
		public DateTime Date { get; set; }
	}
}
