using System;

namespace Grayscale.TileToPng.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineOption_NumberImpl : IEngineOption
    {
        /// <summary>
        /// 
        /// </summary>
        public EngineOption_NumberImpl()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public EngineOption_NumberImpl(long value)
        {
            this.Value = value;
            this.Default = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        public EngineOption_NumberImpl(long value, long defaultValue)
        {
            this.Value = value;
            this.Default = defaultValue;
        }

        /// <summary>
        /// 既定値 long
        /// </summary>
        public object Default { get; set; }

        /// <summary>
        /// 現在値 long
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 論理値型でのみ使用可能。論理値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public bool IsTrue()
        {
            throw new ApplicationException("型変換エラー");
        }

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public long GetNumber()
        {
            return (long)this.Value;
        }

        /// <summary>
        /// 現在値（文字列読取）
        /// </summary>
        /// <param name="value"></param>
        public void ParseValue(string value)
        {
            if (long.TryParse(value, out long result))
            {
                this.Value = result;
            }
        }
    }
}
