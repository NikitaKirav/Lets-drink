using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
	public class PageInfo
	{
		public int ProductCount { get; set; }	// Кол-во товара
		public int PageSize { get; set; }		// Кол-во товара на странице
		public int CurrentPage { get; set; }	// Текущая станица
		public int PageCount { get { return (int)Math.Ceiling((decimal)ProductCount / PageSize); } }
	}
}