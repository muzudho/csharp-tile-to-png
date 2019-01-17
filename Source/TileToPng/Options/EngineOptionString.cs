namespace Grayscale.TileToPng.Options
{
    using System;

    /// <summary>
    /// エンジン オプション。
    /// </summary>
    public class EngineOptionString : IEngineOption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionString"/> class.
        /// </summary>
        public EngineOptionString()
        {
            this.Value = string.Empty;
            this.DefaultValue = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionString"/> class.
        /// 最初に入ってきた値を、既定値とします。
        /// </summary>
        /// <param name="value">値。</param>
        public EngineOptionString(string value)
        {
            this.Value = value;
            this.DefaultValue = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionString"/> class.
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="defaultValue">デフォルト値。</param>
        public EngineOptionString(string value, string defaultValue)
        {
            this.Value = value;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Gets or sets 既定値 string
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets 現在値
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns>論理値。</returns>
        public bool IsTrue()
        {
            if (bool.TryParse((string)this.Value, out bool result))
            {
                return result;
            }

            throw new TileToException("型変換エラー");
        }

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns>数値。</returns>
        public long ToNumber()
        {
            if (long.TryParse((string)this.Value, out long result))
            {
                return result;
            }

            throw new TileToException("型変換エラー");
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value">値。</param>
        public void ParseValue(string value)
        {
            this.Value = value;
        }
    }
}
