namespace Umbraco_Application.Controllers
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;

    using Umbraco_Application.ContentModels;
    using Umbraco_Application.Models.ViewModels;
    using Umbraco_Application.SiteConstants;

    public class ProfileSurfaceController : SurfaceController
    {
        public ActionResult RenderLogin()
        {
            return this.PartialView(SiteConstants.PartialViewPath + "LoginPartial.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var currentMember = this.Services.MemberService.GetByUsername(viewModel.Email);
                    var homePage = this.CurrentPage.AncestorOrSelf(1).OfType<HomePage>();
                    var brandName = homePage.BrandName;
                    var allowedRoles = brandName;
                    if (currentMember == null)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user does not exist.");
                        return this.CurrentUmbracoPage();
                    }

                    if (currentMember.IsLockedOut)
                    {
                        this.ModelState.AddModelError(
                            string.Empty,
                            "Account locked out. Please contact our administrator.");
                        return this.CurrentUmbracoPage();
                    }

                    if (!Roles.IsUserInRole(viewModel.Email, allowedRoles))
                    {
                        this.ModelState.AddModelError(string.Empty, "The user does not exist.");
                        return this.CurrentUmbracoPage();
                    }

                    var loggedIn = this.Members.Login(viewModel.Email, viewModel.Password);
                    if (loggedIn)
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                        var userCookie =
                            new HttpCookie("logincookie-" + brandName)
                            {
                                Value = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(currentMember.Username), "ProtectCookie")),
                                Expires = DateTime.Now.AddMinutes(180),
                                Domain = this.Request.Url.Host
                            };
                        this.Response.Cookies.Add(userCookie);
                        var redirectUrl = homePage.FirstChild<QueryPage>().Url;
                        return this.Redirect(redirectUrl);
                    }

                    this.ModelState.AddModelError(string.Empty, "Wrong username or password entered.");
                    return this.CurrentUmbracoPage();
                }

                return this.CurrentUmbracoPage();
            }
            catch (Exception exception)
            {
                LogHelper.Error<ProfileSurfaceController>("error while logging in", exception);
                return this.CurrentUmbracoPage();
            }
        }

        public ActionResult HandleLogout()
        {
            var homePage = this.CurrentPage.AncestorOrSelf(1).OfType<HomePage>();
            var brandName = homePage.BrandName;
            var cookieName = "logincookie-" + brandName;
            this.TempData.Clear();
            this.Session.Clear();
            FormsAuthentication.SignOut();
            if (this.Request.Cookies[cookieName] != null)
            {
                this.Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
            }

            return this.Redirect("/");
        }
    }
}