namespace Grayscale.TileToPng.Options
{
    /// <summary>
    /// USI「setoption」コマンドのリストです。
    /// </summary>
    public interface IEngineOptions
    {
        /// <summary>
        /// 項目の有無
        /// </summary>
        /// <param name="name">名前。</param>
        /// <returns>有無。</returns>
        bool ContainsKey(string name);

        /// <summary>
        /// 項目を上書き。なければ文字列型として項目を追加。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <param name="entry">エントリー。</param>
        void ParseValueAutoAdd(string name, string entry);

        /// <summary>
        /// 項目を追加。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <param name="entry">エントリー。</param>
        void AddEntry(string name, IEngineOption entry);

        /// <summary>
        /// 項目を取得。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <returns>エンジン オプション。</returns>
        IEngineOption GetEntry(string name);

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="name">名前。</param>
        /// <param name="value">値。</param>
        void ParseValue(string name, string value);
    }
}
