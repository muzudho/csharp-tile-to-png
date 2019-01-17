using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]
namespace Grayscale.TileToPng
{
    using NLua;

    /// <summary>
    /// プログラム。
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Luaファイル名
        /// </summary>
        public const string LuaFileName = "Work/TileToPng.lua";

        /// <summary>
        /// タイル横幅。
        /// </summary>
        public static readonly int TileWidth;

        /// <summary>
        /// タイル縦幅。
        /// </summary>
        public static readonly int TileHeight;

        /// <summary>
        /// Luaスクリプト・ファイルを１個しか使わないのなら、インスタンス１つで十分。
        /// </summary>
        static Lua lua;

        /// <summary>
        /// Initializes static members of the <see cref="Program"/> class.
        /// 静的コンストラクター
        /// </summary>
        static Program()
        {
            // Luaの初期設定
            lua = new Lua();

            // 初期化
            lua.LoadCLRPackage();

            // ファイルの読み込み
            lua.DoFile(LuaFileName);

            // タイル画像の横幅
            var value = lua["TILE_WIDTH"];
            if (!(value is double))
            {
                value = 32d;
            }

            TileWidth = (int)((double)value);

            // タイル画像の縦幅
            value = lua["TILE_HEIGHT"];
            if (!(value is double))
            {
                value = 32d;
            }

            TileHeight = (int)((double)value);
        }

        /// <summary>
        /// Gets or sets ファイル パスの短縮名が入っている。
        /// </summary>
        public static FileListModel FileListModel { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new ConsoleTraceListener());

            FileListModel = FileListModel.ReadFile("image-file-path.toml");
#if DEBUG
            // デバッグ表示
            Trace.WriteLine(string.Format(
                CultureInfo.CurrentCulture,
                "fileList.ImagePath.Count: {0}.",
                FileListModel.ImagePath.Count));
            foreach (var pair in FileListModel.ImagePath)
            {
                Trace.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}: '{1}'.",
                    pair.Key,
                    pair.Value));
            }
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
