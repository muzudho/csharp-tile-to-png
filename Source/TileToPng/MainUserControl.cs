using Grayscale.TileToPng.CommandLineModel;
using Grayscale.TileToPng.Menu;
using Grayscale.TileToPng.Options;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LoadingWork = Grayscale.TileToPng.Actions.LoadingWork;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MainUserControl()
        {
            InitializeComponent();
        }

        #region プロパティー

        /// <summary>
        /// 設定。
        /// </summary>
        IEngineOptions engineOptions;

        /// <summary>
        /// 金色のペン。
        /// </summary>
        Pen penGold;

        /// <summary>
        /// 青色のペン。
        /// </summary>
        Pen penBlue;

        /// <summary>
        /// 緑のブラシ。
        /// </summary>
        Brush brushGreen;

        /// <summary>
        /// リサイズ用のつまみ。
        /// </summary>
        RectangleF resizeHandle;
        bool resizerPressing;

        /// <summary>
        /// グリッド
        /// </summary>
        Grid grid;
        /// <summary>
        /// ファイル名のテーブル。
        /// </summary>
        public string[][][] gridFilenames;
        public Image[][][] gridImages;
        public const int GRID_MAX_WIDTH = 30;
        public const int GRID_MAX_HEIGHT = 30;
        public const int GRID_MAX_LAYER = 5;

        /// <summary>
        /// （コピー＆ペースト）
        /// </summary>
        string clipboardFilename;
        Image clipboardImage;

        /// <summary>
        /// 水色のカーソル
        /// </summary>
        RectangleF cursor;
        Point cursorPos;
        int cursorZ;

        /// <summary>
        /// レイヤー表示（編集レイヤー・ラジオボタン）
        /// </summary>
        RectangleF[] layersEditsRadiobuttons;

        /// <summary>
        /// レイヤー表示（可視レイヤー・チェックボックス）
        /// </summary>
        Rectangle[] layersVisibledCheckboxes;
        bool[] layersVisibled;

        /// <summary>
        /// 選択範囲関連
        /// </summary>
        public long Selection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScanOrder SelectionScanOrder { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IMargin SelectionMargin { get; set; }

        /// <summary>
        /// 選択範囲用の、半透明の指定色のブラシ。
        /// </summary>
        public Brush SelectionBrush { get; set; }

        /// <summary>
        /// 自作メニュー・バー
        /// </summary>
        public IMenubar Menubar { get; set; }

        #endregion

        private void UcMain_Load(object sender, EventArgs e)
        {
            //────────────────────────────────────────
            // 設定
            //────────────────────────────────────────
            this.engineOptions = new EngineOptionsImpl();

            //────────────────────────────────────────
            // ペンとブラシ
            //────────────────────────────────────────
            this.penGold = new Pen(Brushes.Gold, 3.0f);
            this.penBlue = new Pen(Brushes.Blue, 3.0f);
            this.brushGreen = new SolidBrush(Color.Green);
            this.SelectionBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 0));

            //────────────────────────────────────────
            // グリッド
            //────────────────────────────────────────
            this.grid = new Grid(
                // 原点
                60.0f,
                40.0f,
                // セルのサイズ
                Program.tileWidth,
                Program.tileHeight
                );

            // リサイズハンドル（リサイズ用のつまみ）は、グリッドの右下に付く。
            this.resizeHandle = new RectangleF(
                //タテ線５本分の横幅
                5 * this.grid.CellW + this.grid.OriginX - 20.0f / 2,
                //ヨコ線５本分の横幅
                5 * this.grid.CellH + this.grid.OriginY - 20.0f / 2,
                20.0f,
                20.0f);

            this.UpdateGridSize();

            //────────────────────────────────────────
            // 選択範囲
            //────────────────────────────────────────
            this.SelectionScanOrder = ScanOrder.None;
            this.SelectionMargin = new Margin();

            //────────────────────────────────────────
            // カーソル
            //────────────────────────────────────────
            this.cursorPos = new Point();
            this.cursor = new RectangleF();
            this.UpdateCursor();

            //────────────────────────────────────────
            // テーブル
            //────────────────────────────────────────
            // とりあえず固定長で。
            this.gridFilenames = new string[MainUserControl.GRID_MAX_LAYER][][];
            for (int i = 0; i < this.gridFilenames.Length; i++)
            {
                this.gridFilenames[i] = new string[MainUserControl.GRID_MAX_HEIGHT][];
                for (int j = 0; j < this.gridFilenames[i].Length; j++)
                {
                    this.gridFilenames[i][j] = new string[MainUserControl.GRID_MAX_WIDTH];
                }
            }

            this.gridImages = new Image[MainUserControl.GRID_MAX_LAYER][][];
            for (int i = 0; i < this.gridImages.Length; i++)
            {
                this.gridImages[i] = new Image[MainUserControl.GRID_MAX_HEIGHT][];
                for (int j = 0; j < this.gridImages[i].Length; j++)
                {
                    this.gridImages[i][j] = new Image[MainUserControl.GRID_MAX_WIDTH];
                }
            }


            //────────────────────────────────────────
            // レイヤー表示（編集レイヤー・ラジオボタン）
            //────────────────────────────────────────
            this.layersEditsRadiobuttons = new RectangleF[MainUserControl.GRID_MAX_LAYER];
            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                this.layersEditsRadiobuttons[iLayer] = new RectangleF(
                    32.0f,
                    (MainUserControl.GRID_MAX_LAYER - 1 - iLayer) * this.grid.CellH + 20.0f - 20.0f / 2 + this.grid.OriginY,
                    20.0f,
                    20.0f
                    );
            }

            //────────────────────────────────────────
            // レイヤー表示（可視レイヤー・チェックボックス）
            //────────────────────────────────────────
            this.layersVisibled = new bool[MainUserControl.GRID_MAX_LAYER];
            this.layersVisibledCheckboxes = new Rectangle[MainUserControl.GRID_MAX_LAYER];
            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                // 初期状態はすべて可視
                this.layersVisibled[iLayer] = true;

                this.layersVisibledCheckboxes[iLayer] = new Rectangle(
                    8,
                    (int)((MainUserControl.GRID_MAX_LAYER - 1 - iLayer) * this.grid.CellH + 20.0f - 20.0f / 2 + this.grid.OriginY),
                    20,
                    20
                    );
            }

            //────────────────────────────────────────
            // メニュー・バー
            //────────────────────────────────────────
            this.Menubar = new Menubar(0, 0, 10, 20, this.Font);
            this.OnSizeUpdated();
        }


        private void UpdateGridSize()
        {
            // ヨコ線とタテ線の終点
            this.grid.End = new PointF(
                this.resizeHandle.X + this.resizeHandle.Width / 2,
                this.resizeHandle.Y + this.resizeHandle.Height / 2
                );
        }

        private void UpdateCursor()
        {
            this.cursor.X = this.cursorPos.X * this.grid.CellW + this.grid.OriginX;
            this.cursor.Y = this.cursorPos.Y * this.grid.CellH + this.grid.OriginY;
            this.cursor.Width = this.grid.CellW;
            this.cursor.Height = this.grid.CellH;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //────────────────────────────────────────
            // レイヤー関連（編集レイヤー・ラジオボタン）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                if (iLayer == this.cursorZ)
                {
                    g.FillEllipse(this.brushGreen, this.layersEditsRadiobuttons[iLayer]);
                }

                g.DrawEllipse(this.penGold, this.layersEditsRadiobuttons[iLayer]);
            }

            //────────────────────────────────────────
            // レイヤー関連（可視レイヤー・チェックボックス）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                if (this.layersVisibled[iLayer])
                {
                    g.FillRectangle(this.brushGreen, this.layersVisibledCheckboxes[iLayer]);
                }

                g.DrawRectangle(this.penGold, this.layersVisibledCheckboxes[iLayer]);
            }

            //────────────────────────────────────────
            // グリッド
            //────────────────────────────────────────

            // ヨコ線を引きます。
            for (float y = this.grid.OriginY; y <= this.grid.End.Y; y += this.grid.CellH)
            {
                g.DrawLine(this.penGold, this.grid.OriginX, y, this.grid.End.X, y);
            }

            // タテ線を引きます。
            for (float x = this.grid.OriginX; x <= this.grid.End.X; x += this.grid.CellW)
            {
                g.DrawLine(this.penGold, x, this.grid.OriginY, x, this.grid.End.Y);
            }

            // とりあえず画像描画
            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                if (this.layersVisibled[iLayer])
                {
                    for (int y = 0; y < MainUserControl.GRID_MAX_HEIGHT; y++)
                    {
                        for (int x = 0; x < MainUserControl.GRID_MAX_WIDTH; x++)
                        {
                            if (null != this.gridImages[iLayer][y][x])
                            {
                                g.DrawImage(this.gridImages[iLayer][y][x],
                                    x * this.grid.CellW + this.grid.OriginX,
                                    y * this.grid.CellH + this.grid.OriginY
                                    );
                            }
                        }
                    }
                }
            }

            //────────────────────────────────────────
            // 選択範囲
            //────────────────────────────────────────
            this.PaintSelection(g, false);

            //────────────────────────────────────────
            // 青い四角のカーソル
            //────────────────────────────────────────
            g.DrawRectangle(this.penBlue, this.cursor.X, this.cursor.Y, this.cursor.Width, this.cursor.Height);

            // 「つまみ」を角っこに置きます。
            if (this.resizerPressing)
            {
                g.DrawEllipse(this.penBlue, this.resizeHandle);
            }
            else
            {
                g.DrawEllipse(this.penGold, this.resizeHandle);
            }

            //────────────────────────────────────────
            // メニュー・バー
            //────────────────────────────────────────
            this.Menubar.Paint(g);
        }

        private void PaintSelection(Graphics g, bool isWriteCase)
        {
            if (
                0 < this.Selection
                &&
                !(0 == this.grid.Cols || 0 == this.grid.Rows)
            )
            {
                string binary = Convert.ToString(this.Selection, 2);
                int height = (int)(this.resizeHandle.Height / this.grid.CellH);
                int width = (int)(this.resizeHandle.Width / this.grid.CellW);

                PointF origin;
                if (ScanOrder.Hsw == this.SelectionScanOrder)
                {
                    float ox;
                    float oy;
                    if (isWriteCase)
                    {
                        ox = 0.0f;
                        oy = 0.0f;
                    }
                    else
                    {
                        ox = this.grid.OriginX;
                        oy = this.grid.OriginY;
                    }

                    origin = new PointF(
                        ox,
                        ((int)((this.grid.End.Y - this.grid.OriginY) / this.grid.CellH) - 1) * this.grid.CellH + oy
                        );
                }
                else
                {
                    // 未実装
                    origin = new PointF(); ;
                }

                for (int iScan = 0; iScan < binary.Length; iScan++)
                {
                    char figure = binary[binary.Length - 1 - iScan];

                    // マージンを省いたテーブル・サイズ。
                    int cols = this.grid.Cols - this.SelectionMargin.East - this.SelectionMargin.West;
                    int rows = this.grid.Rows - this.SelectionMargin.North - this.SelectionMargin.South;

                    int x;
                    switch (this.SelectionScanOrder)
                    {
                        case ScanOrder.Hsw:
                            x = (int)((iScan % cols + this.SelectionMargin.West) * this.grid.CellW + origin.X);
                            break;
                        default:
                            x = (int)(iScan % rows * this.grid.CellW + origin.X);
                            break;
                    }


                    int y;
                    switch (this.SelectionScanOrder)
                    {
                        case ScanOrder.Hsw:
                            y = (int)((iScan / cols + this.SelectionMargin.South) * -this.grid.CellH + origin.Y);
                            break;
                        default:
                            y = (int)(iScan / rows * this.grid.CellH + origin.Y);
                            break;
                    }

                    switch (figure)
                    {
                        case '1':
                            g.FillRectangle(
                                this.SelectionBrush,
                                x,
                                y,
                                this.grid.CellW,
                                this.grid.CellH
                                );
                            break;
                    }
                }
            }
        }

        private void WritePng()
        {
            // テーブル・サイズ
            int cols, rows;
            {
                cols = (int)((this.grid.End.X - this.grid.OriginX) / this.grid.CellW);
                rows = (int)((this.grid.End.Y - this.grid.OriginY) / this.grid.CellH);

                if (MainUserControl.GRID_MAX_WIDTH < cols)
                {
                    cols = MainUserControl.GRID_MAX_WIDTH;
                }

                if (MainUserControl.GRID_MAX_HEIGHT < rows)
                {
                    rows = MainUserControl.GRID_MAX_HEIGHT;
                }

                /*１マスに、でかい画像を使っていることもあるので、これは理屈が合わなくなるぜ。
                // データを走査
                int dataMaxRow = 0;//1スタートの数字
                int dataMaxCol = 0;
                for (int iLayer = 0; iLayer < UcMain.GRID_MAX_LAYER; iLayer++)
                {
                    for (int iRow = 0; iRow < rows; iRow++)
                    {
                        for (int iCol = 0; iCol < cols; iCol++)
                        {
                            if (null != this.m_gridImages_[iLayer, iRow, iCol])
                            {
                                if (dataMaxCol < iCol + 1)
                                {
                                    dataMaxCol = iCol + 1;
                                }

                                if (dataMaxRow < iRow + 1)
                                {
                                    dataMaxRow = iRow + 1;
                                }
                            }
                        }
                    }
                }

                if (dataMaxCol < cols)
                {
                    cols = dataMaxCol;
                }

                if (dataMaxRow < rows)
                {
                    rows = dataMaxRow;
                }
                */
            }

            // 画像
            //タイムスタンプ
            string timestamp;
            {
                StringBuilder s = new StringBuilder();
                DateTime now = System.DateTime.Now;
                s.Append(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0:0000}{1:00}{2:00}_{3:00}{4:00}{5:00}_{6:000}",
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour,
                    now.Minute,
                    now.Second,
                    now.Millisecond));
                timestamp = s.ToString();
            }

            {
                //Graphicsオブジェクトを取得
                Graphics g = null;

                try
                {
                    using (var bitmap = new Bitmap(
                        (int)(cols * this.grid.CellW),
                        (int)(rows * this.grid.CellH)
                        ))
                    {
                        g = Graphics.FromImage(bitmap);

                        // とりあえず画像描画
                        for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
                        {
                            if (this.layersVisibled[iLayer])
                            {
                                for (int row = 0; row < rows; row++)
                                {
                                    for (int col = 0; col < cols; col++)
                                    {
                                        if (null != this.gridImages[iLayer][row][col])
                                        {
                                            // マージン無し
                                            g.DrawImage(this.gridImages[iLayer][row][col],
                                                col * this.grid.CellW,
                                                row * this.grid.CellH
                                                );
                                        }
                                    }
                                }
                            }
                        }

                        // 選択範囲描画
                        this.PaintSelection(g, true);

                        string file = Path.Combine(Application.StartupPath, "TileToPng_" + timestamp + ".png");
                        bitmap.Save(file, System.Drawing.Imaging.ImageFormat.Png);
                        var message = string.Format(CultureInfo.CurrentCulture, "保存しました： {0}", file);
                        MessageBox.Show(message);
                    }
                }
                finally
                {
                    if (null != g)
                    {
                        g.Dispose();
                    }
                }
            }
        }

        private void UcMain_MouseDown(object sender, MouseEventArgs e)
        {
            bool isRefresh = false;

            // リサイズハンドル
            if (this.resizeHandle.Contains(e.Location))
            {
                this.resizerPressing = true;
                isRefresh = true;
            }

            //────────────────────────────────────────
            // メニュー・バー
            //────────────────────────────────────────
            if (this.Menubar.OnMouseLeftDown(e.Location))
            {
                isRefresh = true;
                goto gt_Refresh;
            }

            //────────────────────────────────────────
            // レイヤー関連（編集レイヤー・ラジオボタン）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                if (this.layersEditsRadiobuttons[iLayer].Contains(e.Location))
                {
                    this.cursorZ = iLayer;
                    isRefresh = true;
                    break;
                }
            }

            //────────────────────────────────────────
            // レイヤー関連（可視レイヤー・チェックボックス）
            //────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                if (this.layersVisibledCheckboxes[iLayer].Contains(e.Location))
                {
                    this.layersVisibled[iLayer] = !this.layersVisibled[iLayer];
                    isRefresh = true;
                    break;
                }
            }

            if (!isRefresh)
            {
                // まだ何もクリックしていなければ、カーソル移動をする。
                this.cursorPos.X = (int)((e.X - this.grid.OriginX) / this.grid.CellW);
                this.cursorPos.Y = (int)((e.Y - this.grid.OriginY) / this.grid.CellH);
                this.UpdateCursor();
                isRefresh = true;
            }

        gt_Refresh:
            if (isRefresh)
            {
                this.Refresh();
            }
        }

        private void UcMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.resizerPressing)
            {
                this.resizerPressing = false;
                this.Refresh();
            }
        }

        private void UcMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.resizerPressing)
            {
                this.resizeHandle.X = e.X - this.resizeHandle.Width / 2;
                this.resizeHandle.Y = e.Y - this.resizeHandle.Height / 2;
                this.UpdateGridSize();
                this.Refresh();
            }
        }

        private void UcMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    //────────────────────────────────────────
                    // 編集
                    //────────────────────────────────────────
                    case Keys.C:
                        // コピー
                        this.clipboardFilename = this.gridFilenames[this.cursorZ][this.cursorPos.Y][this.cursorPos.X];
                        this.clipboardImage = this.gridImages[this.cursorZ][this.cursorPos.Y][this.cursorPos.X];
                        break;

                    case Keys.X:
                        // カット
                        this.clipboardFilename = this.gridFilenames[this.cursorZ][this.cursorPos.Y][this.cursorPos.X];
                        this.clipboardImage = this.gridImages[this.cursorZ][this.cursorPos.Y][this.cursorPos.X];

                        this.gridFilenames[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = null;
                        this.gridImages[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = null;
                        this.Refresh();
                        break;

                    case Keys.V:
                        // ペースト
                        this.gridFilenames[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = this.clipboardFilename;
                        this.gridImages[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = this.clipboardImage;
                        this.Refresh();
                        break;

                    //────────────────────────────────────────
                    // 入出力
                    //────────────────────────────────────────
                    case Keys.S:
                        // 保存
                        this.SaveWorking();
                        break;

                    case Keys.L:
                        // 再開
                        this.LoadWorking();
                        break;
                }
            }

            switch (e.KeyCode)
            {
                //────────────────────────────────────────
                // カーソル移動
                //────────────────────────────────────────
                case Keys.Enter://改行
                    this.DoNewline();
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Up://↑
                    this.cursorPos.Y--;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Right://→
                    this.cursorPos.X++;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Down://↓
                    this.cursorPos.Y++;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                case Keys.Left://←
                    this.cursorPos.X--;
                    this.UpdateCursor();
                    this.Refresh();
                    break;

                //────────────────────────────────────────
                // 編集
                //────────────────────────────────────────
                case Keys.Delete://削除
                    this.gridFilenames[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = null;
                    this.gridImages[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = null;
                    this.Refresh();
                    break;

                //────────────────────────────────────────
                // 入出力
                //────────────────────────────────────────
                //case Keys.PrintScreen: //プリント・スクリーン・キーは利かないようだ。
                case Keys.P://画像出力
                    this.WritePng();
                    break;
            }
        }

        private void UcMain_DragEnter(object sender, DragEventArgs e)
        {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 改行
        /// </summary>
        private void DoNewline()
        {
            if (
                this.cursorPos.Y + 1 < GRID_MAX_HEIGHT
            )
            {
                // 強制改行
                this.cursorPos.X = 0;
                this.cursorPos.Y++;
            }
        }

        private void UcMain_DragDrop(object sender, DragEventArgs e)
        {
            //コントロール内にドロップされたとき実行される
            //ドロップされたすべてのファイル名を取得する
            string[] fileNames =
                (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // カーソルのある位置から順番に追加する。

            foreach (string name in fileNames)
            {
                if (
                    GRID_MAX_WIDTH <= this.cursorPos.X
                    )
                {
                    this.DoNewline();
                }

                if (
                    this.cursorPos.X < GRID_MAX_WIDTH
                    &&
                    this.cursorPos.Y < GRID_MAX_HEIGHT
                    )
                {
                    this.gridFilenames[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = name;

                    // とりあえず画像読み込み
                    this.gridImages[this.cursorZ][this.cursorPos.Y][this.cursorPos.X] = Image.FromFile(name);

                    // カーソルを右へ。
                    this.cursorPos.X++;
                    this.UpdateCursor();
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// 前回の作業状態から再開。
        /// </summary>
        private void LoadWorking()
        {
            {
                LoadingWork.ContextModel context = new LoadingWork.ContextModel(this);
                LoadingWork.InputModel input = new LoadingWork.InputModel();
                LoadingWork.OutputModel output = new LoadingWork.OutputModel();
                LoadingWork.Action.Perform(context, input, output);
            }
        }

        /// <summary>
        /// 作業状態の保存。
        /// </summary>
        private void SaveWorking()
        {
            StringBuilder sb = new StringBuilder();

            for (int iLayer = 0; iLayer < MainUserControl.GRID_MAX_LAYER; iLayer++)
            {
                for (int y = 0; y < MainUserControl.GRID_MAX_HEIGHT; y++)
                {
                    for (int x = 0; x < MainUserControl.GRID_MAX_WIDTH; x++)
                    {
                        string filename = this.gridFilenames[iLayer][y][x];


                        // フォルダーへのパスを「%HOME%」という文字に置き換えて短くするんだぜ☆（＾▽＾）
                        if (null != filename && filename.StartsWith(Application.StartupPath))
                        {
                            filename = "%HOME%" + filename.Substring(Application.StartupPath.Length);
                        }

                        sb.Append(filename);
                        sb.Append(",");
                    }
                    sb.AppendLine();
                }
            }

            string file = Path.Combine(Application.StartupPath, "TileToPng_save.txt");
            File.WriteAllText(file, sb.ToString());
        }

        /// <summary>
        /// ウィンドウ・サイズ変更時
        /// </summary>
        public void OnSizeUpdated()
        {
            // メニュー・バー
            this.Menubar.UpdateSize(this.Width);
        }
    }
}
