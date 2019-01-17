namespace Grayscale.TileToPng.Actions
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// [Ctrl] + [X] キー。
    /// </summary>
    public sealed class ControlXStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlXStep"/> class.
        /// </summary>
        public ControlXStep()
        {
        }

        /// <summary>
        /// Gets or sets ユーザーコントロール。
        /// </summary>
        public MainUserControl MainUserControl { get; set; }

        /// <summary>
        /// 実行。
        /// </summary>
        public void Perform()
        {
            var tile = this.MainUserControl.GetTileOnCursor();
            this.MainUserControl.ClipboardFileName = tile.FileName;
            this.MainUserControl.ClipboardImage = tile.Image;

            this.MainUserControl.SetTileOnCursor(TileMapItem.Empty);
            this.MainUserControl.Refresh();
        }
    }
}
