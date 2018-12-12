using Nett;
using System.Collections.Generic;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// 番号と、ファイルパスを紐づけ。
    /// </summary>
    public class FileListModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">ファイル パス。</param>
        /// <returns></returns>
        public static FileListModel ReadFile(string path)
        {
            return Toml.ReadFile<FileListModel>(path);
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> ImagePath { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public FileListModel()
        {
            ImagePath = new Dictionary<string, string>();
        }
    }
}
