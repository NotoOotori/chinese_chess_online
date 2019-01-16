CREATE VIEW ViewA
AS
	SELECT student.snum, course.cnum, course.cname, sc.score FROM student, sc, sections, course
	WHERE student.snum = sc.snum AND sc.secnum = sections.secnum AND sections.cnum = course.cnum;
    
CREATE PROCEDURE ProcC
(
	IN _snum CHAR(4),
	OUT _avg INT,
	OUT _ccnt INT,
	OUT _fcnt INT
)
	SELECT AVG(score), COUNT(cnum), SUM(CASE WHEN score < 60 THEN 1 ELSE 0 END) INTO _avg, _ccnt, _fcnt FROM ViewA
	WHERE snum = _snum
	GROUP BY snum;
