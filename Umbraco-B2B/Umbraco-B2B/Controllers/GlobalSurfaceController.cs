namespace Umbraco_Application.Controllers
{
    using System.Web.Mvc;

    using Umbraco.Web;
    using Umbraco.Web.Mvc;

    using Umbraco_Application.ContentModels;

    using Umbraco_Application.SiteConstants;

    public class GlobalSurfaceController : SurfaceController
    {
        // GET: GlobalSurface
        public ActionResult GetBrandSpecificStyleBundle()
        {
            var homePage = this.CurrentPage.AncestorOrSelf(1).OfType<HomePage>();
            var brandName = homePage.BrandName;
            var assetFolder = brandName.ToLower().Replace(" ", string.Empty);
            return this.PartialView(SiteConstants.PartialViewPath + "BrandSpecificStyles.cshtml", assetFolder);
        }

        public ActionResult GetHeaderNavigation()
        {
            var homePage = this.CurrentPage.AncestorOrSelf(1).OfType<HomePage>();
            var headerNav = homePage.HeaderNavigation;
           return this.PartialView(SiteConstants.PartialViewPath + "HeaderNavigation.cshtml", headerNav);
        }

        public ActionResult GetSiteName()
        {
            var homePage = this.CurrentPage.AncestorOrSelf(1).OfType<HomePage>();
            var siteName = homePage.SiteName;
            return this.PartialView(SiteConstants.PartialViewPath + "SiteName.cshtml", siteName);
        }
    }
}