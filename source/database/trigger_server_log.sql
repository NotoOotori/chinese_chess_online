DELIMITER //

CREATE TRIGGER trigger_server_log_check_insert BEFORE INSERT
ON server_log FOR EACH ROW
BEGIN
	# CHECK logout time,
	IF NOT NEW.logout_time >= NEW.login_time
	THEN
		SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Please check the logout time.';
	END IF;

	# CHECK user_host,
    IF NOT
    (
		NEW.user_host >= '000.000.000.000' AND
		NEW.user_host <= '255.255.255.255'
	)
	THEN
		SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Please check the user host.';
	END IF;

    # CHECK user_port,
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

CREATE TRIGGER trigger_server_log_check_update BEFORE UPDATE
ON server_log FOR EACH ROW
BEGIN
	# CHECK logout time,
	IF NOT NEW.logout_time >= NEW.login_time
	THEN
		SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Please check the logout time.';
	END IF;

	# CHECK user_host,
    IF NOT
    (
		NEW.user_host >= '000.000.000.000' AND
		NEW.user_host <= '255.255.255.255'
	)
	THEN
		SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = 'Please check the user host.';
	END IF;

    # CHECK user_port,
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
