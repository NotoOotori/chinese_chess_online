DELIMITER //

CREATE PROCEDURE procedure_sign_up
(
    IN _email_address VARCHAR(254),
    IN _username VARCHAR(16),
    IN _unencrypted_password VARCHAR(256),
    IN _avatar MEDIUMBLOB,
    IN _gender CHAR(1),
    IN _birthday DATE
)
BEGIN
    SET @_salt_value = RANDOM_BYTES(64);
    SET @_encrypted_password = SHA2(CONCAT(
        @_salt_value, _unencrypted_password), 256);
    INSERT INTO platform_user VALUE
    (
        _email_address, _username, @_encrypted_password,
        @_salt_value, NULL, _avatar, _gender, _birthday
    );
END //

DELIMITER ;
