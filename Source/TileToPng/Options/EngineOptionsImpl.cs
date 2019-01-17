namespace Grayscale.TileToPng.Options
{
    using System.Collections.Generic;

    /// <summary>
    /// エンジン オプション。
    /// </summary>
    public class EngineOptionsImpl : IEngineOptions
    {
        // public Dictionary<string, EngineOption> Entries
        // {
        //    get { return this.m_entries_; }
        //    set { this.m_entries_ = value; }
        // }
        private readonly Dictionary<string, IEngineOption> entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineOptionsImpl"/> class.
        /// </summary>
        public EngineOptionsImpl()
        {
            this.entries = new Dictionary<string, IEngineOption>();
        }

        /// <summary>
        /// 項目の有無
        /// </summary>
        /// <param name="name">名前。</param>
        /// <returns>有無。</returns>
        public bool ContainsKey(string name)
        {
            return this.entries.ContainsKey(name);
        }

        /// <summary>
        /// 項目を上書き。なければ文字列型として項目を追加。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <param name="entry">エントリー。</param>
        public void ParseValueAutoAdd(string name, string entry)
        {
            if (this.ContainsKey(name))
            {
                this.entries[name].ParseValue(entry);
            }
            else
            {
                // 文字列型項目の追加。
                this.entries.Add(name, new EngineOptionString(entry));
            }
        }

        /// <summary>
        /// 項目を追加。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <param name="entry">エントリー。</param>
        public void AddEntry(string name, IEngineOption entry)
        {
            this.entries.Add(name, entry);
        }

        /// <summary>
        /// 項目を取得。
        /// </summary>
        /// <param name="name">名前。</param>
        /// <returns>項目。</returns>
        public IEngineOption GetEntry(string name)
        {
            return this.entries[name];
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="name">名前。</param>
        /// <param name="value">値。</param>
        public void ParseValue(string name, string value)
        {
            this.entries[name].ParseValue(value);
        }
    }
}
