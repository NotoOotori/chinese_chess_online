DELIMITER //

CREATE PROCEDURE procedure_log_out
(
    IN _email_address VARCHAR(254),
    IN _login_id INT UNSIGNED
)
BEGIN
    IF
    (
        SELECT COUNT(*) FROM user_login_record
        WHERE email_address = _email_address
        AND login_id = _login_id
    )
    THEN
        INSERT INTO user_logout_record VALUE
        (_login_id, NULL);
    ELSE
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Email address and login id don\'t match.';
    END IF;
END //

DELIMITER ;
