namespace Acme.Model.DisplayChannels
{
    /// <summary>
    /// Defines resolution for desktop displays
    /// </summary>
    public class StandardResolution : DisplayResolutionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardResolution"/> class.
        /// </summary>
        public StandardResolution()
            : base("/resolutions/standard", 1366, 768)
        {
        }
    }
}
