using System.Drawing;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// タイルの項目。
    /// </summary>
    public class TileMapItem
    {
        /// <summary>
        /// 空っぽのタイル。
        /// </summary>
        public static readonly TileMapItem Empty = new TileMapItem("", null);

        /// <summary>
        /// ファイル名。
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 画像。
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">ファイル名。</param>
        /// <param name="image">画像。</param>
        public TileMapItem(string fileName, Image image)
        {
            FileName = fileName;
            Image = image;
        }
    }
}
