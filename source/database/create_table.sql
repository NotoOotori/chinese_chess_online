CREATE TABLE platform_server
(
    server_hostname CHAR(15),
    # CHECK (server_hostname >= '000.000.000.000' AND server_hostname <= '255.255.255.255')
    server_port SMALLINT UNSIGNED,
    PRIMARY KEY (server_hostname, server_port)
);

CREATE TABLE platform_user
(
    email_address VARCHAR(254) PRIMARY KEY,
    # CHECK (email_address LIKE '%@%')
    username VARCHAR(16) NOT NULL,
    encrypted_password BINARY(64) NOT NULL,
    salt_value BINARY(64) NOT NULL,
    gender CHAR(1),
    # CHECK (gender LIKE '[fm]')
    birthday DATE
    # CHECK (birthday <= CURDATE())
    # state: store whether is blocked?
);

CREATE TABLE user_login_record
(
    login_id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    email_address VARCHAR(254),
    server_hostname CHAR(15),
    server_port SMALLINT UNSIGNED,
    user_hostname CHAR(15),
    # CHECK (user_hostname BETWEEN '000.000.000.000' AND '255.255.255.255')
    user_port SMALLINT UNSIGNED,
    login_time DATETIME NOT NULL DEFAULT NOW(),
    is_successful BOOL,
    FOREIGN KEY (email_address)
        REFERENCES platform_user (email_address)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    FOREIGN KEY (server_hostname, server_port)
        REFERENCES platform_server (server_hostname, server_port)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);

CREATE TABLE user_logout_record
(
    login_id INT UNSIGNED PRIMARY KEY,
    logout_time DATETIME NOT NULL DEFAULT NOW(),
    # CHECK logout_time >= login_time
    FOREIGN KEY (login_id)
        REFERENCES user_login_record (login_id)
        ON DELETE RESTRICT
        ON UPDATE RESTRICT
);
