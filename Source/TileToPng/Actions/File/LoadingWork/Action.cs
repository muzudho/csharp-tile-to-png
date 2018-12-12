using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grayscale.TileToPng.Actions.LoadingWork
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Action
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Perform(ContextModel context, InputModel input, OutputModel output)
        {
            string file = Path.Combine(Application.StartupPath, "TileToPng_save.txt");
            if (File.Exists(file))
            {
                string text = File.ReadAllText(file);

                string[] tokens = text.Split(',');
                int index = 0;

                for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
                {
                    for (int y = 0; y < MainUserControl.GRID_MAX_HEIGHT; y++)
                    {
                        for (int x = 0; x < MainUserControl.GRID_MAX_WIDTH; x++)
                        {
                            string token = tokens[index].Trim();
                            index++;

                            // 「%HOME%」という文字列が含まれていれば、フォルダーへのパスに置き換えるぜ☆（＾▽＾）
                            if (-1 < token.IndexOf("%HOME%"))
                            {
                                token = token.Replace("%HOME%", Application.StartupPath);
                            }

                            if (string.IsNullOrEmpty(token))
                            {
                                context.MainUserControl.gridFilenames[iLayer][y][x] = "";
                                context.MainUserControl.gridImages[iLayer][y][x] = null;
                            }
                            else
                            {
                                context.MainUserControl.gridFilenames[iLayer][y][x] = token;
                                // とりあえず画像読み込み
                                context.MainUserControl.gridImages[iLayer][y][x] = Image.FromFile(token);
                            }
                        }
                    }
                }

                context.MainUserControl.Refresh();
            }
        }
    }
}
