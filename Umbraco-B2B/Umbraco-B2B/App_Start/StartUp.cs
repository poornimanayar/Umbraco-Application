namespace Umbraco_Application
{
    using System.Linq;
    using System.Web.Optimization;

    using umbraco.presentation;
    using  Umbraco.Web;
    using Umbraco.Core;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Models.ContentEditing;

    using Umbraco_Application.ContentModels;
    using UmbracoContext = Umbraco.Web.UmbracoContext;

    /// <summary>
    /// The event handler.
    /// </summary>
    public class EventHandler : IApplicationEventHandler
    {
        /// <summary>
        /// ApplicationContext is created and other static objects that require initialization have been setup
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            
        }

        /// <summary>
        /// All resolvers have been initialized but resolution is not frozen so they can be modified in this method
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Bootup is completed, this allows you to perform any other bootup logic required for the application.
        /// Resolution is frozen so now they can be used to resolve instances.
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
           EditorModelEventManager.SendingContentModel += this.EditorModelEventManager_SendingContentModel;
        }

        private void EditorModelEventManager_SendingContentModel(System.Web.Http.Filters.HttpActionExecutedContext sender, EditorModelEventArgs<Umbraco.Web.Models.ContentEditing.ContentItemDisplay> e)
        {
            var contentItemDisplay = e.Model;
            var context = e.UmbracoContext;

           MakePropertiesReadOnly(contentItemDisplay, context);
        }

        private void MakePropertiesReadOnly(ContentItemDisplay contentItemDisplay, UmbracoContext context)
        {
            var usergroups = context.Security.CurrentUser.Groups.ToList();

            if (contentItemDisplay.ContentTypeAlias == HomePage.ModelTypeAlias && !usergroups.Exists(x => x.Alias == "admin"))
            {
                var settingsTab = contentItemDisplay.Tabs.FirstOrDefault(x => x.Label == "Settings");

                if (settingsTab != null)
                {
                    settingsTab.Properties.First(x => x.Alias == "brandName").View = "readonlyvalue";
                    settingsTab.Properties.First(x => x.Alias == "isResellerSite").View = "readonlyvalue";
                }

            }
        }
    }
}
