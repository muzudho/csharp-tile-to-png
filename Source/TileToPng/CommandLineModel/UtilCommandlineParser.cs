namespace Grayscale.TileToPng.CommandLineModel
{
    using System;
    using System.Globalization;

    /// <summary>
    /// コマンドライン パーサー。
    /// </summary>
    public abstract class UtilCommandlineParser
    {
        /// <summary>
        /// select 0b0000000000000000000000000000000000000000000000000000000000000000 hsw margin=1,1,1,1
        ///
        /// [Option]
        /// hsw: horizontal read from south west. (ex. chess program)
        /// margin=1,1,1,1: offset from origin point. north,east,south,west.
        /// color=red: color.
        /// </summary>
        /// <param name="line">コマンドライン。</param>
        /// <returns>コマンドライン オブジェクト。</returns>
        public static ICommandline Parse(string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException("line");
            }

            string commandName = string.Empty;
            long number = 0L;
            ScanOrder scanOrder = ScanOrder.None;
            IMargin margin = new Margin();
            int colorA = -1;
            int colorR = -1;
            int colorG = -1;
            int colorB = -1;

            string[] tokens = line.Split(' ');
            for (int iToken = 0; iToken < tokens.Length; iToken++)
            {
                switch (tokens[iToken])
                {
                    case "select":
                        commandName = tokens[iToken];
                        iToken++;
                        for (; iToken < tokens.Length; iToken++)
                        {
                            string token = tokens[iToken].Trim();
                            if (token.StartsWith("0b", false, CultureInfo.CurrentCulture) && 2 < token.Length)
                            {
                                // 2進数表記だ。頭の２文字を取る。
                                token = token.Substring("0b".Length);

                                // 頭の 0b は、２進数を表す記号。
                                // 特別な仕様として、見やすいように任意でコロンで区切りをつけることができるものとするぜ☆（＾～＾）
                                // 0b:00000000:00000000:00000000:00000000:00000000:00000000:00000000:00000000:
                                token = token.Replace(":", string.Empty);

                                number = Convert.ToInt32(token, 2);
                            }
                            else if ("hsw" == token)
                            {
                                scanOrder = ScanOrder.Hsw;
                            }
                            else if (token.StartsWith("margin=", false, CultureInfo.CurrentCulture))
                            {
                                token = token.Substring("margin=".Length);

                                // 残りはカンマ区切り。
                                string[] numbers = token.Split(',');
                                if (3 < numbers.Length)
                                {
                                    if (int.TryParse(numbers[0], out int num))
                                    {
                                        margin.North = num;
                                    }

                                    if (int.TryParse(numbers[1], out num))
                                    {
                                        margin.East = num;
                                    }

                                    if (int.TryParse(numbers[2], out num))
                                    {
                                        margin.South = num;
                                    }

                                    if (int.TryParse(numbers[3], out num))
                                    {
                                        margin.West = num;
                                    }
                                }
                            }
                            else if (token.StartsWith("color=", false, CultureInfo.CurrentCulture))
                            {
                                token = token.Substring("color=".Length);

                                // 残りはカンマ区切り。ARGB
                                string[] numbers = token.Split(',');
                                if (3 < numbers.Length)
                                {
                                    if (int.TryParse(numbers[0], out int num))
                                    {
                                        colorA = num;
                                    }

                                    if (int.TryParse(numbers[1], out num))
                                    {
                                        colorR = num;
                                    }

                                    if (int.TryParse(numbers[2], out num))
                                    {
                                        colorG = num;
                                    }

                                    if (int.TryParse(numbers[3], out num))
                                    {
                                        colorB = num;
                                    }
                                }
                            }
                        }

                        break;
                }
            }

            // デフォルト値
            switch (commandName)
            {
                case "select":
                    if (-1 == colorA && -1 == colorR && -1 == colorG && -1 == colorB)
                    {
                        // 半透明の黄色☆
                        colorA = 128;
                        colorR = 255;
                        colorG = 255;
                        colorB = 0;
                    }

                    break;
            }

            return new Commandline(
                number,
                scanOrder,
                margin,
                colorA,
                colorR,
                colorG,
                colorB);
        }
    }
}
