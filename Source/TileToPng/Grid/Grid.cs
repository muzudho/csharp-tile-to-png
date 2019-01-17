namespace Grayscale.TileToPng
{
    using System.Drawing;

    /// <summary>
    /// グリッド
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="ox">左。</param>
        /// <param name="oy">上。</param>
        /// <param name="cellW">セル横幅。</param>
        /// <param name="cellH">セル縦幅。</param>
        public Grid(
            float ox,
            float oy,
            float cellW,
            float cellH)
        {
            this.OriginX = ox;
            this.OriginY = oy;
            this.CellW = cellW;
            this.CellH = cellH;
        }

        /// <summary>
        /// Gets or sets 原点x
        /// </summary>
        public float OriginX { get; set; }

        /// <summary>
        /// Gets or sets 原点y
        /// </summary>
        public float OriginY { get; set; }

        /// <summary>
        /// Gets or sets セル横幅
        /// </summary>
        public float CellW { get; set; }

        /// <summary>
        /// Gets or sets セル縦幅
        /// </summary>
        public float CellH { get; set; }

        /// <summary>
        /// Gets or sets ヨコ線とタテ線の終点（の１つ右下）
        /// </summary>
        public PointF End { get; set; }

        /// <summary>
        /// Gets 列。
        /// </summary>
        public int Cols
        {
            get { return (int)((this.End.X - this.OriginX) / this.CellW); }
        }

        /// <summary>
        /// Gets 行。
        /// </summary>
        public int Rows
        {
            get { return (int)((this.End.Y - this.OriginY) / this.CellH); }
        }
    }
}
