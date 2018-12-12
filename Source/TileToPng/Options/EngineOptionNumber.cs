using System;

namespace Grayscale.TileToPng.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineOptionNumber : IEngineOption
    {
        /// <summary>
        /// 
        /// </summary>
        public EngineOptionNumber()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public EngineOptionNumber(long value)
        {
            this.Value = value;
            this.DefaultValue = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        public EngineOptionNumber(long value, long defaultValue)
        {
            this.Value = value;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// 既定値 long
        /// </summary>
        public object DefaultValue { get; set; }

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
            throw new TileToException("型変換エラー");
        }

        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
        public long ToNumber()
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
