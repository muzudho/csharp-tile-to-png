using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// リサイズ用のつまみ。
    /// </summary>
    public class CircleSizeHandle
    {
        /// <summary>
        /// 
        /// </summary>
        public RectangleF Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Pressing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CircleSizeHandle(float left, float top)
        {
            this.Bounds = new RectangleF(
                left,
                top,
                20.0f,
                20.0f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
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
