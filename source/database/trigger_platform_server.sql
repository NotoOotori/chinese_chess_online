DELIMITER //

CREATE TRIGGER trigger_platform_server_check_insert BEFORE INSERT
ON platform_server FOR EACH ROW
BEGIN
    # CHECK server_hostname
    IF NOT
    (
        NEW.server_hostname >= '000.000.000.000' AND
        NEW.server_hostname <= '255.255.255.255'
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the server hostname.';
    END IF;

    # CHECK server_port
    IF NOT
    (
        NEW.server_port >= 0 AND
        NEW.server_port <= 65535
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the server port.';
    END IF;
END //

CREATE TRIGGER trigger_platform_server_check_update BEFORE UPDATE
ON platform_server FOR EACH ROW
BEGIN
    # CHECK server_hostname
    IF NOT
    (
        NEW.server_hostname >= '000.000.000.000' AND
        NEW.server_hostname <= '255.255.255.255'
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the server hostname.';
    END IF;

    # CHECK server_port
    IF NOT
    (
        NEW.server_port >= 0 AND
        NEW.server_port <= 65535
    )
    THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Please check the server port.';
    END IF;
END //

DELIMITER ;
