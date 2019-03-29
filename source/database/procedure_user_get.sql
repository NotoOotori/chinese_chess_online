DELIMITER //

CREATE PROCEDURE procedure_get_username
(
    IN _email_address VARCHAR(254),
    OUT _username VARCHAR(16)
)
BEGIN
    SELECT username INTO _username
    FROM platform_user
    WHERE email_address = _email_address;
END //

CREATE PROCEDURE procedure_get_avatar
(
    IN _email_address VARCHAR(254),
    OUT _avatar MEDIUMBLOB
)
BEGIN
    SELECT avatar INTO _avatar
    FROM platform_user
    WHERE email_address = _email_address;
END //

DELIMITER ;