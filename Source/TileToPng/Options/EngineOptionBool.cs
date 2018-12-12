using System;

namespace Grayscale.TileToPng.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineOptionBool : IEngineOption
    {
        /// <summary>
        /// 
        /// </summary>
        public EngineOptionBool()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        public EngineOptionBool(bool value, bool defaultValue)
        {
            this.Value = value;
            this.Default = defaultValue;
        }

        /// <summary>
        /// 既定値
        /// </summary>
        public object Default { get; set; }

        /// <summary>
        /// 現在値
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public bool IsTrue()
        {
            return (bool)this.Value;
        }

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public long GetNumber()
        {
            throw new ApplicationException("型変換エラー");
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value"></param>
        public void ParseValue(string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                this.Value = result;
            }
        }
    }
}
