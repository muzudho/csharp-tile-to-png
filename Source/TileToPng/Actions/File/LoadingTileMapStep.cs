namespace Grayscale.TileToPng.Actions.LoadingTileMap
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// タイルマップをCSV形式で読取。
    /// </summary>
    public sealed class LoadingTileMapStep
    {
        public LoadingTileMapStep()
        {
        }

        /// <summary>
        /// セーブファイル パス。
        /// </summary>
        public string InputSaveFile { get; set; }

        /// <summary>
        /// Gets ユーザーコントロール。
        /// </summary>
        public MainUserControl MainUserControl { get; set; }

        /// <summary>
        /// 実行。
        /// </summary>
        public void Perform()
        {
            string file = Path.Combine(Application.StartupPath, this.InputSaveFile);
            if (File.Exists(file))
            {
                string text = File.ReadAllText(file);

                // ファイルパス、または別名。
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

                            // リストにあれば置換。
                            if (Program.FileListModel.ImagePath.ContainsKey(token))
                            {
                                token = Program.FileListModel.ImagePath[token];
                            }

                            // 「%HOME%」という文字列が含まれていれば、フォルダーへのパスに置き換えるぜ☆（＾▽＾）
                            if (-1 < token.IndexOf("%HOME%", StringComparison.CurrentCulture))
                            {
                                token = token.Replace("%HOME%", Application.StartupPath);
                            }

                            if (string.IsNullOrEmpty(token))
                            {
                                this.MainUserControl.TileMapModel.SetItem(iLayer, y, x, TileMapItem.Empty);
                            }
                            else
                            {
                                // とりあえず画像読み込み
                                this.MainUserControl.TileMapModel.SetItem(iLayer, y, x, new TileMapItem(token, Image.FromFile(token)));
                            }
                        }
                    }
                }

                this.MainUserControl.Refresh();
            }
        }
    }
}
