using System.Windows.Forms;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommandlineUserControl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public CommandlineUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((Form1)this.ParentForm).UpdateCommandline(((TextBox)sender).Text);
            }
        }
    }
}
