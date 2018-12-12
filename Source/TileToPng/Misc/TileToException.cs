using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TileToException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public TileToException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TileToException(string message) : base(message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TileToException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TileToException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
