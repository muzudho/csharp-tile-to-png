namespace Grayscale.TileToPng.CommandLineModel
{
    /// <summary>
    /// 走査の順。
    /// </summary>
    public enum ScanOrder
    {
        /// <summary>
        /// 未指定。
        /// </summary>
        None,

        /// <summary>
        /// horizontal read from south west. (ex. chess program)
        /// </summary>
        Hsw
    }
}
