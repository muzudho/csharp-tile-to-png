namespace Grayscale.TileToPng.CommandLineModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMargin
    {
        /// <summary>
        /// 上側。
        /// </summary>
        int North { get; set; }

        /// <summary>
        /// 右側。
        /// </summary>
        int East { get; set; }

        /// <summary>
        /// 下側。
        /// </summary>
        int South { get; set; }

        /// <summary>
        /// 左側。
        /// </summary>
        int West { get; set; }
    }
}
