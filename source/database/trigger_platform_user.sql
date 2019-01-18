DELIMITER //

CREATE TRIGGER trigger_platform_user_check_insert BEFORE INSERT
ON platform_user FOR EACH ROW
BEGIN
    # CHECK email_address
    IF NOT NEW.email_address LIKE '%@%'
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the email address.';
    END IF;

    # CHECK gender
    IF NOT NEW.gender REGEXP '[fm]'
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the gender.';
    END IF;
    
    # CHECK birthday
    IF NOT NEW.birthday <= CURDATE() + INTERVAL 1 DAY
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the birthday.';
    END IF;
END //

CREATE TRIGGER trigger_platform_user_check_update BEFORE UPDATE
ON platform_user FOR EACH ROW
BEGIN
    # CHECK email_address
    IF NOT NEW.email_address LIKE '%@%'
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the email address.';
    END IF;

    # CHECK gender
    IF NOT NEW.gender REGEXP '[fm]'
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the gender.';
    END IF;
    
    # CHECK birthday
    IF NOT NEW.birthday <= CURDATE() + INTERVAL 1 DAY
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the birthday.';
    END IF;
END //

DELIMITER ;
