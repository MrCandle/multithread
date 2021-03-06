-- You can run the query here: http://sqlfiddle.com/#!18/cca37/19

create table DONOR (
    BG varchar(5) NOT NULL,
    AMOUNT int NOT NULL
)

insert into DONOR
values 
('A+', 1),
('A+', 4),
('A+', 9),
('A+', 12),
('A+', 2),
('O', 14),
('O', 29),
('O', 12),
('O', 13)

create table ACCEPTOR (
  BG varchar(5) NOT NULL,
  AMOUNT int NOT NULL
)

insert into ACCEPTOR
values 
('A+', 2),
('A+', 4),
('A+', 6),
('A+', 2),
('A+', 2),
('O', 21),
('O', 40),
('O', 19),
('O', 17)




SELECT BG, SUM(total) as requiredpints
FROM (
  SELECT BG, SUM(AMOUNT) as total
  FROM DONOR
  GROUP BY BG
  UNION ALL
  SELECT BG, SUM(-AMOUNT) as total
  FROM ACCEPTOR
  GROUP BY BG
) tmp
GROUP BY BG
HAVING SUM(total) > 0;
