namespace Grayscale.TileToPng
{
    using System.Windows.Forms;

    /// <summary>
    /// ユーザーコントロール。
    /// </summary>
    public partial class CommandlineUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandlineUserControl"/> class.
        /// </summary>
        public CommandlineUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// キー押下時。
        /// </summary>
        /// <param name="sender">送信元。</param>
        /// <param name="e">イベント。</param>
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((Form1)this.ParentForm).UpdateCommandline(((TextBox)sender).Text);
            }
        }
    }
}
