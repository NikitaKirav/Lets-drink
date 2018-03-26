using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;

namespace Domain.Entities
{
	public class CartLine
	{
		public int CartLineId { get; set; }
		public string CustomerId { get; set; }
		public virtual Product Product { get; set; }
		public int Quantity { get; set; }
		public DateTime Date { get; set; }
	}
}
