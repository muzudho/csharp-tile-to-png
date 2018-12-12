using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Grayscale.TileToPng.Actions.SavingTileMap
{
    /// <summary>
    /// タイルマップをCSV形式で保存。
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

            StringBuilder sb = new StringBuilder();

            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                for (int y = 0; y < MainUserControl.GridMaxHeight; y++)
                {
                    for (int x = 0; x < MainUserControl.GridMaxWidth; x++)
                    {
                        string filename = context.MainUserControl.TileMapModel.GetItem(iLayer, y, x).FileName;

                        // フォルダーへのパスを「%HOME%」という文字に置き換えて短くするんだぜ☆（＾▽＾）
                        if (null != filename && filename.StartsWith(Application.StartupPath, StringComparison.CurrentCulture))
                        {
                            filename = "%HOME%" + filename.Substring(Application.StartupPath.Length);
                        }

                        sb.Append(filename);
                        sb.Append(",");
                    }
                    sb.AppendLine();
                }
            }

            string file = Path.Combine(Application.StartupPath, output.SaveFile);
            File.WriteAllText(file, sb.ToString());
        }

        Action()
        {
        }
    }
}
