using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Models;
using WebUI.CommonMethods;

namespace WebUI.Controllers
{
	public class ProductsController : Controller
	{
		EFProductRepository repoProduct;
		EFCartLineRepository repoCartLine;
		CommonMethod cm;	// Библиотека Методов для контроллеров

		public ProductsController(EFProductRepository repoProduct, EFCartLineRepository repoCartLine, CommonMethod common)
		{
			this.repoProduct = repoProduct;
			this.repoCartLine = repoCartLine;
			cm = common;
		}

		// Детальная информация о товаре
		public ViewResult Details(int id)
		{
			HttpCookie cartUserID = Request.Cookies["CartUserID"];
			return View(cm.ProductList(0, 0, cartUserID, null, repoProduct.FindProduct(id)));
		}

		// Страница со списком всех товаров
		public ViewResult List(int page = 1, int pageSize = 6)
		{
			HttpCookie cartUserID = Request.Cookies["CartUserID"];

			if (Session["pageSize"] != null)
			{
				pageSize = (int)Session["pageSize"];
			}

			return View(cm.ProductList(pageSize, page, cartUserID));
		}

		// Коррекция списка товаров, через Ajax запрос
		[HttpGet]
		public ActionResult ProductList(int pageSize = 6)
		{
			Session["pageSize"] = pageSize;

			return PartialView("_Products", cm.ProductList(pageSize, 1));
		}

		// Коррекция кол-ва страниц, через Ajax запрос
		[HttpGet]
		public ActionResult PageLinks(int pageSize = 6)
		{
			return PartialView("_PageLinks", cm.ProductList(pageSize, 1));
		}

		// Добавление в корзину единицы товара
		[HttpGet]
		public ActionResult AddToCart(int productId, int quantity = -1)
		{
			HttpCookie cartUserID = Request.Cookies["CartUserID"];

			if (cartUserID == null) // Если Cookies CartUserID у пользователя нет, то добавляем
			{
				Guid customerId = Guid.NewGuid();
				cartUserID = new HttpCookie("CartUserID");
				cartUserID.Value = customerId.ToString();
				Response.Cookies.Add(cartUserID);
				cartUserID = Request.Cookies["CartUserID"]; //!!! В конце следует обязательно заново назначить переменной cartUserID кукки
			}

			CartLine cartLine = new CartLine { CustomerId = cartUserID.Value, Product = repoProduct.FindProduct(productId), Quantity = quantity }; // (Quantity = -1)  = (Quantity += 1)

			if (cartLine != null)
			{
				repoCartLine.SaveCart(cartLine);
			}

			return PartialView("_CartColNavTemp", new ProductListViewModel {
				Carts = repoCartLine.CartLines.Where(c => c.CustomerId == cartUserID.Value).ToArray()
			});
		}

		// Удаляем из корзины строчку товара
		[HttpGet]
		public ActionResult RemoveToCart(int productId)
		{
			HttpCookie cartUserID = Request.Cookies["CartUserID"];

			CartLine cartLine = new CartLine { CustomerId = cartUserID.Value, Product = repoProduct.FindProduct(productId) };

			if (cartLine != null)
			{
				repoCartLine.RemoveCart(cartLine);
			}

			return PartialView("_CartColNavTemp", new ProductListViewModel
			{
				Carts = repoCartLine.CartLines.Where(c => c.CustomerId == cartUserID.Value).ToArray()
			});
		}

	}
}