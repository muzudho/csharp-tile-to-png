﻿namespace Grayscale.TileToPng.Actions.SavingWork
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
