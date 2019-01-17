namespace Grayscale.TileToPng
{
    using System.Drawing;

    /// <summary>
    /// 四角いカーソル。
    /// </summary>
    public class RectangleCursor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleCursor"/> class.
        /// </summary>
        public RectangleCursor()
        {
            this.Position = default(Point);
            this.Bounds = default(RectangleF);
        }

        /// <summary>
        /// Gets or sets コントロールパネル上の位置とサイズ。
        /// </summary>
        public RectangleF Bounds { get; set; }

        /// <summary>
        /// Gets or sets 盤上の位置。
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets 盤上のヨコ位置。
        /// </summary>
        public int LeftIndex
        {
            get
            {
                return this.Position.X;
            }
        }

        /// <summary>
        /// Gets 盤上のタテ位置。
        /// </summary>
        public int TopIndex
        {
            get
            {
                return this.Position.Y;
            }
        }

        /// <summary>
        /// Gets or sets z位置。
        /// </summary>
        public int ZOrder { get; set; }

        /// <summary>
        /// 平行移動。
        /// </summary>
        /// <param name="offsetX">オフセット。</param>
        public void MoveToRight(int offsetX)
        {
            this.Position = new Point(this.LeftIndex + offsetX, this.TopIndex);
        }

        /// <summary>
        /// 平行移動。
        /// </summary>
        /// <param name="offsetY">オフセット。</param>
        public void MoveToBottom(int offsetY)
        {
            this.Position = new Point(this.LeftIndex, this.TopIndex + offsetY);
        }

        /// <summary>
        /// 設定。
        /// </summary>
        /// <param name="left">左。</param>
        public void SetLeft(int left)
        {
            this.Position = new Point(left, this.TopIndex);
        }

        /// <summary>
        /// 設定。
        /// </summary>
        /// <param name="top">上。</param>
        public void SetTop(int top)
        {
            this.Position = new Point(this.LeftIndex, top);
        }

        /// <summary>
        /// 設定。
        /// </summary>
        /// <param name="left">左。</param>
        /// <param name="top">上。</param>
        public void SetLocationOnTable(int left, int top)
        {
            this.Position = new Point(left, top);
        }

        /// <summary>
        /// 設定。
        /// </summary>
        /// <param name="left">左。</param>
        /// <param name="top">上。</param>
        /// <param name="width">横幅。</param>
        /// <param name="height">縦幅。</param>
        public void SetBoundsOnPixel(float left, float top, float width, float height)
        {
            this.Bounds = new RectangleF(left, top, width, height);
        }
    }
}
