using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using WebUI.Models;

namespace WebUI.HtmlHelpers
{
	public static class PagingHelpers
	{
		public static MvcHtmlString PageLink(this HtmlHelper html, PageInfo pageInfo, Func<int, string> url)
		{
			StringBuilder result = new StringBuilder();
			for (int page=1; page <= pageInfo.PageCount; page++)
			{
				TagBuilder tag = new TagBuilder("a");
				tag.InnerHtml = page.ToString();
				tag.MergeAttribute("href", url(page));
				
				if (pageInfo.CurrentPage == page)
				{
					tag.AddCssClass(" btn-activ");
				}
				tag.AddCssClass("btn-link");
				result.Append(tag.ToString());
			}
			return MvcHtmlString.Create(result.ToString());
		}
	}
}