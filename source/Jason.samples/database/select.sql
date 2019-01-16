-- 列出不及格记录的学生名单.
SELECT snum FROM sc
WHERE score < 60;

-- 列出选修了计算机系课程的学生姓名和年龄.
SELECT DISTINCT student.sname, student.dept FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND course.dept = '计算机系';

-- 检索选修了数据库技术课程的学生姓名和系别.
SELECT DISTINCT student.sname, student.dept FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND course.cname = '数据库技术';

-- 检索选修了所有课程的学生名单.
SELECT student.* FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum
GROUP BY student.snum HAVING COUNT(*) = (SELECT COUNT(*) FROM course);

-- 检索每门课程成绩都在80分以上(含80分)的学生名单.
SELECT student.* FROM student, sc
WHERE student.snum = sc.snum
GROUP BY student.snum HAVING COUNT(*) = SUM(CASE WHEN sc.score >= 80 THEN 1 ELSE 0 END);

-- 检索获奖学金的学生名单: 每门课程在80分以上(含80分), 平均成绩在90分以上(含90分).
SELECT student.* FROM student, sc
WHERE student.snum = sc.snum
GROUP BY student.snum HAVING COUNT(*) = SUM(CASE WHEN sc.score >= 80 THEN 1 ELSE 0 END) AND AVG(sc.score) >= 90;

-- 检索选修了大学英语的学生名单和成绩, 并按成绩从高到低排列.
SELECT student.snum, student.sname, sc.score FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND course.cname = '大学英语'
ORDER BY sc.score DESC;

-- 统计每门课程的选修人数, 输出列名为课程号, 人数.
SELECT course.cnum, COUNT(student.snum) FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum
GROUP BY course.cnum;

-- 查询选修了数据库技术, 没有选修高等数学的学生姓名和系别.
SELECT student.sname, student.dept FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND course.cname = '数据库技术' AND student.snum NOT IN
(
	SELECT DISTINCT student.snum FROM student, course, sections, sc
	WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND course.cname = '高等数学'
);

-- 检索使用高等教育出版社出版的教材的课程名.
SELECT course.cname FROM course
WHERE course.textbook LIKE '%高等教育出版社%';

-- 统计所有课程的最高成绩, 最低成绩和平均成绩.
SELECT course.cnum, course.cname, MAX(sc.score) AS '最高成绩', MIN(sc.score) AS '最低成绩', AVG(sc.score) AS '平均成绩' FROM course, sections, sc
WHERE course.cnum = sections.cnum AND sections.secnum = sc.secnum
GROUP BY course.cnum;

-- 统计每门课程的选课人数及不及格人数.
SELECT course.cnum, course.cname, COUNT(student.snum) AS '选课人数', SUM(CASE WHEN sc.score < 60 THEN 1 ELSE 0 END) AS '不及格人数' FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum
GROUP BY course.cnum;

-- 查询土木工程系, 交通工程系和城市规划系的学生学号和姓名.
SELECT student.snum, student.sname FROM student
WHERE student.dept IN ('土木工程', '交通工程', '城市规划');

-- 查询选修了数据库技术或选修了多媒体技术的学生学号.
SELECT student.snum FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND course.cname = '数据库技术'
UNION
SELECT student.snum FROM student, course, sections, sc
WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum AND course.cname = '多媒体技术';

-- 查询计算机系年龄不大于19岁的学生信息.
SELECT student.* FROM student
WHERE student.dept = '计算机' AND student.snum NOT IN
(
	SELECT student.snum FROM student
    WHERE CURDATE() - student.birthday > 19
);

-- 查询计算机系年龄大于19岁的学生信息.
SELECT student.* FROM student
WHERE student.dept = '计算机' AND student.snum IN
(
	SELECT student.snum FROM student
    WHERE CURDATE() - student.birthday > 19
);

-- 查询学生的学号, 姓名, 选修课程门数, 平均成绩和不及格门数.
SELECT student.snum AS 学号, student.sname AS 姓名,
	COUNT(sc.secnum) AS 选修课程门数, FLOOR(AVG(sc.score)) AS 平均成绩,
	SUM(CASE WHEN sc.score < 60 THEN 1 ELSE 0 END) AS 不及格门数
    FROM student, sc
WHERE student.snum = sc.snum
GROUP BY student.snum;
