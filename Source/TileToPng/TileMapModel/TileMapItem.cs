namespace Grayscale.TileToPng
{
    using System.Drawing;

    /// <summary>
    /// タイルの項目。
    /// </summary>
    public class TileMapItem
    {
        /// <summary>
        /// 空っぽのタイル。
        /// </summary>
        public static readonly TileMapItem Empty = new TileMapItem(string.Empty, null);

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMapItem"/> class.
        /// </summary>
        /// <param name="fileName">ファイル名。</param>
        /// <param name="image">画像。</param>
        public TileMapItem(string fileName, Image image)
        {
            this.FileName = fileName;
            this.Image = image;
        }

        /// <summary>
        /// Gets ファイル名。
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets 画像。
        /// </summary>
        public Image Image { get; private set; }
    }
}
