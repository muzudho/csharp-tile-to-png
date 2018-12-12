using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.TileToPng.Actions.LoadingWork
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextModel
    {
        /// <summary>
        /// 
        /// </summary>
        public MainUserControl MainUserControl { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainUserControl"></param>
        public ContextModel(MainUserControl mainUserControl)
        {
            MainUserControl = mainUserControl;
        }
    }
}
