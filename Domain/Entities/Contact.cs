using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Contact
	{
		public int ContactId { get; set; }
		public string PhoneMain { get; set; }
		public string Email { get; set; }
		public string Skype { get; set; }
		public string WebSait { get; set; }
	}

	public class Shop
	{
		public int ShopId { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Skype { get; set; }
		public string WebSait { get; set; }
		public string Address { get; set; }
	}
}
