namespace Grayscale.TileToPng.Actions
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// タイルマップをCSV形式で保存。
    /// </summary>
    public sealed class SavingTileMapStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavingTileMapStep"/> class.
        /// </summary>
        public SavingTileMapStep()
        {
        }

        /// <summary>
        /// Gets or sets セーブファイル パス。
        /// </summary>
        public string OutputSaveFile { get; set; }

        /// <summary>
        /// Gets or sets ユーザーコントロール。
        /// </summary>
        public MainUserControl MainUserControl { get; set; }

        /// <summary>
        /// 実行。
        /// </summary>
        public void Perform()
        {
            StringBuilder sb = new StringBuilder();

            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                for (int y = 0; y < MainUserControl.GridMaxHeight; y++)
                {
                    for (int x = 0; x < MainUserControl.GridMaxWidth; x++)
                    {
                        string filename = this.MainUserControl.TileMapModel.GetItem(iLayer, y, x).FileName;

                        // フォルダーへのパスを「%HOME%」という文字に置き換えて短くするんだぜ☆（＾▽＾）
                        if (filename != null && filename.StartsWith(Application.StartupPath, StringComparison.CurrentCulture))
                        {
                            filename = "%HOME%" + filename.Substring(Application.StartupPath.Length);
                        }

                        // リストにあれば短縮名に置換。
                        foreach (var pair in Program.FileListModel.ImagePath)
                        {
                            if (pair.Value == filename)
                            {
                                filename = pair.Key;
                                break;
                            }
                        }

                        sb.Append(filename);
                        sb.Append(",");
                    }

                    sb.AppendLine();
                }
            }

            string file = Path.Combine(Application.StartupPath, this.OutputSaveFile);
            File.WriteAllText(file, sb.ToString());
        }
    }
}
