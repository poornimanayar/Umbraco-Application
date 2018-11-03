namespace Umbraco_Application.Controllers
{
    using System.Web.Mvc;

    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    public class HomePageController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            if (this.Members.IsLoggedIn())
            {
                var redirectUrl = model.Content.FirstChild(c => c.DocumentTypeAlias == "queryPage").Url;
                return this.Redirect(redirectUrl);
            }
            return base.Index(model);
        }
    }
}