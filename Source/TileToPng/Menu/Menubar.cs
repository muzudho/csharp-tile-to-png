namespace Grayscale.TileToPng.Menu
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// メニューバー。
    /// </summary>
    public class Menubar : IMenubar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menubar"/> class.
        /// </summary>
        public Menubar()
        {
            // グラデーションのための幅
            int height = 20;

            // 目視できるように幅をちょっとだけ指定。
            this.Bounds = new Rectangle(0, 0, 10, height);

            // 不透明の黒
            this.BrushBackground = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, height),
                Color.FromArgb(255, 0, 0, 0),
                Color.FromArgb(96, 0, 0, 0));
            this.BrushText = new SolidBrush(Color.White);
            this.Font = new Font("Serif", 12.0f);
            this.CursorMode = CursorMode.Pointer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menubar"/> class.
        /// </summary>
        /// <param name="left">x 座標。</param>
        /// <param name="top">y 座標。</param>
        /// <param name="width">横幅。</param>
        /// <param name="height">縦幅。</param>
        /// <param name="font">フォント。</param>
        public Menubar(int left, int top, int width, int height, Font font)
        {
            this.Bounds = new Rectangle(left, top, width, height);

            // 不透明の黒
            // 半透明の黒
            this.BrushBackground = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, height),
                Color.FromArgb(255, 0, 0, 0),
                Color.FromArgb(96, 0, 0, 0));
            this.BrushText = new SolidBrush(Color.White);
            this.Font = font;
            this.CursorMode = CursorMode.Pointer;
        }

        /// <summary>
        /// Gets or sets カーソル・モード
        /// </summary>
        public CursorMode CursorMode { get; set; }

        /// <summary>
        /// Gets or sets 位置とサイズ
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets or sets 背景色ブラシ
        /// </summary>
        public Brush BrushBackground { get; set; }

        /// <summary>
        /// Gets or sets 文字ブラシ
        /// </summary>
        public Brush BrushText { get; set; }

        /// <summary>
        /// Gets or sets フォント
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// サイズの更新
        /// </summary>
        /// <param name="width">横幅。</param>
        public void UpdateSize(int width)
        {
            this.Bounds = new Rectangle(this.Bounds.X, this.Bounds.Y, width, this.Bounds.Height);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="gra">グラフィック</param>
        public void Paint(Graphics gra)
        {
            if (gra == null)
            {
                throw new ArgumentNullException("gra");
            }

            gra.FillRectangle(this.BrushBackground, this.Bounds);

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

            gra.DrawString(label, this.Font, this.BrushText, new PointF(this.Bounds.X, this.Bounds.Y));
        }

        /// <summary>
        /// マウス左ボタン押下時
        /// </summary>
        /// <param name="mouseLocation">マウス位置。</param>
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
