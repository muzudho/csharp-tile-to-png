namespace Grayscale.TileToPng.CommandLineModel
{
    /// <summary>
    /// マージン。
    /// </summary>
    public class Margin : IMargin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Margin"/> class.
        /// </summary>
        public Margin()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Margin"/> class.
        /// </summary>
        /// <param name="north">北側。</param>
        /// <param name="east">東側。</param>
        /// <param name="south">南側。</param>
        /// <param name="west">西側。</param>
        public Margin(
            int north,
            int east,
            int south,
            int west)
        {
            this.North = north;
            this.East = east;
            this.South = south;
            this.West = west;
        }

        /// <summary>
        /// Gets or sets 北側。
        /// </summary>
        public int North { get; set; }

        /// <summary>
        /// Gets or sets 東側。
        /// </summary>
        public int East { get; set; }

        /// <summary>
        /// Gets or sets 南側。
        /// </summary>
        public int South { get; set; }

        /// <summary>
        /// Gets or sets 西側。
        /// </summary>
        public int West { get; set; }

        /// <summary>
        /// デバッグ出力。
        /// </summary>
        /// <returns>文字列。</returns>
        public override string ToString()
        {
            return this.North + ", " + this.East + ", " + this.South + ", " + this.West;
        }
    }
}
