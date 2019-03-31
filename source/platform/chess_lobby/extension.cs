using System;
using System.ComponentModel;

namespace platform.chess_lobby
{
    public static class ChessLobbyExtension
    {
        #region ' PieceIdentifier '

        /// <summary>
        /// 将<see cref="PieceIdentifier"/>转化为音频文件所需要的格式.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static String to_audio_string(this PieceIdentifier id)
        {
            switch (id)
            {
                default:
                    throw new InvalidEnumArgumentException();
                case PieceIdentifier.FRONT:
                case PieceIdentifier.MIDDLE:
                case PieceIdentifier.REAR:
                    return id.ToString().Substring(0, 2).ToLower();
                case PieceIdentifier.ONE:
                case PieceIdentifier.TWO:
                case PieceIdentifier.THREE:
                case PieceIdentifier.FOUR:
                case PieceIdentifier.FIVE:
                    return ((Int32)id).ToString();
                case PieceIdentifier.NONE:
                    return "";
            }
        }

        #endregion

        #region ' CoordinateDelta '

        public static String to_chinese_format(
            this CoordinateDelta delta, ChessColour player)
        {
            Int32 value = Math.Abs(delta.y);
            if (player == ChessColour.RED)
                return "一二三四五六七八九".Substring(value - 1, 1);
            return "１２３４５６７８９".Substring(value - 1, 1);
        }

        #endregion

        #region ' MoveDirection '

        public static String to_audio_string(this MoveDirection direction)
        {
            switch (direction)
            {
                default:
                    throw new InvalidEnumArgumentException();
                case MoveDirection.FORWARD:
                    return "ad";
                case MoveDirection.SIDEWARD:
                    return "tr";
                case MoveDirection.BACKWARD:
                    return "wi";
            }
        }

        #endregion

        #region ' ChessColour '

        /// <summary>
        /// 返回相反的颜色.
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static ChessColour flip(this ChessColour colour)
        {
            return ChessColour.NONE ^ colour;
        }

        /// <summary>
        /// 将颜色字符转化为<see cref="ChessColour"./>
        /// </summary>
        /// <param name="colour_char">颜色字符</param>
        /// <returns></returns>
        public static ChessColour to_chess_colour(this Char colour_char)
        {
            switch(colour_char)
            {
                default:
                    throw new ArgumentOutOfRangeException("颜色字符越界!");
                case 'r':
                    return ChessColour.RED;
                case 'b':
                    return ChessColour.BLACK;
            }
        }

        #endregion

        #region ' Elo '

        public static String to_elo_change_string(this Int32 elo_change)
        {
            String elo_change_string = elo_change.ToString("D2");
            if (elo_change >= 0)
                elo_change_string.Insert(0, "+");
            return elo_change_string;
        }

        #endregion
    }
}
