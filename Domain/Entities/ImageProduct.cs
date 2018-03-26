using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ImageProduct
	{
		public int ImageProductId { get; set; }
		public Product Product { get; set; }
		public bool ImageMainPath { get; set; }
		public string ImagePath { get; set; }
		public string ImagePathMini { get; set; }
	}
}
