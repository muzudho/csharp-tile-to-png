namespace Grayscale.TileToPng
{
    using System.Collections.Generic;
    using Nett;

    /// <summary>
    /// 番号と、ファイルパスを紐づけ。
    /// </summary>
    public class FileListModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileListModel"/> class.
        /// </summary>
        public FileListModel()
        {
            this.ImagePath = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets 画像パス。
        /// </summary>
        public Dictionary<string, string> ImagePath { get; private set; }

        /// <summary>
        /// ファイル読み取り。
        /// </summary>
        /// <param name="path">ファイル パス。</param>
        /// <returns>ファイル一覧。</returns>
        public static FileListModel ReadFile(string path)
        {
            return Toml.ReadFile<FileListModel>(path);
        }
    }
}
