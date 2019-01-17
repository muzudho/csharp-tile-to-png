namespace Grayscale.TileToPng.Actions
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// [Ctrl] + [C] キー。
    /// </summary>
    public sealed class ControlCStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlCStep"/> class.
        /// </summary>
        public ControlCStep()
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
        }
    }
}
