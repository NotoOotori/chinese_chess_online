DELIMITER //

CREATE TRIGGER trigger_user_login_record_check_insert BEFORE INSERT
ON user_login_record FOR EACH ROW
BEGIN
    # CHECK login time
    IF NOT NEW.login_time <= NOW()
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the login time.';
    END IF;

    # CHECK user_hostname
    IF NOT
        INET_ATON(NEW.user_hostname) BETWEEN
            INET_ATON('000.000.000.000') AND
            INET_ATON('255.255.255.255')
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the user hostname.';
    END IF;
END //

CREATE TRIGGER trigger_user_login_record_check_update BEFORE UPDATE
ON user_login_record FOR EACH ROW
BEGIN
    # CHECK login time
    IF NOT NEW.login_time <= NOW()
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the login time.';
    END IF;

    # CHECK user_hostname
    IF NOT
        INET_ATON(NEW.user_hostname) BETWEEN
            INET_ATON('000.000.000.000') AND
            INET_ATON('255.255.255.255')
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the user hostname.';
    END IF;
END //

DELIMITER ;
