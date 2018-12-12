namespace Grayscale.TileToPng.CommandLine
{
    /// <summary>
    /// 
    /// </summary>
    public class Commandline : ICommandline
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="scanOrder"></param>
        /// <param name="margin"></param>
        /// <param name="colorA"></param>
        /// <param name="colorR"></param>
        /// <param name="colorG"></param>
        /// <param name="colorB"></param>
        public Commandline(
            long number,
            ScanOrder scanOrder,
            Margin margin,
            int colorA,
            int colorR,
            int colorG,
            int colorB
            )
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
        /// 何かしらの指定された数字。用途は任意。
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// 何かしらのマージン。北、東、南、西。用途は任意。
        /// </summary>
        public Margin Margin { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        public int ColorA { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        public int ColorR { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        public int ColorG { get; set; }

        /// <summary>
        /// 何かしらの色。
        /// </summary>
        public int ColorB { get; set; }

        /// <summary>
        /// 走査する順番。
        /// </summary>
        public ScanOrder ScanOrder { get; set; }
    }
}
