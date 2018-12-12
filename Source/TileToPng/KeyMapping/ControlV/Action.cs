using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Grayscale.TileToPng.KeyMapping.ControlV
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

            context.MainUserControl.SetTileOnCursor(new TileMapItem(
                context.MainUserControl.ClipboardFilename,
                context.MainUserControl.ClipboardImage));
            context.MainUserControl.Refresh();
        }

        Action()
        {
        }
    }
}
