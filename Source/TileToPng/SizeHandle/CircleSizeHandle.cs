namespace Grayscale.TileToPng
{
    using System.Drawing;

    /// <summary>
    /// リサイズ用のつまみ。
    /// </summary>
    public class CircleSizeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircleSizeHandle"/> class.
        /// </summary>
        /// <param name="left">左。</param>
        /// <param name="top">上。</param>
        public CircleSizeHandle(float left, float top)
        {
            this.Bounds = new RectangleF(
                left,
                top,
                20.0f,
                20.0f);
        }

        /// <summary>
        /// Gets or sets 矩形。
        /// </summary>
        public RectangleF Bounds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 押下。
        /// </summary>
        public bool Pressing { get; set; }

        /// <summary>
        /// 設定。
        /// </summary>
        /// <param name="left">左。</param>
        /// <param name="top">上。</param>
        public void SetLocationPixel(float left, float top)
        {
            this.Bounds = new RectangleF(
                left,
                top,
                this.Bounds.Width,
                this.Bounds.Height);
        }
    }
}
