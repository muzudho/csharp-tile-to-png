using System.Drawing;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// 四角いカーソル。
    /// </summary>
    public class RectangleCursor
    {
        /// <summary>
        /// コントロールパネル上の位置とサイズ。
        /// </summary>
        public RectangleF Bounds { get; set; }

        /// <summary>
        /// 盤上の位置。
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// 盤上のヨコ位置。
        /// </summary>
        public int LeftIndex
        {
            get
            {
                return Position.X;
            }
        }

        /// <summary>
        /// 盤上のタテ位置。
        /// </summary>
        public int TopIndex
        {
            get
            {
                return Position.Y;
            }
        }

        /// <summary>
        /// Z位置。
        /// </summary>
        public int ZOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RectangleCursor()
        {
            this.Position = new Point();
            this.Bounds = new RectangleF();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsetX"></param>
        public void MoveToRight(int offsetX)
        {
            this.Position = new Point(this.LeftIndex + offsetX, this.TopIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsetY"></param>
        public void MoveToBottom(int offsetY)
        {
            this.Position = new Point(this.LeftIndex, this.TopIndex + offsetY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        public void SetLeft(int left)
        {
            this.Position = new Point(left, this.TopIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        public void SetTop(int top)
        {
            this.Position = new Point(this.LeftIndex, top);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public void SetLocationOnTable(int left, int top)
        {
            this.Position = new Point(left, top);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetBoundsOnPixel(float left, float top, float width, float height)
        {
            this.Bounds = new RectangleF(left, top, width, height);
        }
    }
}
