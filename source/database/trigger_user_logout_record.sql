DELIMITER //

CREATE TRIGGER trigger_user_logout_record_check_insert BEFORE INSERT
ON user_logout_record FOR EACH ROW
BEGIN
    # CHECK whether login is successful
    IF
    (
        SELECT status_code FROM user_login_record
        WHERE login_id = NEW.login_id
    ) 
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the login id.';
    END IF;

    # CHECK logout time
    SET @_login_time =
    (
        SELECT login_time FROM user_login_record
        WHERE login_id = NEW.login_id
    );
    IF NOT
    (
        NEW.logout_time >= @_login_time AND
        NEW.logout_time <= NOW()
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the logout time.';
    END IF;
END //

CREATE TRIGGER trigger_user_logout_record_check_update BEFORE UPDATE
ON user_logout_record FOR EACH ROW
BEGIN
    # CHECK whether login is successful
    IF
    (
        SELECT status_code FROM user_login_record
        WHERE login_id = NEW.login_id
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the login id.';
    END IF;

    # CHECK logout time
    SET @_login_time =
    (
        SELECT login_time FROM user_login_record
        WHERE login_id = NEW.login_id
    );
    IF NOT
    (
        NEW.logout_time >= @_login_time AND
        NEW.logout_time <= NOW()
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the logout time.';
    END IF;
END //

DELIMITER ;
