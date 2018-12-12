using Grayscale.TileToPng.Menu;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Grayscale.TileToPng.Menu
{
    /// <summary>
    /// 
    /// </summary>
    public class Menubar : IMenubar
    {
        /// <summary>
        /// 
        /// </summary>
        public Menubar()
        {
            int height = 20;//グラデーションのための幅
            this.Bounds = new Rectangle(0,0,
                10,//目視できるように幅をちょっとだけ指定。
                height);
            this.BrushBackground = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, height),
                Color.FromArgb(255, 0, 0, 0),//不透明の黒
                Color.FromArgb(96, 0, 0, 0)//半透明の黒
                );
            this.BrushText = new SolidBrush(Color.White);
            this.Font = new Font("Serif", 12.0f);
            this.CursorMode = CursorMode.Pointer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="font"></param>
        public Menubar(int x, int y, int width, int height, Font font)
        {
            this.Bounds = new Rectangle(x,y,width,height);
            this.BrushBackground = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, height),
                Color.FromArgb(255, 0, 0, 0),//不透明の黒
                Color.FromArgb(96, 0, 0, 0)//半透明の黒
                );
            this.BrushText = new SolidBrush(Color.White);
            this.Font = font;
            this.CursorMode = CursorMode.Pointer;
        }

        /// <summary>
        /// サイズの更新
        /// </summary>
        /// <param name="width"></param>
        public void UpdateSize(int width)
        {
            this.Bounds = new Rectangle(this.Bounds.X, this.Bounds.Y, width, this.Bounds.Height);
        }

        /// <summary>
        /// カーソル・モード
        /// </summary>
        public CursorMode CursorMode { get; set; }

        /// <summary>
        /// 位置とサイズ
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// 背景色ブラシ
        /// </summary>
        public Brush BrushBackground { get; set; }

        /// <summary>
        /// 文字ブラシ
        /// </summary>
        public Brush BrushText { get; set; }

        /// <summary>
        /// フォント
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            g.FillRectangle(this.BrushBackground, this.Bounds);

            // カーソル・モード
            string label;
            switch (this.CursorMode)
            {
                case CursorMode.Pointer:
                    label = "カーソル: ポインター";
                    break;
                case CursorMode.Copy:
                    label = "カーソル: コピー";
                    break;
                default:
                    label = "カーソル: ムーブ";
                    break;
            }

            g.DrawString(label, this.Font,this.BrushText, new PointF(this.Bounds.X, this.Bounds.Y));
        }

        /// <summary>
        /// マウス左ボタン押下時
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <returns>このオブジェクトをクリックしたのなら真</returns>
        public bool OnMouseLeftDown(Point mouseLocation)
        {
            if (this.Bounds.Contains(mouseLocation))
            {
                // カーソル・モードをローテーションするぜ☆（＾▽＾）
                switch (this.CursorMode)
                {
                    case CursorMode.Pointer:
                        this.CursorMode = CursorMode.Copy;
                        break;
                    case CursorMode.Copy:
                        this.CursorMode = CursorMode.Move;
                        break;
                    default:
                        this.CursorMode = CursorMode.Pointer;
                        break;
                }
                return true;
            }

            return false;
        }
    }
}
