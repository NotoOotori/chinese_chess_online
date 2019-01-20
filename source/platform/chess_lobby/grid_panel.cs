using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform.chess_lobby
{
    /// <summary>
    /// 作为格点的<see cref="Panel"/>
    /// </summary>
    class GridPanel : Panel
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="chess_lobby.GridPanel"/>类的新实例
        /// </summary>
        public GridPanel()
        {
            ;
        }

        #endregion

        #region ' Methods '

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }

        #endregion
    }
}
