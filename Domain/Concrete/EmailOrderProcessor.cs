using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
	public class EmailSettings
	{
		public string MailToAddress = "order@example.com";
		public string MailFromAddress = "lets-drink@example.com";
		public bool UseSsl = true;
		public string Username = "MySmtpUsername";
		public string Password = "MySmtpPassword";
		public string ServerName = "smtp.example.com";
		public int ServerPort = 587;
		public bool WriteAsFile = true;
		public string FileLocation = @"c:\Lets-drink_email";
	}

	public class EmailOrderProcessor : IOrderProcessor
	{
		private EmailSettings emailSettings;

		public EmailOrderProcessor(EmailSettings settings)
		{
			emailSettings = settings;
		}

		public void ProcessOrder(IEnumerable<CartLine> cart, ShipingDetail shipingDetail)
		{
			using (var smtpClient = new SmtpClient())
			{
				smtpClient.EnableSsl = emailSettings.UseSsl;
				smtpClient.Host = emailSettings.ServerName;
				smtpClient.Port = emailSettings.ServerPort;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

				if (emailSettings.WriteAsFile)
				{
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
					smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
					smtpClient.EnableSsl = false;
				}

				StringBuilder body = new StringBuilder()
					.AppendFormat("Новый заказ № {0} Обработан", shipingDetail.OrderId)
					.AppendLine("---")
					.AppendLine("Товары:");

				int i = 1;
				foreach (var line in cart)
				{
					var subtotal = line.Product.Price * line.Quantity;
					body.AppendFormat("{0}. {1} - {2} x {3} (итого: {4:c})", i, line.Product.Name, line.Quantity, line.Product.Price, subtotal);
					body.Append(Environment.NewLine);
					i++;
				}

				body.Append(Environment.NewLine);
				body.AppendLine("Информация о клиенте:\r\n")
					.AppendFormat("Имя: {0}\r\n", shipingDetail.Name)
					.AppendFormat("Телефон: {0}\r\n", shipingDetail.Phone)
					.AppendFormat("Email: {0}\r\n", shipingDetail.Email)
					.AppendFormat("Тип доставки: {0}\r\n", shipingDetail.TypeDelivery)
					.AppendFormat("Область: {0}\r\n", shipingDetail.State)
					.AppendFormat("Город: {0}\r\n", shipingDetail.City)
					.AppendFormat("Адрес(Улица, Дом, Корпус): {0}\r\n", shipingDetail.Address)
					.AppendFormat("Подъезд: {0}\r\n", shipingDetail.Entrance)
					.AppendFormat("Квартира: {0}\r\n", shipingDetail.Apartment)
					.AppendFormat("Домофон: {0}\r\n", shipingDetail.Doorphone)
					.AppendFormat("Комментарий курьеру: {0}\r\n", shipingDetail.Comment)
					.AppendFormat("Индекс: {0}\r\n", shipingDetail.Indecs);


				MailMessage mailMessage = new MailMessage(
				emailSettings.MailFromAddress,
				emailSettings.MailToAddress,
				"Новый заказ отправлен",
				body.ToString()
				);

				if (emailSettings.WriteAsFile)
				{
					mailMessage.BodyEncoding = Encoding.UTF8;
				}

				smtpClient.Send(mailMessage);
			}
		}
	}
}
