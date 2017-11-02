using HomeWork.Models;
using System.Collections.Generic;
using System.Linq;


namespace System.Web.Mvc
{
    public static class ListHelper
    {
        //experemental
        public static MvcHtmlString ListUsers(this HtmlHelper html, IEnumerable<UserInfo> users, object htmlAttributes = null)
        {
            string htmlOutput = string.Empty;

            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("nav list-group");

            if (users.Count() > 0)
            {
                foreach (UserInfo item in users)
                {
                    TagBuilder li = new TagBuilder("li");
                    li.AddCssClass("list-group-item");
                    TagBuilder div = new TagBuilder("div");
                    div.AddCssClass("checkbox");
                    TagBuilder span = new TagBuilder("div");
                    span.AddCssClass("glyphicon glyphicon - user");
                    li.InnerHtml += span.ToString();
                    li.SetInnerText(item.FirstName);
                    li.SetInnerText(item.LastName);
                    li.InnerHtml += div.ToString();
                    ul.InnerHtml += li.ToString();
                   
                }
                ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            return MvcHtmlString.Create(ul.ToString());
        }
    }
}