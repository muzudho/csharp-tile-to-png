﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Z位置。
        /// </summary>
        public int ZOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RectangleCursor()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsetX"></param>
        public void MoveToRight(int offsetX)
        {
            this.Position = new Point(this.Position.X + offsetX, this.Position.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offsetY"></param>
        public void MoveToBottom(int offsetY)
        {
            this.Position = new Point(this.Position.X, this.Position.Y + offsetY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        public void SetLeft(int left)
        {
            this.Position = new Point(left, this.Position.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        public void SetTop(int top)
        {
            this.Position = new Point(this.Position.X, top);
        }
    }
}