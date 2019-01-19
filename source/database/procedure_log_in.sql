DELIMITER //

CREATE PROCEDURE procedure_log_in
(
    IN _email_address VARCHAR(254),
    IN _unencrypted_password VARCHAR(256),
    IN _server_hostname CHAR(15),
    IN _server_port SMALLINT UNSIGNED,
    IN _user_hostname CHAR(15),
    IN _user_port SMALLINT UNSIGNED,
    OUT _status_code TINYINT UNSIGNED,
    OUT _login_id INT UNSIGNED
)
BEGIN
    SELECT encrypted_password, salt_value
        INTO @_stored_password, @_salt_value
        FROM platform_user
        WHERE email_address = _email_address;
    SET @_encrypted_password = SHA2(CONCAT(
        @_salt_value, _unencrypted_password), 256);
    IF @_encrypted_password = @_stored_password
    THEN
        SET _status_code = 0;
    ELSE
        SET _status_code = 1;
    END IF;
    INSERT INTO user_login_record VALUE
    (
        0, _email_address, _server_hostname,
        _server_port, _user_hostname, _user_port,
        NULL, _status_code
    );
    SET _login_id = IF(_status_code = 0, LAST_INSERT_ID(), 0);
END //

DELIMITER ;
