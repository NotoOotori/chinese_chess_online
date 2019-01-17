# sample/chinese_chess

## 介绍

这是非常粗制滥造的样例版中国象棋对战.

## 运行说明

请先运行sample/server/server_chess.py,
再在同一局域网内以在命令行内输入`python chinese_chess.py -s battle`的方式打开两个对战程序,
然后就开始玩耍吧

## 已知bug

1. 在同一个对战程序中用户既可以走红棋又可以走黑棋
2. mask的显示bug
3. 不支持分房间对战, 意思就是不管多少个对战程序被打开, 下的都是同一盘棋,
   惊不惊喜意不意外!
