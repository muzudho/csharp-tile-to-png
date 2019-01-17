namespace Grayscale.TileToPng.Actions
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// [Ctrl] + [V] キー。
    /// </summary>
    public sealed class ControlVStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlVStep"/> class.
        /// </summary>
        public ControlVStep()
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
            this.MainUserControl.SetTileOnCursor(new TileMapItem(
                this.MainUserControl.ClipboardFilename,
                this.MainUserControl.ClipboardImage));
            this.MainUserControl.Refresh();
        }
    }
}
