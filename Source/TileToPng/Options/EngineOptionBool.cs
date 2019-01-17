namespace Grayscale.TileToPng.Options
{
    using System;

    /// <summary>
    /// エンジン オプション。
    /// </summary>
    public class EngineOptionBool : IEngineOption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionBool"/> class.
        /// </summary>
        public EngineOptionBool()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionBool"/> class.
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="defaultValue">デフォルト値。</param>
        public EngineOptionBool(bool value, bool defaultValue)
        {
            this.Value = value;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Gets or sets 既定値
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
            return (bool)this.Value;
        }

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns>数値。</returns>
        public long ToNumber()
        {
            throw new TileToException("型変換エラー");
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value">文字列。</param>
        public void ParseValue(string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                this.Value = result;
            }
        }
    }
}
