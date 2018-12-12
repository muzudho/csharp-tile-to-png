﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Grayscale.TileToPng.Actions.LoadingWork
{
    /// <summary>
    /// 前回の作業状態から再開。
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

            string file = Path.Combine(Application.StartupPath, "TileToPng_save.txt");
            if (File.Exists(file))
            {
                string text = File.ReadAllText(file);

                string[] tokens = text.Split(',');
                int index = 0;

                for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
                {
                    for (int y = 0; y < MainUserControl.GridMaxHeight; y++)
                    {
                        for (int x = 0; x < MainUserControl.GridMaxWidth; x++)
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
                                context.MainUserControl.GridFilenames[iLayer][y][x] = "";
                                context.MainUserControl.GridImages[iLayer][y][x] = null;
                            }
                            else
                            {
                                context.MainUserControl.GridFilenames[iLayer][y][x] = token;
                                // とりあえず画像読み込み
                                context.MainUserControl.GridImages[iLayer][y][x] = Image.FromFile(token);
                            }
                        }
                    }
                }

                context.MainUserControl.Refresh();
            }
        }

        Action()
        {
        }
    }
}
