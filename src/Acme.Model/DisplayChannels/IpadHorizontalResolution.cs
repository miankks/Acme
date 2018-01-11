namespace Acme.Model.DisplayChannels
{
    /// <summary>
    /// Defines resolution for a horizontal iPad
    /// </summary>
    public class IpadHorizontalResolution : DisplayResolutionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IpadHorizontalResolution"/> class.
        /// </summary>
        public IpadHorizontalResolution()
            : base("/resolutions/ipadhorizontal", 1024, 768)
        {
        }
    }
}
