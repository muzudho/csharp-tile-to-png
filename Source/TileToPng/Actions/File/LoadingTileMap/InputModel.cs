namespace Grayscale.TileToPng.Actions.LoadingTileMap
{
    /// <summary>
    /// 
    /// </summary>
    public class InputModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SaveFile { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveFile"></param>
        public InputModel(string saveFile)
        {
            SaveFile = saveFile;
        }
    }
}
