namespace Grayscale.TileToPng.CommandLineModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Margin : IMargin
    {
        /// <summary>
        /// 
        /// </summary>
        public Margin()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="north"></param>
        /// <param name="east"></param>
        /// <param name="south"></param>
        /// <param name="west"></param>
        public Margin(
            int north,
            int east,
            int south,
            int west
            )
        {
            this.North = north;
            this.East = east;
            this.South = south;
            this.West = west;
        }

        /// <summary>
        /// 
        /// </summary>
        public int North { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int East { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int South { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int West { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.North + ", " + this.East + ", " + this.South + ", " + this.West;
        }

    }
}
