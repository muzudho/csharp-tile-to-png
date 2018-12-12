namespace Grayscale.TileToPng.Actions.SavingTileMap
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SaveFile { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveFile"></param>
        public OutputModel(string saveFile)
        {
            SaveFile = saveFile;
        }
    }
}
