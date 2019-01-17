namespace Grayscale.TileToPng.CommandLineModel
{
    using System.Drawing;

    /// <summary>
    /// コマンドライン。
    /// </summary>
    public interface ICommandline
    {
        /// <summary>
        /// Gets or sets 何かしらの指定された数字。用途は任意。
        /// </summary>
        long Number { get; set; }

        /// <summary>
        /// Gets or sets 何かしらのマージン。北、東、南、西。用途は任意。
        /// </summary>
        IMargin Margin { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色。
        /// </summary>
        int ColorA { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色。
        /// </summary>
        int ColorR { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色。
        /// </summary>
        int ColorG { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色。
        /// </summary>
        int ColorB { get; set; }

        /// <summary>
        /// Gets or sets 走査する順番。
        /// </summary>
        ScanOrder ScanOrder { get; set; }
    }
}
