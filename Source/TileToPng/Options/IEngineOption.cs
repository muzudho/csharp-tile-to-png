namespace Grayscale.TileToPng.Options
{
    /// <summary>
    /// エンジン オプション。
    /// </summary>
    public interface IEngineOption
    {
        /// <summary>
        /// Gets or sets 既定値
        /// bool, long, string
        /// </summary>
        object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets 現在値
        /// bool, long, string
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value">値。</param>
        void ParseValue(string value);

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns>論理値。</returns>
        bool IsTrue();

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns>数値。</returns>
        long ToNumber();
    }
}
