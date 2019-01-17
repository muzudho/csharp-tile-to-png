namespace Grayscale.TileToPng
{
    using System.Drawing;

    /// <summary>
    /// タイル マップ構造。
    /// </summary>
    public class TileMapModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileMapModel"/> class.
        /// </summary>
        public TileMapModel()
        {
            this.Model = new TileMapItem[MainUserControl.GridMaxLayer][][];
            for (int i = 0; i < this.Model.Length; i++)
            {
                this.Model[i] = new TileMapItem[MainUserControl.GridMaxHeight][];
                for (int j = 0; j < this.Model[i].Length; j++)
                {
                    this.Model[i][j] = new TileMapItem[MainUserControl.GridMaxWidth];
                }
            }
        }

        /// <summary>
        /// Gets or sets レイヤー、行、列の３次元配列。
        /// </summary>
        public TileMapItem[][][] Model { get; set; }

        /// <summary>
        /// 項目取得。
        /// </summary>
        /// <param name="layer">レイヤー。</param>
        /// <param name="row">行。</param>
        /// <param name="column">列。</param>
        /// <returns>タイル項目。</returns>
        public TileMapItem GetItem(int layer, int row, int column)
        {
            return this.Model[layer][row][column];
        }

        /// <summary>
        /// 項目設定。
        /// </summary>
        /// <param name="layer">レイヤー。</param>
        /// <param name="row">行。</param>
        /// <param name="column">列。</param>
        /// <param name="item">タイル項目。</param>
        public void SetItem(int layer, int row, int column, TileMapItem item)
        {
            this.Model[layer][row][column] = item;
        }
    }
}
