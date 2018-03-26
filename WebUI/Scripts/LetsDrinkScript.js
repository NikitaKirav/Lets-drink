// Delivery - способ доставки
var asCourier = document.querySelector(".delivery1");
var asYourself = document.querySelector(".delivery2");
var asPost = document.querySelector(".delivery3");
var postTable = document.querySelector(".post-table");
var courierTable = document.querySelector(".courier-table");
var youselfTable = document.querySelector(".youself-table");
var postCourierTable = document.querySelector(".post-courier-table");

if (asCourier) {
	asCourier.onclick = handleDeliveryClick('courier');
	asYourself.onclick = handleDeliveryClick('yourself');
	asPost.onclick = handleDeliveryClick('post');
	renderDelivery();
}

function handleDeliveryClick(delivery) {
	return function () {
		localStorage.setItem('delivery', delivery);
		renderDelivery();
	}
}

function renderDelivery() {
	applyButtonDelivery();
}

function itemDelivery() {
	return localStorage.getItem('delivery');
}

function applyButtonDelivery() {
	var deliver = itemDelivery();
	var methodDelivery = $("#ShipingDetails_TypeDelivery");
	switch (deliver) {
		case 'courier':
			removeClass(asYourself, 'action-delivery');
			removeClass(asPost, 'action-delivery');
			addClass(asCourier, 'action-delivery');
			removeClass(postTable, 'on-delivery');
			removeClass(youselfTable, 'on-delivery');
			addClass(courierTable, 'on-delivery');
			addClass(postCourierTable, 'on-delivery');
			methodDelivery.val("courier");
			break;
		case 'yourself':
			removeClass(asCourier, 'action-delivery');
			removeClass(asPost, 'action-delivery');
			addClass(asYourself, 'action-delivery');
			removeClass(postTable, 'on-delivery');
			removeClass(courierTable, 'on-delivery');
			removeClass(postCourierTable, 'on-delivery');
			addClass(youselfTable, 'on-delivery');
			methodDelivery.val("yourself");
			break;
		case 'post':
			removeClass(asCourier, 'action-delivery');
			removeClass(asYourself, 'action-delivery');
			addClass(asPost, 'action-delivery');
			addClass(postTable, 'on-delivery');
			addClass(postCourierTable, 'on-delivery');
			removeClass(courierTable, 'on-delivery');
			removeClass(youselfTable, 'on-delivery');
			methodDelivery.val("post");
			break;
	}
}

// Список Grid-List-View
	var asListBtn = document.querySelector(".as-list");
	var asGridBtn = document.querySelector(".as-grid");
	var products = document.querySelector(".container-product");

if (asListBtn) {
	asListBtn.onclick = handleButtonClick('list');
	asGridBtn.onclick = handleButtonClick('grid');
	render();
}

$("#ShipingDetails_Phone").mask("+7(999) 999-99-99");

function handleButtonClick(listView) {
	return function () {
		localStorage.setItem('listView', listView);
		render();
	}
}

function render() {
	applyProductSettings();
	applyButtonState();
}

function applyButtonState() {
	if (itemsAsGrid()) {
		removeClass(asListBtn, 'active');
		addClass(asGridBtn, 'active');
	}
	else {
		removeClass(asGridBtn, 'active');
		addClass(asListBtn, 'active');
	}
}

function itemsAsGrid() {
	return localStorage.getItem('listView') === 'grid';
}

function applyProductSettings() {
	itemsAsGrid() ? addClass(products, 'grid') : removeClass(products, 'grid');
}

function addClass(el, className = null) {
	if (el.classList)
		el.classList.add(className);
	else
		el.className += ' ' + className;
}

function removeClass(el, className = null) {
	if (el.classList)
		el.classList.remove(className);
	else
		el.className = el.className.replace(new RegExp(className, 'gi'), ' ');
}

// Изменение кол-ва товаров на листе
function onChangeDropDownList() {

	let setupRequest = [{
		method: 'ProductList',
		destination: '.container-product'
	},
	{
		method: 'PageLinks',
		destination: '.page-number'
	}];

	function makeRequest(value) {
		setupRequest.forEach(({
			method, destination
		}) => $.ajax({
				type: "GET",
				url: '/Products/' + method,
				data: { pageSize: value },
				success: data => $(destination).html(data),
				dataType: "html"
			}));
	}
	makeRequest($("#pageSize").val());

}

// Нажали Купить
function OnClickBuy(productId) {
	var quantity = -1;
	var quant = document.querySelector("#quantity");
	if (quant) {
		quantity = $("#quantity").val();
	}
		$.ajax({
			type: "GET",
			url: '/Products/AddToCart',
			data: { productId: productId, quantity: quantity  },
			success: function (data) {
				$('.cart-col-nav').html(data);
			},
			dataType: "html"
		});
}

// Нажали Удалить из корзины
function OnClickRemove(productId) {
	$.ajax({
		type: "GET",
		url: '/Products/RemoveToCart',
		data: { productId: productId },
		success: function (data) {
			$('.cart-col-nav').html(data);
		},
		dataType: "html"
	});
}

// Нажали Удалить из корзины со страницы Корзины
function OnClickRemove2(productId) {
	$.ajax({
		type: "GET",
		url: '/Cart/RemoveToCart',
		data: { productId: productId },
		success: function (data) {
			$('.details-cart').html(data);
		},
		dataType: "html"
	});
}

function OnChangeQuantity(productId, quantity) {
	$.ajax({
		type: "GET",
		url: 'ChangeToCart',
		data: { productId: productId, quantity: quantity },
		success: function (data) {
			$('.details-cart').html(data);
		},
		dataType: "html"
	});
}

// Разворачивание картинки из миниатюры в big icon
function ClickImageSmall(value) {
	$(".window-image").html("<img class='big-icon' src='" + value + "' />");
}

// Раскрытие ссылки
function look(type) {
		param = document.getElementById(type);
	if(param.style.display == "none") param.style.display = "block";
else param.style.display = "none"
}

