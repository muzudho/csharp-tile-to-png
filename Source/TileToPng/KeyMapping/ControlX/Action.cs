using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Grayscale.TileToPng.KeyMapping.ControlX
{
    /// <summary>
    /// [Ctrl] + [C] キー。
    /// </summary>
    public sealed class Action
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Perform(ContextModel context, InputModel input, OutputModel output)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            var tile = context.MainUserControl.GetTileOnCursor();
            context.MainUserControl.ClipboardFilename = tile.FileName;
            context.MainUserControl.ClipboardImage = tile.Image;

            context.MainUserControl.SetTileOnCursor(TileMapItem.Empty);
            context.MainUserControl.Refresh();
        }

        Action()
        {
        }
    }
}
