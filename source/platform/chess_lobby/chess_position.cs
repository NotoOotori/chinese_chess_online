using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace platform.chess_lobby
{
    /// <summary>
    /// 记录棋局状态的类
    /// </summary>
    public class ChessPosition : Dictionary<Coordinate, Piece>
    {
        #region ' Constructors '

        /// <summary>
        /// 初始化<see cref="ChessPosition"/>的新实例
        /// </summary>
        /// <param name="fen">棋局的FEN串</param>
        public ChessPosition(String fen = FEN.init)
        {
            String[] strings = fen.Split(' ');
            if (strings.Length != 6)
                throw new ArgumentOutOfRangeException("FEN值长度错误!");
            if (!Regex.IsMatch(strings[1], "[br]"))
                throw new ArgumentOutOfRangeException("走子方字符长度有误!");
            this._current_player = MyConvert.to_chess_colour(strings[1][0]);
            this.noncapture_moves = Int32.Parse(strings[4]);
            this.total_rounds = Int32.Parse(strings[5]);
            String[] rows = strings[0].Split('/');

            Int32 y = 9;
            foreach (String row in rows)
            {
                Int32 x = 0;
                foreach (Char c in row)
                {
                    if (y < 0)
                        throw new ArgumentOutOfRangeException("纵坐标越界!");
                    if (Char.IsDigit(c))
                    {
                        for (Int32 i = 0; i < Char.GetNumericValue(c); i++)
                        {
                            if (x > 8)
                                throw new ArgumentOutOfRangeException(
                                    "横坐标越界!");
                            this.Add(new Coordinate(x++, y), new Piece());
                        }
                        continue;
                    }
                    if (Char.IsLetter(c))
                    {
                        if (x > 8)
                            throw new ArgumentOutOfRangeException("横坐标越界!");
                        this.Add(new Coordinate(x++, y), new Piece(c));
                        continue;
                    }
                    throw new ArgumentOutOfRangeException("字符错误!");
                }
                y--;
            }
        }

        /// <summary>
        /// 初始化<see cref="ChessPosition"/>的新实例
        /// </summary>
        /// <param name="chess_position"></param>
        public ChessPosition(ChessPosition chess_position)
            : this(chess_position.ToString())
        {
            ;
        }

        #endregion

        #region ' Properties '

        /// <summary>
        /// 最近一次吃子或者进兵后棋局进行的步数(半回合数)
        /// </summary>
        private Int32 noncapture_moves { get; set; }
        /// <summary>
        /// 棋局的回合数
        /// </summary>
        private Int32 total_rounds { get; set; }
        /// <summary>
        /// 轮到走子的那一方
        /// </summary>
        private ChessColour _current_player { get; set; }
        /// <summary>
        /// 轮到走子的那一方
        /// </summary>
        public ChessColour current_player { get { return this._current_player; } }
        /// <summary>
        /// 未轮到走子的那一方.
        /// </summary>
        public ChessColour idle_player { get { return current_player.flip(); } }
        /// <summary>
        /// 棋局的fen串
        /// </summary>
        public String fen { get { return this.ToString(); } }

        #endregion

        #region ' Methods '

        #region ' Private '

        private static Boolean is_inside_castle(Coordinate cdn, ChessColour player)
        {
            if (cdn.x.value < 3 || cdn.x.value > 5)
                return false;
            switch (player)
            {
                default:
                    throw new ArgumentOutOfRangeException("选手颜色越界!");
                case ChessColour.BLACK:
                    if (cdn.y.value < 7)
                        return false;
                    return true;
                case ChessColour.RED:
                    if (cdn.y.value > 2)
                        return false;
                    return true;
            }
        }

        private static Boolean is_castle_side(Coordinate cdn, ChessColour player)
        {
            switch (player)
            {
                default:
                    throw new ArgumentOutOfRangeException("选手颜色越界!");
                case ChessColour.BLACK:
                    if (cdn.y.value < 5)
                        return false;
                    return true;
                case ChessColour.RED:
                    if (cdn.y.value > 4)
                        return false;
                    return true;
            }
        }

        private Boolean check_bishop_eye(Coordinate start, CoordinateDelta delta)
        {
            Coordinate eye = start + delta / 2;
            if (this[eye].type == PieceType.NONE)
                return true;
            return false;
        }

        private Boolean check_knight_leg(Coordinate start, CoordinateDelta delta)
        {
            Coordinate leg = start + delta / 2;
            if (this[leg].type == PieceType.NONE)
                return true;
            return false;
        }

        private Int32 count_piece(Coordinate start, CoordinateDelta delta)
        {
            Int32 x = 0;
            Int32 y = 0;
            Int32 count = 0;
            if (delta.x == 0)
            {
                for
                (
                    y = Math.Sign(delta.y);
                    Math.Abs(y) < Math.Abs(delta.y);
                    y += Math.Sign(delta.y)
                )
                    if (this[start + new CoordinateDelta(x, y)].type != PieceType.NONE)
                        count += 1;
                return count;
            }
            if (delta.y == 0)
            {
                for
                (
                    x = Math.Sign(delta.x);
                    Math.Abs(x) < Math.Abs(delta.x);
                    x += Math.Sign(delta.x)
                )
                    if (this[start + new CoordinateDelta(x, y)].type != PieceType.NONE)
                        count += 1;
                return count;
            }
            throw new ArgumentOutOfRangeException("不是直线移动!");
        }

        private static MoveDirection get_move_direction(
            CoordinateDelta delta, ChessColour player)
        {
            switch (player)
            {
                default:
                    throw new ArgumentOutOfRangeException("选手颜色越界!");
                case ChessColour.BLACK:
                    if (delta.y < 0)
                        return MoveDirection.FORWARD;
                    if (delta.y == 0)
                        return MoveDirection.SIDEWARD;
                    return MoveDirection.BACKWARD;
                case ChessColour.RED:
                    if (delta.y > 0)
                        return MoveDirection.FORWARD;
                    if (delta.y == 0)
                        return MoveDirection.SIDEWARD;
                    return MoveDirection.BACKWARD;
            }
        }

        private static VerticalLine get_vertical_line(
            Coordinate cdn, ChessColour player)
        {
            return new VerticalLine(cdn.x.value, player);
        }

        private PieceIdentifier get_identifier(Coordinate cdn)
        {
            return PieceIdentifier.NONE;
        }

        /// <summary>
        /// 判断棋步是否合法
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终止坐标</param>
        /// <returns></returns>
        private Boolean is_valid_move(Coordinate start, Coordinate end)
        {
            ChessColour player = this.current_player;
            PieceType piece = this[start].type;
            CoordinateDelta delta = end - start;

            // 筛选偏移量是否合法
            if (!CoordinateDelta.is_valid(delta, piece))
                return false;
            // 筛选忽略棋规情况下是否合法
            if (!this.is_raw_move(start, end))
                return false;
            // 棋规
            if (!this.check_rules(start, end))
                return false;
            return true;
        }

        /// <summary>
        /// 判断忽略棋规的情况下棋步是否合法
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终止坐标</param>
        private Boolean is_raw_move(Coordinate start, Coordinate end)
        {
            ChessColour player = this.current_player;
            PieceType piece = this[start].type;
            CoordinateDelta delta = end - start;

            switch (piece)
            {
                default:
                    throw new ArgumentOutOfRangeException("棋子种类越界!");
                case PieceType.ADVISOR:
                case PieceType.KING:
                    if (ChessPosition.is_inside_castle(end, player))
                        return true;
                    return false;
                case PieceType.BISHOP:
                    if (ChessPosition.is_castle_side(end, player))
                        if (this.check_bishop_eye(start, delta))
                            return true;
                    return false;
                case PieceType.CANNON:
                    if (this[end].type == PieceType.NONE)
                    {
                        if (this.count_piece(start, delta) == 0)
                            return true;
                        return false;
                    }
                    if (this.count_piece(start, delta) == 1)
                        return true;
                    return false;
                case PieceType.KNIGHT:
                    if (this.check_knight_leg(start, delta))
                        return true;
                    return false;
                case PieceType.PAWN:
                    if (ChessPosition.get_move_direction(delta, player) ==
                        MoveDirection.BACKWARD)
                        return false;
                    if (ChessPosition.is_castle_side(start, player))
                        if (!delta.abs().Equals(new CoordinateDelta(0, 1)))
                            return false;
                    return true;
                case PieceType.ROOK:
                    if (this.count_piece(start, delta) == 0)
                        return true;
                    return false;
            }
        }

        private Boolean check_rules(Coordinate start, Coordinate end)
        {
            // 不能送将
            if (this.is_check(start, end, current_player))
                return false;
            return true;
        }

        /// <summary>
        /// 判断棋步之后该<see cref="ChessColour"/>是否被将军
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终止坐标</param>
        /// <returns></returns>
        private Boolean is_check(
            Coordinate start, Coordinate end, ChessColour player)
        {
            return this.move(start, end).is_check(player);
        }

        /// <summary>
        /// 判断<see cref="ChessColour"/>是否正在被将军.
        /// </summary>
        /// <param name="player">玩家</param>
        /// <returns></returns>
        private Boolean is_check(ChessColour player)
        {
            return false;
        }

        #endregion

        /// <summary>
        /// 获得棋步的音频字符串
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终止坐标</param>
        /// <returns></returns>
        public String get_audio_string(Coordinate start, Coordinate end)
        {
            ChessColour player = this.current_player;
            PieceType piece = this[start].type;
            CoordinateDelta delta = end - start;
            VerticalLine start_line = ChessPosition.get_vertical_line(
                start, player);
            VerticalLine end_line = ChessPosition.get_vertical_line(
                end, player);
            MoveDirection direction = ChessPosition.get_move_direction(
                delta, player);
            PieceIdentifier id;

            switch (piece)
            {
                default:
                    throw new ArgumentOutOfRangeException("棋子种类越界!");
                case PieceType.ADVISOR:
                case PieceType.BISHOP:
                    return ChessMove.to_audio_name(
                        player, piece, start_line, direction, end_line);
                case PieceType.CANNON:
                case PieceType.ROOK:
                case PieceType.PAWN:
                    id = this.get_identifier(start);
                    if (direction == MoveDirection.SIDEWARD)
                        return ChessMove.to_audio_name(
                            id, player, piece, start_line, direction, end_line);
                    return ChessMove.to_audio_name(
                        id, player, piece, start_line, direction, delta);
                case PieceType.KING:
                    if (direction == MoveDirection.SIDEWARD)
                        return ChessMove.to_audio_name(
                            player, piece, start_line, direction, end_line);
                    return ChessMove.to_audio_name(
                        player, piece, start_line, direction, delta);
                case PieceType.KNIGHT:
                    id = this.get_identifier(start);
                    return ChessMove.to_audio_name(
                        id, player, piece, start_line, direction, end_line);
            }
        }

        /// <summary>
        /// 判断棋步是否合法
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终止坐标</param>
        /// <returns></returns>
        public MoveType get_move_type(Coordinate start, Coordinate end)
        {
            if(is_valid_move(start, end))
            {
                MoveType type = MoveType.NORMAL_MOVE;
                if (this[end].type != PieceType.NONE)
                    type = type | MoveType.CAPTURE;
                if (this.is_check(start, end, idle_player))
                    type = type | MoveType.CHECK;
                return type;
            }
            return MoveType.INVALID_MOVE;
        }

        /// <summary>
        /// 完成棋子的移动以及回合数的更新
        /// </summary>
        /// <param name="start">起始坐标</param>
        /// <param name="end">终止坐标</param>
        /// <returns></returns>
        public ChessPosition move(Coordinate start, Coordinate end)
        {
            ChessPosition new_position = new ChessPosition(this);

            #region ' Update the Stats '

            new_position._current_player = new_position._current_player.flip();
            if (this[end] == null)
                new_position.noncapture_moves += 1;
            else
                new_position.noncapture_moves = 0;
            if (this._current_player == ChessColour.BLACK)
                new_position.total_rounds += 1;

            #endregion

            #region ' Move the Piece ' 

            new_position[start] = new Piece();
            new_position[end] = new Piece(this[start].ToString());

            #endregion

            return new_position;
        }

        /// <summary>
        /// 返回棋局的FEN串
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            String fen_0 = "";
            for (Int32 y = 9; y >= 0; y--)
            {
                Int32 empty = 0;
                for (Int32 x = 0; x < 9; x++)
                {
                    Coordinate cdn = new Coordinate(x, y);
                    if (this[cdn].type == PieceType.NONE)
                    {
                        empty++;
                        continue;
                    }
                    if (empty != 0)
                    {
                        fen_0 += empty.ToString();
                        empty = 0;
                    }
                    fen_0 += this[cdn].ToChar().ToString();
                }
                if (empty != 0)
                    fen_0 += empty.ToString();
                if (y != 0)
                    fen_0 += "/";
            }
            return $"{fen_0} {Char.ToLower(this.current_player.ToString()[0])} " +
                $"- - {this.noncapture_moves} {this.total_rounds}";
        }

        #endregion
    }
    
    /// <summary>
    /// 初始局面FEN值
    /// </summary>
    public class FEN
    {
        public const String init  = "rnbakabnr/9/1c5c1/p1p1p1p1p/" +
            "9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR r - - 0 1";
        public const String empty = "9/9/9/9/9/9/9/9/9/9 r - - 0 1";
    }

    /// <summary>
    /// 进平退
    /// </summary>
    public enum MoveDirection
    {
        FORWARD = '+',
        BACKWARD = '-',
        SIDEWARD = '_'
    }

    public class ChessMove
    {
        /// <summary>
        /// 相三进五型
        /// </summary>
        /// <returns></returns>
        public static String to_audio_name
        (
            ChessColour player,
            PieceType piece,
            VerticalLine start,
            MoveDirection direction,
            VerticalLine end
        )
        {
            if (player == ChessColour.NONE || piece == PieceType.NONE)
                throw new ArgumentOutOfRangeException("越界!");
            return ($"{player.ToString()[0]}{(char)piece}{start.value}" +
                $"{direction.to_audio_string()}{end.value}").ToLower();
        }

        /// <summary>
        /// 前炮平五型或相三进五型
        /// </summary>
        /// <returns></returns>
        public static String to_audio_name
        (
            PieceIdentifier id,
            ChessColour player,
            PieceType piece,
            VerticalLine start,
            MoveDirection direction,
            VerticalLine end
        )
        {
            if (id == PieceIdentifier.NONE)
                return to_audio_name(
                    player, piece, start, direction, end);
            if (player == ChessColour.NONE || piece == PieceType.NONE)
                throw new ArgumentOutOfRangeException("越界!");
            return ($"{id.to_audio_string()}{player.ToString()[0]}{(char)piece}" +
                $"{direction.to_audio_string()}{end.value}").ToLower();
        }

        /// <summary>
        /// 帅五进一型
        /// </summary>
        /// <returns></returns>
        public static String to_audio_name
        (
            ChessColour player,
            PieceType piece,
            VerticalLine start,
            MoveDirection direction,
            CoordinateDelta delta
        )
        {
            if (player == ChessColour.NONE || piece == PieceType.NONE)
                throw new ArgumentOutOfRangeException("越界!");
            return ($"{player.ToString()[0]}{(char)piece}{start.value}" +
                $"{direction.to_audio_string()}{Math.Abs(delta.y)}")
                .ToLower();
        }

        /// <summary>
        /// 前炮进一型或炮五进一型
        /// </summary>
        /// <returns></returns>
        public static String to_audio_name
        (
            PieceIdentifier id,
            ChessColour player,
            PieceType piece,
            VerticalLine start,
            MoveDirection direction,
            CoordinateDelta delta
        )
        {
            if (id == PieceIdentifier.NONE)
                return to_audio_name(
                    player, piece, start, direction, delta);
            if (player == ChessColour.NONE || piece == PieceType.NONE)
                throw new ArgumentOutOfRangeException("越界!");
            return ($"{id.to_audio_string()}{player.ToString()[0]}{(char)piece}" +
                $"{direction.to_audio_string()}{Math.Abs(delta.y)}").ToLower();
        }
    }
}
