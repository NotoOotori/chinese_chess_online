using System;

namespace platform.chess_lobby
{
    [Flags]
    public enum ChessColour
    {
        NONE = 2,
        RED = 3,
        BLACK = 1,
    }

    [Flags]
    public enum ReflectionType
    {
        None = 0,
        /// <summary>
        /// 上下翻转
        /// </summary>
        VerticalReflection = 1,
        /// <summary>
        /// 左右翻转
        /// </summary>
        HorizontalReflection = 2,
        /// <summary>
        /// 旋转180°
        /// </summary>
        PointReflection = 3
    }

    public class MyConvert
    {
        #region ' Methods '

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colour">代表颜色的小写字符</param>
        /// <returns></returns>
        public static ChessColour to_chess_colour(Char colour)
        {
            switch(colour)
            {
                default:
                    throw new ArgumentOutOfRangeException("颜色有误");
                case 'b':
                    return ChessColour.BLACK;
                case 'r':
                    return ChessColour.RED;
            }
        }
        
        public static ChessColour to_chess_colour(Boolean is_red)
        {
            if (is_red)
                return ChessColour.RED;
            else
                return ChessColour.BLACK;
        }

        #endregion
    }

    [Flags]
    public enum MoveType
    {
        INVALID_MOVE = 0,
        NORMAL_MOVE = 1,
        CAPTURE = 3
    }
}
