namespace Acme.Model.DisplayChannels
{
    /// <summary>
    /// Defines resolution for a vertical Android handheld device
    /// </summary>
    public class AndroidVerticalResolution : DisplayResolutionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndroidVerticalResolution"/> class.
        /// </summary>
        public AndroidVerticalResolution()
            : base("/resolutions/androidvertical", 480, 800)
        {
        }
    }
}
