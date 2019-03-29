DELIMITER //

CREATE TRIGGER trigger_game_record_check_insert BEFORE INSERT
ON game_record FOR EACH ROW
BEGIN
    # CHECK email address
    IF NEW.red_email_address = NEW.black_email_address
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the email addresses.';
    END IF;
    
    # CHECK result
    IF NOT NEW.result BETWEEN 0 AND 2
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the result.';
    END IF;
END //

CREATE TRIGGER trigger_game_record_check_update BEFORE UPDATE
ON game_record FOR EACH ROW
BEGIN
    # CHECK email address
    IF NEW.red_email_address = NEW.black_email_address
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the email addresses.';
    END IF;
    
    # CHECK result
    IF NOT NEW.result BETWEEN 0 AND 2
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the result.';
    END IF;
END //

DELIMITER ;
