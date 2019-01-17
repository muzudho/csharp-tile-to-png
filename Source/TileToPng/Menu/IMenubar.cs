namespace Grayscale.TileToPng.Menu
{
    using System.Drawing;

    /// <summary>
    /// メニューバー。
    /// </summary>
    public interface IMenubar
    {
        /// <summary>
        /// Gets or sets カーソル・モード
        /// </summary>
        CursorMode CursorMode { get; set; }

        /// <summary>
        /// Gets or sets 位置とサイズ
        /// </summary>
        Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets or sets 背景色ブラシ
        /// </summary>
        Brush BrushBackground { get; set; }

        /// <summary>
        /// Gets or sets 文字ブラシ
        /// </summary>
        Brush BrushText { get; set; }

        /// <summary>
        /// Gets or sets フォント
        /// </summary>
        Font Font { get; set; }

        /// <summary>
        /// サイズの更新
        /// </summary>
        /// <param name="width">横幅。</param>
        void UpdateSize(int width);

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="gra">グラフィック。</param>
        void Paint(Graphics gra);

        /// <summary>
        /// マウス左ボタン押下時
        /// </summary>
        /// <param name="mouseLocation">マウス位置。</param>
        /// <returns>有無。</returns>
        bool OnMouseLeftDown(Point mouseLocation);
    }
}
