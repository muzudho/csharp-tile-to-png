namespace Grayscale.TileToPng.Options
{
    using System;

    /// <summary>
    /// エンジン オプション。
    /// </summary>
    public class EngineOptionNumber : IEngineOption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionNumber"/> class.
        /// </summary>
        public EngineOptionNumber()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionNumber"/> class.
        /// </summary>
        /// <param name="value">数値。</param>
        public EngineOptionNumber(long value)
        {
            this.Value = value;
            this.DefaultValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionNumber"/> class.
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="defaultValue">デフォルト値。</param>
        public EngineOptionNumber(long value, long defaultValue)
        {
            this.Value = value;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Gets or sets 既定値 long
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets 現在値 long
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns>論理値。</returns>
        public bool IsTrue()
        {
            throw new TileToException("型変換エラー");
        }

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns>数値。</returns>
        public long ToNumber()
        {
            return (long)this.Value;
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value">文字列。</param>
        public void ParseValue(string value)
        {
            if (long.TryParse(value, out long result))
            {
                this.Value = result;
            }
        }
    }
}
