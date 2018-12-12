using System.Drawing;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// タイル マップ構造。
    /// </summary>
    public class TileMapModel
    {
        /// <summary>
        /// レイヤー、行、列の３次元配列。
        /// </summary>
        public TileMapItem[][][] Model { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public TileMapItem GetItem(int layer, int row, int column)
        {
            return this.Model[layer][row][column];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="item"></param>
        public void SetItem(int layer, int row, int column, TileMapItem item)
        {
            this.Model[layer][row][column] = item;
        }
    }
}
