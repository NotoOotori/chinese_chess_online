using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace platform.chess_lobby
{
    /// <summary>
    /// 重做pygame中的Rect类
    /// </summary>
    /// <typeparam name="Double">整数或浮点数</typeparam>
    public class Rect
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="Rect{T}"/>的新实例
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public Rect(Double width, Double height)
        {
            this.width = width;
            this.height = height;
        }

        #endregion

        #region ' Properties '

        #region ' Private '

        private Double _x { get; set; } = 0;
        private Double _y { get; set; } = 0;
        private Double _width { get; set; }
        private Double _height { get; set; }

        #endregion

        #region ' Public '

        /// <summary>
        /// 左上顶点的横坐标
        /// </summary>
        public Double x { get { return _x; } set { _x = value; } }
        /// <summary>
        /// 左上顶点的纵坐标
        /// </summary>
        public Double y { get { return _y; } set { _y = value; } }

        /// <summary>
        /// 上边
        /// </summary>
        public Double top { get { return _y; } set { _y = value; } }
        /// <summary>
        /// 左边
        /// </summary>
        public Double left { get { return _x; } set { _x = value; } }

        /// <summary>
        /// 中心的横坐标
        /// </summary>
        public Double centerx
        {
            get { return _x + _width / 2; }
            set { _x = value - _width / 2; }
        }
        /// <summary>
        /// 中心的纵坐标
        /// </summary>
        public Double centery
        {
            get { return _y + _height / 2; }
            set { _y = value - _height / 2; }
        }
        
        /// <summary>
        /// 宽
        /// </summary>
        public Double width { get { return _width; } set { _width = value; } }
        /// <summary>
        /// 高
        /// </summary>
        public Double height { get { return _height; } set { _height = value; } }

        #endregion

        #endregion
    }
}
