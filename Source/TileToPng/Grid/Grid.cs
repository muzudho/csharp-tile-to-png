using System.Drawing;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// グリッド
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ox"></param>
        /// <param name="oy"></param>
        /// <param name="cellW"></param>
        /// <param name="cellH"></param>
        public Grid(
            float ox,
            float oy,
            float cellW,
            float cellH
            )
        {
            Ox = ox;
            Oy = oy;
            CellW = cellW;
            CellH = cellH;
        }

        /// <summary>
        /// 原点x
        /// </summary>
        public float Ox { get; set; }

        /// <summary>
        /// 原点y
        /// </summary>
        public float Oy { get; set; }

        /// <summary>
        /// セル横幅
        /// </summary>
        public float CellW { get; set; }

        /// <summary>
        /// セル縦幅
        /// </summary>
        public float CellH { get; set; }

        /// <summary>
        /// ヨコ線とタテ線の終点（の１つ右下）
        /// </summary>
        public PointF End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Cols
        {
            get { return (int)((End.X - Ox) / CellW); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Rows
        {
            get { return (int)((End.Y - Oy) / CellH); }
        }
    }
}
