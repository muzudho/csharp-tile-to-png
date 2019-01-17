namespace Grayscale.TileToPng
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// タイルの例外。
    /// </summary>
    [Serializable]
    public class TileToException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileToException"/> class.
        /// </summary>
        public TileToException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileToException"/> class.
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public TileToException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileToException"/> class.
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">例外。</param>
        public TileToException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileToException"/> class.
        /// </summary>
        /// <param name="info">情報。</param>
        /// <param name="context">コンテキスト。</param>
        protected TileToException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
