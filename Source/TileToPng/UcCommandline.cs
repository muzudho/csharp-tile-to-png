using Grayscale.TileToPng.CommandLine;
using System.Windows.Forms;

namespace Grayscale.TileToPng
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UcCommandline : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UcCommandline()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((Form1)this.ParentForm).UpdateCommandline(((TextBox)sender).Text);
            }
        }
    }
}
