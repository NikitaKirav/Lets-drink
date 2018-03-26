using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain.Entities;

namespace Domain.Abstract
{
	public interface ICartRepository
	{
		IEnumerable<CartLine> CartLines { get; }
		void SaveCart(CartLine cartLine);
		void RemoveCart(CartLine cartLine);
	}
}
