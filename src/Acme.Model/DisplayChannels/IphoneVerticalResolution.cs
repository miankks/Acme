namespace Acme.Model.DisplayChannels
{
    /// <summary>
    /// Defines resolution for a vertical iPad
    /// </summary>
    public class IphoneVerticalResolution : DisplayResolutionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IphoneVerticalResolution"/> class.
        /// </summary>
        public IphoneVerticalResolution()
            : base("/resolutions/iphonevertical", 320, 568)
        {
        }
    }
}
