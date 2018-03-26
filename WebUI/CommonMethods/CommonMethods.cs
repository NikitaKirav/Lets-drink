using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.CommonMethods
{
	/// <summary>
	/// Класс для хранения общих методов Контроллеров
	/// </summary>
	public class CommonMethod
	{
// ------------------------------------------------------- Методы в помощь!
		EFProductRepository repoProduct;
		EFCartLineRepository repoCartLine;

		public CommonMethod(EFProductRepository repoProduct, EFCartLineRepository repoCartLine)
		{
			this.repoProduct = repoProduct;
			this.repoCartLine = repoCartLine;
		}

		/// <summary>
		/// Возвращает экземпляр класса ProductListViewModel на основании переданных параметров
		/// </summary>
		/// <param name="pageSize">Кол-во товаров на странице (int)</param>
		/// <param name="page">Кол-во страниц (int)</param>
		/// <param name="cartUserID">Уникальный GUID номер клиента (HttpCookie)</param>
		/// <param name="shipingDetail">Детальная информация по заказу (ShipingDetail)</param>
		/// <param name="product">Детальная информация о конкретном товаре (Product)</param>
		/// <returns>ProductListViewModel</returns>
		public ProductListViewModel ProductList(int pageSize, int page, HttpCookie cartUserID = null, ShipingDetail shipingDetail = null, Product product = null)
		{
			return new ProductListViewModel()
				{
					PageInfo = new PageInfo
					{
						CurrentPage = page,
						ProductCount = repoProduct.ProductsCount(),
						PageSize = pageSize
					},

					Products = repoProduct.ProductsOnPage(page, pageSize),

					Carts = repoCartLine.FindCartCustomer(cartUserID == null ? "0" : cartUserID.Value),

					ShipingDetails = shipingDetail,

					Product = product

				};

		}

	}
}