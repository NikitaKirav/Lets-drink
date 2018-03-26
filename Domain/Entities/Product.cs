using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Product
	{
		public Product()
		{
			this.CartLines = new HashSet<CartLine>();
			this.Image = new List<ImageProduct>();
		}

		public int ProductId { get; set; }
		public string Article { get; set; }
		public string Name { get; set; }
		public string Mark { get; set; }
		public string Country { get; set; }
		public string Description { get; set; }
		public string Group { get; set; }
		public decimal Price { get; set; }

		public virtual ICollection<CartLine> CartLines { get; set; }
		public virtual List<ImageProduct> Image { get; set; }
	}
}
