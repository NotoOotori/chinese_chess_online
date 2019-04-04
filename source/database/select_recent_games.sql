SET @email_address = '345238563@qq.com';
SELECT
    record.result AS red_result,
    CASE
        WHEN self.email_address = red_email_address THEN TRUE
        ELSE FALSE
    END AS is_red,
    record.game_string,
    opponent.avatar AS opponent_avatar,
    opponent.username AS opponent_username,
    opponent.email_address AS opponemt_email_address
FROM platform_user AS opponent, platform_user AS self, game_record AS record
WHERE
    (
        (self.email_address = record.red_email_address AND opponent.email_address = record.black_email_address)
        OR
        (self.email_address = record.black_email_address AND opponent.email_address = record.red_email_address)
    )
    AND
        self.email_address = @email_address
ORDER BY record.game_id DESC;
