using System;

namespace Grayscale.TileToPng.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineOptionString : IEngineOption
    {
        /// <summary>
        /// 
        /// </summary>
        public EngineOptionString()
        {
            this.Value = "";
            this.DefaultValue = "";
        }

        /// <summary>
        /// 最初に入ってきた値を、既定値とします。
        /// </summary>
        /// <param name="value"></param>
        public EngineOptionString(string value)
        {
            this.Value = value;
            this.DefaultValue = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        public EngineOptionString(string value, string defaultValue)
        {
            this.Value = value;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// 既定値 string
        /// </summary>
        public object DefaultValue { get; set; }

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
            if (bool.TryParse((string)this.Value,out bool result))
            {
                return result;
            }

            throw new TileToException("型変換エラー");
        }


        /// <summary>
        /// 数値型でのみ使用可能。数値型でない場合、エラー。
        /// </summary>
        /// <returns></returns>
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
        /// <param name="value"></param>
        public void ParseValue(string value)
        {
            this.Value = value;
        }
    }
}
