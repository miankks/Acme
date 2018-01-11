namespace Acme.Domain.Initialization
{
    using Acme.Models.Constants;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;
    using EPiServer.Web;

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DisplayOptionsInittialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var displayOptions = ServiceLocator.Current.GetInstance<DisplayOptions>();

            displayOptions.Add(RenderingTags.DisplayOptionTags.ThreeQuarters, "Three quarters", string.Empty, string.Empty, "epi-icon__layout  epi-icon__layout--three-quarters");
            displayOptions.Add(RenderingTags.DisplayOptionTags.TwoThirds, "Two thirds", string.Empty, string.Empty, "epi-icon__layout  epi-icon__layout--two-thirds");
            displayOptions.Add(RenderingTags.DisplayOptionTags.Half, "Half", string.Empty, string.Empty, "epi-icon__layout  epi-icon__layout--half");
            displayOptions.Add(RenderingTags.DisplayOptionTags.Third, "Third", string.Empty, string.Empty, "epi-icon__layout  epi-icon__layout--third");
            displayOptions.Add(RenderingTags.DisplayOptionTags.Quarter, "Quarter", string.Empty, string.Empty, "epi-icon__layout  epi-icon__layout--quarter");
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
