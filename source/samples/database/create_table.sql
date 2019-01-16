CREATE TABLE student
(
	snum CHAR(4) PRIMARY KEY COMMENT '学号' CHECK(snum LIKE 's[0-9][0-9][0-9]'),
	sname VARCHAR(20) COMMENT '姓名',
    sex CHAR(2) COMMENT '性别' CHECK(sex IN ('男', '女')),
    dept VARCHAR(30) COMMENT '系别',
    birthday DATE COMMENT '出生日期',
    telephone CHAR(12) COMMENT '联系电话' CHECK(telephone Like '[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)CHARACTER SET = utf8;

CREATE TABLE course
(
	cnum CHAR(4) PRIMARY KEY COMMENT '课程号' CHECK(cnum LIKE 'c[0-9][0-9][0-9]'),
    cname VARCHAR(30) COMMENT '课程名',
    credits SMALLINT(2) COMMENT '学分' CHECK(credits BETWEEN 0 AND 8),
    descr VARCHAR(40) COMMENT '课程说明',
    dept VARCHAR(30) COMMENT '开课系别',
    textbook VARCHAR(40) COMMENT '教材'
);

CREATE TABLE sections
(
	secnum CHAR(5) PRIMARY KEY COMMENT '班号' CHECK(secnum LIKE '[0-9][0-9][0-9][0-9][0-9]'),
    cnum CHAR(4) COMMENT '课程号',
    pnum CHAR(4) COMMENT '教师工号' CHECK(pnum LIKE 'p%'),
    FOREIGN KEY(cnum) REFERENCES course(cnum)
);

CREATE TABLE sc
(
	snum CHAR(4) COMMENT '学号',
    secnum CHAR(5) COMMENT '班号',
    score INT(4) COMMENT '分数' CHECK(score between 0 AND 100),
    PRIMARY KEY(snum, secnum),
    FOREIGN KEY(snum) REFERENCES student(snum),
    FOREIGN KEY(secnum) REFERENCES sections(secnum)
);
