namespace Grayscale.TileToPng
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Grayscale.TileToPng.CommandLineModel;

    /// <summary>
    /// メイン フォーム。
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 更新。
        /// </summary>
        /// <param name="line">コマンドライン。</param>
        public void UpdateCommandline(string line)
        {
            ICommandline commandline = UtilCommandlineParser.Parse(line);

            this.ucMain1.Selection = commandline.Number;
            this.ucMain1.SelectionScanOrder = commandline.ScanOrder;
            this.ucMain1.SelectionMargin = commandline.Margin;
            this.ucMain1.SelectionBrush = new SolidBrush(Color.FromArgb(commandline.ColorA, commandline.ColorR, commandline.ColorG, commandline.ColorB));

            string binary = Convert.ToString(this.ucMain1.Selection, 2);
            var message = string.Format(
                CultureInfo.CurrentCulture,
                "デバッグ binary={0} scanOrder={1} margin={2}",
                binary,
                commandline.ScanOrder,
                commandline.Margin.ToString());
            MessageBox.Show(message);
            this.Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.ucMain1.OnSizeUpdated();
        }
    }
}
