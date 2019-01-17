namespace Grayscale.TileToPng.CommandLineModel
{
    /// <summary>
    /// マージン。
    /// </summary>
    public interface IMargin
    {
        /// <summary>
        /// Gets or sets 上側。
        /// </summary>
        int North { get; set; }

        /// <summary>
        /// Gets or sets 右側。
        /// </summary>
        int East { get; set; }

        /// <summary>
        /// Gets or sets 下側。
        /// </summary>
        int South { get; set; }

        /// <summary>
        /// Gets or sets 左側。
        /// </summary>
        int West { get; set; }
    }
}
