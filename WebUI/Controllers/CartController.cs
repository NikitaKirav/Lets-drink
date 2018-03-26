using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.CommonMethods;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
		ProductListViewModel productList;
		EFProductRepository repoProduct;
		EFCartLineRepository repoCartLine;
		EFCartLineSaleRepository repoCartLineSale;
		EFShipingDetailRepository repoShipDetails;
		IOrderProcessor orderProcessor;
		CommonMethod cm;    // Библиотека Методов для контроллеров

		public CartController(EFProductRepository repoProduct, EFCartLineRepository repoCartLine, IOrderProcessor order, 
			CommonMethod common, EFCartLineSaleRepository repoCartLineSale, EFShipingDetailRepository repoShipDetails)
		{
			this.repoProduct = repoProduct;
			this.repoCartLine = repoCartLine;
			this.repoCartLineSale = repoCartLineSale;
			this.repoShipDetails = repoShipDetails;
			orderProcessor = order;
			cm = common;
		}

		// Главная страница корзины
		public ActionResult Index()
        {
			HttpCookie cartUserID = Request.Cookies["CartUserID"];

			return View(cm.ProductList(0, 0, cartUserID));
        }

		// Метод обработки заказа
		[HttpPost]
		public ActionResult CheckOut(ProductListViewModel list)
		{
			HttpCookie cartUserID = Request.Cookies["CartUserID"];
			string ex = null;

			//Важно! В list приходит только ShipingDetails(перебрасывать корзину нет смысла,т.к. корзина тяжелая)
			//но нам для оформления заказа требуется Carts(корзина товаров), которую мы соберем на сервере по GUID пользователя
			//и сформируем новый продуктовый лист productList
			productList = cm.ProductList(0, 0, cartUserID, list.ShipingDetails);

			if (productList.Carts.Count() == 0)
			{
				ModelState.AddModelError("", "Извините, корзина пуста");
			}

			if (ModelState.IsValid)
			{
				int lastOrderId = repoCartLineSale.LastOrderId();   // Вытаскивает номер заказа из базы данных с максимальным номером

				productList.ShipingDetails.OrderId = lastOrderId + 1;   // Изменим номер заказа
				productList.ShipingDetails.CustomerId = cartUserID.Value;   // Изменяем CustomerId

				orderProcessor.ProcessOrder(productList.Carts, productList.ShipingDetails);
				
				repoShipDetails.SaveShipingDetails(productList.ShipingDetails);

				// Перебираем корзину товаров: записываем товары в хранилище заказанных и убираем товары из временного хранилища
				foreach (CartLine cartLine in productList.Carts)
				{
					ex = repoCartLineSale.SaveOrderLine(new CartLineSale
					{
						CustomerId = cartUserID.Value,
						OrderId = lastOrderId + 1,
						Date = DateTime.Now,
						Product = cartLine.Product,
						Quantity = cartLine.Quantity

					});
					if (ex == null) // Если ошибок при записи нет, то удаляем строчку из временного хранилища корзины
					{
						repoCartLine.RemoveCart(cartLine);
					}
				}
				return View("Completed");
			}
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", ex);
			}
			return View("Index", cm.ProductList(0, 0, cartUserID));
		}

		// Удаление из корзины
		[HttpGet]
		public ActionResult RemoveToCart(int productId)
		{
			HttpCookie cartUserID = Request.Cookies["CartUserID"];

			CartLine cartLine = new CartLine { CustomerId = cartUserID.Value, Product = repoProduct.FindProduct(productId) };

			if (cartLine != null)
			{
				repoCartLine.RemoveCart(cartLine);
			}

			return PartialView("_CartTable", new ProductListViewModel
			{
				Carts = repoCartLine.CartLines.Where(c => c.CustomerId == cartUserID.Value).ToArray()
			});
		}

		// Внести изменения в кол-ве товаров в корзине
		[HttpGet]
		public ActionResult ChangeToCart(int productId, int quantity)
		{
			HttpCookie cartUserID = Request.Cookies["CartUserId"];

			CartLine cartLine = new CartLine { CustomerId = cartUserID.Value, Product = repoProduct.FindProduct(productId), Quantity = quantity };

			if (cartLine != null)
			{
				repoCartLine.SaveCart(cartLine);
			}

			return PartialView("_CartTable", new ProductListViewModel
			{
				Carts = repoCartLine.CartLines.Where(c => c.CustomerId == cartUserID.Value).ToArray()
			});
		}

	}
}