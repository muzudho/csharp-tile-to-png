namespace Grayscale.TileToPng.CommandLineModel
{
    /// <summary>
    /// コマンドライン。
    /// </summary>
    public class Commandline : ICommandline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Commandline"/> class.
        /// </summary>
        /// <param name="number">何かしらの指定された数字。用途は任意。</param>
        /// <param name="scanOrder">走査する順番。</param>
        /// <param name="margin">何かしらのマージン。北、東、南、西。用途は任意。</param>
        /// <param name="colorA">何かしらの色A。</param>
        /// <param name="colorR">何かしらの色R。</param>
        /// <param name="colorG">何かしらの色G。</param>
        /// <param name="colorB">何かしらの色B。</param>
        public Commandline(
            long number,
            ScanOrder scanOrder,
            IMargin margin,
            int colorA,
            int colorR,
            int colorG,
            int colorB)
        {
            this.Number = number;
            this.ScanOrder = scanOrder;
            this.Margin = margin;
            this.ColorA = colorA;
            this.ColorR = colorR;
            this.ColorG = colorG;
            this.ColorB = colorB;
        }

        /// <summary>
        /// Gets or sets 何かしらの指定された数字。用途は任意。
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// Gets or sets 何かしらのマージン。北、東、南、西。用途は任意。
        /// </summary>
        public IMargin Margin { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色A。
        /// </summary>
        public int ColorA { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色R。
        /// </summary>
        public int ColorR { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色G。
        /// </summary>
        public int ColorG { get; set; }

        /// <summary>
        /// Gets or sets 何かしらの色B。
        /// </summary>
        public int ColorB { get; set; }

        /// <summary>
        /// Gets or sets 走査する順番。
        /// </summary>
        public ScanOrder ScanOrder { get; set; }
    }
}
