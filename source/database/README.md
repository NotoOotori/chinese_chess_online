# database

## 关系模式

* platform\_server (<ins>**server\_hostname**,
    **server\_port**</ins>)
* platform\_user (<ins>**email\_address**</ins>,
    username, encrypted\_password, salt\_value,
    gender, birthday)
* user\_login\_record (<ins>**login\_id**</ins>,
    <ins>email\_address</ins>, <ins>server\_hostname,
    server\_port</ins>, user\_hostname,
    user\_port, login\_time, status\_code)
* user\_logout\_record (<ins>**login\_id**</ins>,
    logout\_time)
* game\_record (<ins>**game\_id**</ins>,
    <ins>red\_email\_address</ins>, <ins>black\_email\_address</ins>,
    start\_time, game\_string, result)

## 存储过程

* procedure\_sign\_up
  * 参数:
      > | `INOUT` | Parameter Name          | Data Type      | Note             |
      > | ------- | ----------------------- | -------------- | ---------------- |
      > | `IN`    | \_email\_address        | `VARCHAR(254)` |                  |
      > | `IN`    | \_username              | `VARCHAR(16)`  |                  |
      > | `IN`    | \_unencrypted\_password | `VARCHAR(256)` |                  |
      > | `IN`    | \_gender                | `CHAR(1)`      | `'[fm]'`, `NULL` |
      > | `IN`    | \_birthday              | `DATE`         | `NULL`           |
  * 效果:
      如果没有报错则注册成功, 如果报错则注册失败, 原因见报错信息.
* procedure\_log\_in
  * 参数:
      > | `INOUT` | Parameter Name          | Data Type           | Note      |
      > | ------- | ----------------------- | ------------------- | --------- |
      > | `IN`    | \_email\_address        | `VARCHAR(254)`      |           |
      > | `IN`    | \_unencrypted\_password | `VARCHAR(256)`      |           |
      > | `IN`    | \_server\_hostname      | `CHAR(15)`          |           |
      > | `IN`    | \_server\_port          | `SMALLINT UNSIGNED` |           |
      > | `IN`    | \_user\_hostname        | `CHAR(15)`          |           |
      > | `IN`    | \_user\_port            | `SMALLINT UNSIGNED` |           |
      > | `OUT`   | \_status\_code          | `TINYINT UNSIGNED`  | {0, 1, 2} |
      > | `OUT`   | \_login\_id             | `INT UNSIGNED`      |           |
  * 效果:
      如果登录成功, 则\_status\_code为0, \_login\_id为此次登录的id;
      如果邮箱不存在, 则\_status\_code为1, \_login\_id为0;
      如果邮箱或密码错误, 则\_status\_code为2, \_login\_id为0;
      如果执行存储过程时发生错误, 则报错.
* procedure\_log\_out
  * 参数:
      > | `INOUT` | Parameter Name          | Data Type           | Note      |
      > | ------- | ----------------------- | ------------------- | --------- |
      > | `IN`    | \_email\_address        | `VARCHAR(254)`      |           |
      > | `IN`    | \_login\_id             | `INT UNSIGNED`      |           |
  * 效果:
      添加登出记录, 如果邮箱和login_id不匹配, 则报错.
