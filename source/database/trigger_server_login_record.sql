DELIMITER //

CREATE TRIGGER trigger_server_login_record_check_insert BEFORE INSERT
ON server_login_record FOR EACH ROW
BEGIN
    # CHECK login time
    IF NOT NEW.login_time <= NOW()
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the login time.';
    END IF;

    # CHECK user_hostname
    IF NOT
    (
        NEW.user_hostname >= '000.000.000.000' AND
        NEW.user_hostname <= '255.255.255.255'
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the user hostname.';
    END IF;

    # CHECK user_port
    IF NOT
    (
        NEW.user_port >= 0 AND
        NEW.user_port <= 65535
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the user port.';
    END IF;
END //

CREATE TRIGGER trigger_server_login_record_check_update BEFORE UPDATE
ON server_login_record FOR EACH ROW
BEGIN
    # CHECK login time
    IF NOT NEW.login_time <= NOW()
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the login time.';
    END IF;

    # CHECK user_hostname
    IF NOT
    (
        NEW.user_hostname >= '000.000.000.000' AND
        NEW.user_hostname <= '255.255.255.255'
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the user hostname.';
    END IF;

    # CHECK user_port
    IF NOT
    (
        NEW.user_port >= 0 AND
        NEW.user_port <= 65535
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the user port.';
    END IF;
END //

DELIMITER ;
