namespace Umbraco_Application.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;

    using Umbraco_Application.ContentModels;
    using Umbraco_Application.Models.ViewModels;
    using Umbraco_Application.SiteConstants;

    public class QuerySurfaceController : SurfaceController
    {
        public ActionResult RenderQuery()
        {
            var homePage = this.CurrentPage.AncestorOrSelf(1).OfType<HomePage>();
            var categoryNodes = homePage.Descendants("Category");
            var categories = categoryNodes.Select(category => new SelectListItem() { Text = category.Name, Value = category.GetKey().ToString() }).ToList();
             var currentMember = this.Members.GetCurrentMember();
            var currentMemberProducts = currentMember.GetPropertyValue<IEnumerable<IPublishedContent>>("availableProducts");
            var products = currentMemberProducts.Select(product => new SelectListItem() { Text = product.Name, Value = product.GetKey().ToString() }).ToList();
            var viewModel = new QueryViewModel() {MemberId = currentMember.GetKey(), MemberName = currentMember.Name, MemberEmail = this.Members.CurrentUserName, Categories = new SelectList(categories, "Value","Text"), Products = new SelectList(products, "Value", "Text"), IsReseller = homePage.IsResellerSite};
            return this.PartialView(SiteConstants.PartialViewPath + "Query.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitQuery(QueryViewModel viewModel)
        {
            this.ViewData["Message"] = "Thank you";
            return this.CurrentUmbracoPage();
           }
    }
}