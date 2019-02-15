INSERT INTO platform_server
    VALUE ('45.32.82.133', 21567);
INSERT INTO platform_server
    VALUE ('127.0.0.1', 21567);

DELETE FROM platform_server
    WHERE hostname = '45.32.82.133';
DELETE FROM platform_server
    WHERE hostname = '127.0.0.1';

CALL procedure_sign_up('345238563@qq.com', 'noto_ootori', '123pzy', 'f', '1999-04-01');
CALL procedure_sign_up('jason_345238563@outlook.com', 'noto_ootori', '123pzy', 'f', '1999-04-01');

DELETE FROM platform_user
    WHERE username = 'noto_ootori';

CALL procedure_log_in('345238563@qq.com', '123pzy', '45.32.82.133', 21567, '111.187.30.131', 49137, @_status_code, @_login_id);
SELECT @_status_code, @_login_id;
CALL procedure_log_out('345238563@qq.com', 1);

DELETE FROM user_login_record WHERE email_address = '345238563@qq.com';
ALTER TABLE user_login_record AUTO_INCREMENT = 1;
DELETE FROM user_logout_record WHERE login_id > 0;
