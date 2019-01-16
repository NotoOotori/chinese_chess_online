CREATE TABLE platform_server
(
	server_host CHAR(15),
    # CHECK (server_host >= '000.000.000.000' AND server_host <= '255.255.255.255'),
    server_port SMALLINT(4),
    # CHECK (server_port >= 0 AND server_port <= 65535),
    PRIMARY KEY (server_host, server_port)
);

CREATE TABLE platform_user
(
	email_address VARCHAR(254) PRIMARY KEY,
    # CHECK (email_address LIKE '%@%'),
    username VARCHAR(16) NOT NULL,
    encrypted_password BLOB,
    gender CHAR(1),
    # CHECK (gender LIKE '[fm]'),
    birthday DATE
    # CHECK (birthday <= CURDATE()),
    # state: store whether is blocked?
);

CREATE TABLE server_log
(
	email_address VARCHAR(254),
    # CHECK (email_address LIKE '%@%'),
    server_host CHAR(15),
    # CHECK (server_host >= '000.000.000.000' AND server_host <= '255.255.255.255'),
    server_port SMALLINT(4),
    # CHECK (server_port >= 0 AND server_port <= 65535),
	login_time DATETIME,
    logout_time DATETIME,
    # login_time <= logout_time
    user_host CHAR(15),
    # CHECK (user_host >= '000.000.000.000' AND user_host <= '255.255.255.255'),
    user_port SMALLINT(4),
    # CHECK (user >= 0 AND user <= 65535),
    PRIMARY KEY (email_address, server_host, server_port, login_time),
	FOREIGN KEY (email_address)
		REFERENCES platform_user (email_address)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	FOREIGN KEY (server_host, server_port)
		REFERENCES platform_server (server_host, server_port)
        ON DELETE CASCADE
		ON UPDATE CASCADE
);
