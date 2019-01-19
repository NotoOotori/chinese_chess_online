# database

## 关系模式

* platform_server (<ins>**server_hostname**,
    **server_port**</ins>)
* platform_user (<ins>**email_address**</ins>,
    username, encrypted_password, salt_value,
    gender, birthday)
* user_login_record (<ins>**login_id**</ins>,
    <ins>email_address</ins>, <ins>server_hostname,
    server_port</ins>, user_hostname,
    user_port, login_time, status_code)
* user_logout_record (<ins>**login_id**</ins>,
    logout_time)

## 存储过程

* procedure_sign_up
  * 参数:
      | `INOUT` | Parameter Name        | Data Type      | Note     |
      | ------- | --------------------- | -------------- | -------- |
      | `IN`    | _email_address        | `VARCHAR(254)` |          |
      | `IN`    | _username             | `VARCHAR(16)`  |          |
      | `IN`    | _unencrypted_password | `VARCHAR(256)` |          |
      | `IN`    | _gender               | `CHAR(1)`      | `'[fm]'` |
      | `IN`    | _birthday             | `DATE`         |          |
  * 效果:
      如果没有报错则注册成功, 如果报错则注册失败, 原因见报错信息.
* procedure_log_in
  * 参数:
      | `INOUT` | Parameter Name        | Data Type         | Note   |
      | ------- | --------------------- | ----------------- | ------ |
      | IN      | _email_address        | VARCHAR(254)      |        |
      | IN      | _unencrypted_password | VARCHAR(256)      |        |
      | IN      | _server_hostname      | CHAR(15)          |        |
      | IN      | _server_port          | SMALLINT UNSIGNED |        |
      | IN      | _user_hostname        | CHAR(15)          |        |
      | IN      | _user_port            | SMALLINT UNSIGNED |        |
      | OUT     | _status_code          | TINYINT UNSIGNED  | {0, 1} |
      | OUT     | _login_id             | INT UNSIGNED      |        |
  * 效果:
      如果登录成功, 则_status_code为0, _login_id为此次登录的id;
      如果密码错误, 则_status_code为1, _login_id为0;
      如果执行存储过程时发生错误, 则报错.
