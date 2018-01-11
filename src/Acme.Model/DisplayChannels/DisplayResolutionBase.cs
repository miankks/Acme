namespace Acme.Model.DisplayChannels
{
    using EPiServer.Framework.Localization;
    using EPiServer.ServiceLocation;
    using EPiServer.Web;

    /// <summary>
    ///     Base class for all resolution definitions
    /// </summary>
    public abstract class DisplayResolutionBase : IDisplayResolution
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisplayResolutionBase"/> class.
        /// </summary>
        /// <param name="name">The name of the resolution definition.</param>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        protected DisplayResolutionBase(string name, int width, int height)
        {
            this.Id = this.GetType().FullName;
            this.Name = Translate(name);
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        ///     Gets the unique ID for this resolution
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        ///     Gets the name of resolution
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     Gets the resolution width in pixels
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        ///     Gets the resolution height in pixels
        /// </summary>
        public int Height { get; protected set; }

        private static string Translate(string resourceKey)
        {
            var localizationService = ServiceLocator.Current.GetInstance<LocalizationService>();
            string value;

            if (!localizationService.TryGetString(resourceKey, out value))
            {
                value = resourceKey;
            }

            return value;
        }
    }
}
