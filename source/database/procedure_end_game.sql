DELIMITER //

CREATE PROCEDURE procedure_end_game
(
    IN _red_email_address VARCHAR(254),
    IN _black_email_address VARCHAR(254),
    IN _game_string VARCHAR(500),
    IN _result TINYINT,
    OUT _red_elo_change SMALLINT
)
BEGIN
    INSERT INTO game_record VALUE
    (
        0, _red_email_address, _black_email_address,
        NULL, _game_string, _result
    );

    SELECT elo INTO @_red_elo
    FROM platform_user
    WHERE email_address = _red_email_address;

    SELECT elo INTO @_black_elo
    FROM platform_user
    WHERE email_address = _black_email_address;

    SET @_expect_red_score = 1/(1 + POW(10, (@_red_elo - @_black_elo)/400));
    SET _red_elo_change = 32 * (_result/2 - @_expect_red_score);
    
    UPDATE platform_user
    SET elo = @_red_elo + _red_elo_change
    WHERE email_address = _red_email_address;
    
    UPDATE platform_user
    SET elo = @_black_elo - _red_elo_change
    WHERE email_address = _black_email_address;
END //

DELIMITER ;
