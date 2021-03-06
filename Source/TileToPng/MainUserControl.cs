﻿namespace Grayscale.TileToPng
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using Grayscale.TileToPng.Actions;
    using Grayscale.TileToPng.CommandLineModel;
    using Grayscale.TileToPng.Menu;
    using Grayscale.TileToPng.Options;

    /// <summary>
    /// ユーザーコントロール。
    /// </summary>
    public partial class MainUserControl : UserControl
    {
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
        CircleSizeHandle circleSizeHandle;

        /// <summary>
        /// グリッド
        /// </summary>
        Grid grid;

        /// <summary>
        /// Gets or sets レイヤー表示（編集レイヤー・ラジオボタン）
        /// </summary>
        RectangleF[] layersEditsRadiobuttons;

        /// <summary>
        /// Gets or sets レイヤー表示（可視レイヤー・チェックボックス）
        /// </summary>
        Rectangle[] layersVisibledCheckboxes;

        /// <summary>
        /// Gets or sets レイヤー表示。
        /// </summary>
        bool[] layersVisibled;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainUserControl"/> class.
        /// ユーザーコントロール。
        /// </summary>
        public MainUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets 横幅。
        /// </summary>
        public static int GridMaxWidth => 30;

        /// <summary>
        /// Gets 縦幅。
        /// </summary>
        public static int GridMaxHeight => 30;

        /// <summary>
        /// Gets レイヤー。
        /// </summary>
        public static int GridMaxLayer => 5;

        /// <summary>
        /// Gets or sets タイル マップ 構造。
        /// </summary>
        public TileMapModel TileMapModel { get; set; }

        /// <summary>
        /// Gets or sets （コピー＆ペースト）
        /// </summary>
        public string ClipboardFileName { get; set; }

        /// <summary>
        /// Gets or sets （コピー＆ペースト）
        /// </summary>
        public Image ClipboardImage { get; set; }

        /// <summary>
        /// Gets or sets 水色のカーソル
        /// </summary>
        public RectangleCursor TileCursor { get; set; }

        /// <summary>
        /// Gets or sets 選択範囲関連
        /// </summary>
        public long Selection { get; set; }

        /// <summary>
        /// Gets or sets 捜査の順番。
        /// </summary>
        public ScanOrder SelectionScanOrder { get; set; }

        /// <summary>
        /// Gets or sets 選択マージン。
        /// </summary>
        public IMargin SelectionMargin { get; set; }

        /// <summary>
        /// Gets or sets 選択範囲用の、半透明の指定色のブラシ。
        /// </summary>
        public Brush SelectionBrush { get; set; }

        /// <summary>
        /// Gets or sets 自作メニュー・バー
        /// </summary>
        public IMenubar Menubar { get; set; }

        /// <summary>
        /// ウィンドウ・サイズ変更時
        /// </summary>
        public void OnSizeUpdated()
        {
            // メニュー・バー
            this.Menubar.UpdateSize(this.Width);
        }

        /// <summary>
        /// カーソルが指しているタイル項目。
        /// </summary>
        /// <returns>タイルマップ項目。</returns>
        public TileMapItem GetTileOnCursor()
        {
            return this.TileMapModel.GetItem(this.TileCursor.ZOrder, this.TileCursor.TopIndex, this.TileCursor.LeftIndex);
        }

        /// <summary>
        /// カーソルが指しているタイル項目に内容をセットする。
        /// </summary>
        /// <param name="item">タイル項目。</param>
        public void SetTileOnCursor(TileMapItem item)
        {
            this.TileMapModel.SetItem(this.TileCursor.ZOrder, this.TileCursor.TopIndex, this.TileCursor.LeftIndex, item);
        }

        private void UcMain_Load(object sender, EventArgs e)
        {
            // ────────────────────────────────────────
            // 設定
            // ────────────────────────────────────────
            this.engineOptions = new EngineOptionsImpl();

            // ────────────────────────────────────────
            // ペンとブラシ
            // ────────────────────────────────────────
            this.penGold = new Pen(Brushes.Gold, 3.0f);
            this.penBlue = new Pen(Brushes.Blue, 3.0f);
            this.brushGreen = new SolidBrush(Color.Green);
            this.SelectionBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 0));

            // ────────────────────────────────────────
            // グリッド
            // ────────────────────────────────────────
            // 原点
            // セルのサイズ
            this.grid = new Grid(
                60.0f,
                40.0f,
                Program.TileWidth,
                Program.TileHeight);

            // リサイズハンドル（リサイズ用のつまみ）は、グリッドの右下に付く。
            // タテ線５本分の横幅と、ヨコ線５本分の横幅
            this.circleSizeHandle = new CircleSizeHandle(
                (5 * this.grid.CellW) + this.grid.OriginX - (20.0f / 2),
                (5 * this.grid.CellH) + this.grid.OriginY - (20.0f / 2));

            this.UpdateGridSize();

            // ────────────────────────────────────────
            // 選択範囲
            // ────────────────────────────────────────
            this.SelectionScanOrder = ScanOrder.None;
            this.SelectionMargin = new Margin();

            // ────────────────────────────────────────
            // カーソル
            // ────────────────────────────────────────
            this.TileCursor = new RectangleCursor();
            this.UpdateTileCursorPixel();

            // ────────────────────────────────────────
            // テーブル
            // ────────────────────────────────────────
            // とりあえず固定長で。
            this.TileMapModel = new TileMapModel();

            // ────────────────────────────────────────
            // レイヤー表示（編集レイヤー・ラジオボタン）
            // ────────────────────────────────────────
            this.layersEditsRadiobuttons = new RectangleF[MainUserControl.GridMaxLayer];
            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                this.layersEditsRadiobuttons[iLayer] = new RectangleF(
                    32.0f,
                    ((MainUserControl.GridMaxLayer - 1 - iLayer) * this.grid.CellH) + 20.0f - (20.0f / 2) + this.grid.OriginY,
                    20.0f,
                    20.0f);
            }

            // ────────────────────────────────────────
            // レイヤー表示（可視レイヤー・チェックボックス）
            // ────────────────────────────────────────
            this.layersVisibled = new bool[MainUserControl.GridMaxLayer];
            this.layersVisibledCheckboxes = new Rectangle[MainUserControl.GridMaxLayer];
            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                // 初期状態はすべて可視
                this.layersVisibled[iLayer] = true;

                this.layersVisibledCheckboxes[iLayer] = new Rectangle(
                    8,
                    (int)(((MainUserControl.GridMaxLayer - 1 - iLayer) * this.grid.CellH) + 20.0f - (20.0f / 2) + this.grid.OriginY),
                    20,
                    20);
            }

            // ────────────────────────────────────────
            // メニュー・バー
            // ────────────────────────────────────────
            this.Menubar = new Menubar(0, 0, 10, 20, this.Font);
            this.OnSizeUpdated();
        }

        private void UpdateGridSize()
        {
            // ヨコ線とタテ線の終点
            this.grid.End = new PointF(
                this.circleSizeHandle.Bounds.X + (this.circleSizeHandle.Bounds.Width / 2),
                this.circleSizeHandle.Bounds.Y + (this.circleSizeHandle.Bounds.Height / 2));
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sender">送信元。</param>
        /// <param name="e">イベント。</param>
        private void UcMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // ────────────────────────────────────────
            // レイヤー関連（編集レイヤー・ラジオボタン）
            // ────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                if (iLayer == this.TileCursor.ZOrder)
                {
                    g.FillEllipse(this.brushGreen, this.layersEditsRadiobuttons[iLayer]);
                }

                g.DrawEllipse(this.penGold, this.layersEditsRadiobuttons[iLayer]);
            }

            // ────────────────────────────────────────
            // レイヤー関連（可視レイヤー・チェックボックス）
            // ────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                if (this.layersVisibled[iLayer])
                {
                    g.FillRectangle(this.brushGreen, this.layersVisibledCheckboxes[iLayer]);
                }

                g.DrawRectangle(this.penGold, this.layersVisibledCheckboxes[iLayer]);
            }

            // ────────────────────────────────────────
            // グリッド
            // ────────────────────────────────────────

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
            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                if (this.layersVisibled[iLayer])
                {
                    for (int y = 0; y < MainUserControl.GridMaxHeight; y++)
                    {
                        for (int x = 0; x < MainUserControl.GridMaxWidth; x++)
                        {
                            var tile = this.TileMapModel.GetItem(iLayer, y, x);
                            if (tile != null && tile.Image != null)
                            {
                                g.DrawImage(
                                    this.TileMapModel.GetItem(iLayer, y, x).Image,
                                    (x * this.grid.CellW) + this.grid.OriginX,
                                    (y * this.grid.CellH) + this.grid.OriginY);
                            }
                        }
                    }
                }
            }

            // ────────────────────────────────────────
            // 選択範囲
            // ────────────────────────────────────────
            this.PaintSelection(g, false);

            // ────────────────────────────────────────
            // 青い四角のカーソル
            // ────────────────────────────────────────
            g.DrawRectangle(this.penBlue, this.TileCursor.Bounds.X, this.TileCursor.Bounds.Y, this.TileCursor.Bounds.Width, this.TileCursor.Bounds.Height);

            // 「つまみ」を角っこに置きます。
            if (this.circleSizeHandle.Pressing)
            {
                g.DrawEllipse(this.penBlue, this.circleSizeHandle.Bounds);
            }
            else
            {
                g.DrawEllipse(this.penGold, this.circleSizeHandle.Bounds);
            }

            // ────────────────────────────────────────
            // メニュー・バー
            // ────────────────────────────────────────
            this.Menubar.Paint(g);
        }

        private void PaintSelection(Graphics g, bool isWriteCase)
        {
            if (
                0 < this.Selection
                &&
                !(0 == this.grid.Cols || 0 == this.grid.Rows))
            {
                string binary = Convert.ToString(this.Selection, 2);

                // int height = (int)(this.circleSizeHandle.Bounds.Height / this.grid.CellH);
                // int width = (int)(this.circleSizeHandle.Bounds.Width / this.grid.CellW);
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
                        (((int)((this.grid.End.Y - this.grid.OriginY) / this.grid.CellH) - 1) * this.grid.CellH) + oy);
                }
                else
                {
                    // 未実装
                    origin = default(PointF);
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
                            x = (int)((((iScan % cols) + this.SelectionMargin.West) * this.grid.CellW) + origin.X);
                            break;
                        default:
                            x = (int)(((iScan % rows) * this.grid.CellW) + origin.X);
                            break;
                    }

                    int y;
                    switch (this.SelectionScanOrder)
                    {
                        case ScanOrder.Hsw:
                            y = (int)((((iScan / cols) + this.SelectionMargin.South) * -this.grid.CellH) + origin.Y);
                            break;
                        default:
                            y = (int)((iScan / rows * this.grid.CellH) + origin.Y);
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
                                this.grid.CellH);
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

                if (MainUserControl.GridMaxWidth < cols)
                {
                    cols = MainUserControl.GridMaxWidth;
                }

                if (MainUserControl.GridMaxHeight < rows)
                {
                    rows = MainUserControl.GridMaxHeight;
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
            // タイムスタンプ
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
                // Graphicsオブジェクトを取得
                Graphics g = null;

                try
                {
                    using (var bitmap = new Bitmap(
                        (int)(cols * this.grid.CellW),
                        (int)(rows * this.grid.CellH)))
                    {
                        g = Graphics.FromImage(bitmap);

                        // とりあえず画像描画
                        for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
                        {
                            if (this.layersVisibled[iLayer])
                            {
                                for (int row = 0; row < rows; row++)
                                {
                                    for (int col = 0; col < cols; col++)
                                    {
                                        if (null != this.TileMapModel.GetItem(iLayer, row, col))
                                        {
                                            // マージン無し
                                            g.DrawImage(
                                                this.TileMapModel.GetItem(iLayer, row, col).Image,
                                                col * this.grid.CellW,
                                                row * this.grid.CellH);
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
            if (this.circleSizeHandle.Bounds.Contains(e.Location))
            {
                this.circleSizeHandle.Pressing = true;
                isRefresh = true;
            }

            // ────────────────────────────────────────
            // メニュー・バー
            // ────────────────────────────────────────
            if (this.Menubar.OnMouseLeftDown(e.Location))
            {
                isRefresh = true;
                goto gt_Refresh;
            }

            // ────────────────────────────────────────
            // レイヤー関連（編集レイヤー・ラジオボタン）
            // ────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
            {
                if (this.layersEditsRadiobuttons[iLayer].Contains(e.Location))
                {
                    this.TileCursor.ZOrder = iLayer;
                    isRefresh = true;
                    break;
                }
            }

            // ────────────────────────────────────────
            // レイヤー関連（可視レイヤー・チェックボックス）
            // ────────────────────────────────────────
            for (int iLayer = 0; iLayer < MainUserControl.GridMaxLayer; iLayer++)
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
                this.TileCursor.SetLocationOnTable(
                    (int)((e.X - this.grid.OriginX) / this.grid.CellW),
                    (int)((e.Y - this.grid.OriginY) / this.grid.CellH));

                this.UpdateTileCursorPixel();
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
            if (this.circleSizeHandle.Pressing)
            {
                this.circleSizeHandle.Pressing = false;
                this.Refresh();
            }
        }

        private void UcMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.circleSizeHandle.Pressing)
            {
                this.circleSizeHandle.SetLocationPixel(
                    e.X - (this.circleSizeHandle.Bounds.Width / 2),
                    e.Y - (this.circleSizeHandle.Bounds.Height / 2));
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
                    // ────────────────────────────────────────
                    // 編集
                    // ────────────────────────────────────────
                    case Keys.C:
                        // コピー
                        {
                            new ControlCStep()
                            {
                                MainUserControl = this,
                            }.Perform();
                        }

                        break;

                    case Keys.X:
                        // カット
                        {
                            new ControlXStep()
                            {
                                MainUserControl = this,
                            }.Perform();
                        }

                        break;

                    case Keys.V:
                        // ペースト
                        {
                            new ControlVStep()
                            {
                                MainUserControl = this,
                            }.Perform();
                        }

                        break;

                    // ────────────────────────────────────────
                    // 入出力
                    // ────────────────────────────────────────
                    case Keys.S:
                        // 保存
                        {
                            new SavingTileMapStep()
                            {
                                MainUserControl = this,
                                OutputSaveFile = "TileToPng_save.txt",
                            }.Perform();
                        }

                        break;

                    case Keys.L:
                        // 前回の作業状態から再開。
                        {
                            new LoadingTileMapStep()
                            {
                                MainUserControl = this,
                                InputSaveFile = "TileToPng_save.txt",
                            }.Perform();
                        }

                        break;
                }
            }

            switch (e.KeyCode)
            {
                // ────────────────────────────────────────
                // カーソル移動
                // ────────────────────────────────────────
                // 改行
                case Keys.Enter:
                    this.DoNewline();
                    this.UpdateTileCursorPixel();
                    this.Refresh();
                    break;

                // ↑
                case Keys.Up:
                    this.TileCursor.MoveToBottom(-1);
                    this.UpdateTileCursorPixel();
                    this.Refresh();
                    break;

                // →
                case Keys.Right:
                    this.TileCursor.MoveToRight(1);
                    this.UpdateTileCursorPixel();
                    this.Refresh();
                    break;

                // ↓
                case Keys.Down:
                    this.TileCursor.MoveToBottom(1);
                    this.UpdateTileCursorPixel();
                    this.Refresh();
                    break;

                // ←
                case Keys.Left:
                    this.TileCursor.MoveToRight(-1);
                    this.UpdateTileCursorPixel();
                    this.Refresh();
                    break;

                // ────────────────────────────────────────
                // 編集
                // ────────────────────────────────────────
                // 削除
                case Keys.Delete:
                    {
                        this.TileMapModel.SetItem(this.TileCursor.ZOrder, this.TileCursor.TopIndex, this.TileCursor.LeftIndex, TileMapItem.Empty);
                        this.Refresh();
                    }

                    break;

                // ────────────────────────────────────────
                // 入出力
                // ────────────────────────────────────────
                // case Keys.PrintScreen: //プリント・スクリーン・キーは利かないようだ。
                // 画像出力
                case Keys.P:
                    this.WritePng();
                    break;
            }
        }

        private void UcMain_DragEnter(object sender, DragEventArgs e)
        {
            // コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 改行
        /// </summary>
        private void DoNewline()
        {
            if (
                this.TileCursor.TopIndex + 1 < GridMaxHeight)
            {
                // 強制改行
                this.TileCursor.SetLeft(0);
                this.TileCursor.MoveToBottom(1);
            }
        }

        private void UcMain_DragDrop(object sender, DragEventArgs e)
        {
            // コントロール内にドロップされたとき実行される
            // ドロップされたすべてのファイル名を取得する
            string[] fileNames =
                (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // カーソルのある位置から順番に追加する。
            foreach (string name in fileNames)
            {
                if (
                    GridMaxWidth <= this.TileCursor.LeftIndex)
                {
                    this.DoNewline();
                }

                if (
                    this.TileCursor.LeftIndex < GridMaxWidth
                    &&
                    this.TileCursor.TopIndex < GridMaxHeight)
                {
                    // とりあえず画像読み込み
                    this.TileMapModel.SetItem(this.TileCursor.ZOrder, this.TileCursor.TopIndex, this.TileCursor.LeftIndex, new TileMapItem(name, Image.FromFile(name)));

                    // カーソルを右へ。
                    this.TileCursor.MoveToRight(1);
                    this.UpdateTileCursorPixel();
                    this.Refresh();
                }
            }
        }

        private void UpdateTileCursorPixel()
        {
            Trace.WriteLine(string.Format(
                CultureInfo.CurrentCulture,
                "TileCursor.LeftIndex: {0}, TileCursor.TopIndex: {1}.",
                this.TileCursor.LeftIndex,
                this.TileCursor.TopIndex));
            this.TileCursor.SetBoundsOnPixel(
                (this.TileCursor.LeftIndex * this.grid.CellW) + this.grid.OriginX,
                (this.TileCursor.TopIndex * this.grid.CellH) + this.grid.OriginY,
                this.grid.CellW,
                this.grid.CellH);
        }
    }
}
