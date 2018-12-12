using Grayscale.TileToPng.CommandLine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// メイン フォーム。
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public void UpdateCommandline(string line)
        {
            ICommandline commandline = UtilCommandlineParser.Parse(line);

            this.ucMain1.Selection = commandline.Number;
            this.ucMain1.SelectionScanOrder = commandline.ScanOrder;
            this.ucMain1.SelectionMargin = commandline.Margin;
            this.ucMain1.SelectionBrush = new SolidBrush(Color.FromArgb(commandline.ColorA, commandline.ColorR, commandline.ColorG, commandline.ColorB));


            string binary = Convert.ToString(this.ucMain1.Selection, 2);
            MessageBox.Show("デバッグ binary=" + binary + " scanOrder=" + commandline.ScanOrder + " margin=" + commandline.Margin.ToString());
            this.Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.ucMain1.OnSizeUpdated();
        }
    }
}
