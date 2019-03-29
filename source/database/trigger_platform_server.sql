DELIMITER //

CREATE TRIGGER trigger_platform_server_check_insert BEFORE INSERT
ON platform_server FOR EACH ROW
BEGIN
    # CHECK server_hostname
    IF NOT
        INET_ATON(NEW.server_hostname) BETWEEN
            INET_ATON('000.000.000.000') AND
            INET_ATON('255.255.255.255')
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the server hostname.';
    END IF;
END //

CREATE TRIGGER trigger_platform_server_check_update BEFORE UPDATE
ON platform_server FOR EACH ROW
BEGIN
    # CHECK server_hostname
    IF NOT
        INET_ATON(NEW.server_hostname) BETWEEN
            INET_ATON('000.000.000.000') AND
            INET_ATON('255.255.255.255')
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the server hostname.';
    END IF;
END //

DELIMITER ;
