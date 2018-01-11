namespace Acme.Domain.Initialization
{
    using System.Web.Mvc;
    using Acme.Domain.DependencyResolution;
    using EPiServer.Cms.Shell.UI.Rest;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;
    using EPiServer.Web;
    using EPiServer.Web.Mvc.Html;
    using PropertyRenderers;

    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class ContainerConfigurationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.RemoveAll<IContentTypeAdvisor>();

            context.Services.RemoveAll<UrlSegmentOptions>();

            context.Services.AddSingleton(s => new UrlSegmentOptions
            {
                UseLowercase = true,
                SupportIriCharacters = true,
                ValidCharacters = @"\p{L}0-9\-_~\.\$"
            });

            var container = context.StructureMap();
            container.Configure(x => x.For<ContentAreaRenderer>().Use<SiteContentAreaRenderer>());
            container.Configure(x => x.For<PropertyRenderer>().Use<SitePropertyRenderer>());

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }

        /// <summary>
        ///     Exists because IConfigurableModule, not used at present time
        /// </summary>
        /// <param name="context">the InitializationEngine context</param>
        public void Initialize(InitializationEngine context)
        {
        }

        /// <summary>
        ///     Exists because IConfigurableModule, not used at present time
        /// </summary>
        /// <param name="context">the InitializationEngine context</param>
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
