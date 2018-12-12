using System.Drawing;

namespace Grayscale.TileToPng.CommandLineModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandline
    {
        /// <summary>
        /// 何かしらの指定された数字。用途は任意。
        /// </summary>
        long Number { get; set; }

        /// <summary>
        /// 何かしらのマージン。北、東、南、西。用途は任意。
        /// </summary>
        IMargin Margin { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int ColorA { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int ColorR { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int ColorG { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        int ColorB { get; set; }

        /// <summary>
        /// 走査する順番。
        /// </summary>
        ScanOrder ScanOrder { get; set; }
    }
}
