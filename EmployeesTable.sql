--CREATE TABLE `Employees` (
--	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
--	`SuperiorId`	INTEGER,
--	`FirstName`	TEXT NOT NULL,
--	`LastName`	TEXT NOT NULL,
--	`BaseSalary`	REAL NOT NULL,
--	`DateOfEmployment`	TEXT NOT NULL,
--	`PercentageIncrementForYear`	REAL NOT NULL,
--	`MaxPercentageIncrementForYear`	REAL NOT NULL,
--	`PercentageIncrementFromSubordinates`	REAL NOT NULL,
--	`Discriminator`	TEXT NOT NULL
--);

--DELETE FROM Employees;

INSERT INTO Employees
(Id, SuperiorId, FirstName, LastName, BaseSalary, DateOfEmployment, PercentageIncrementForYear, MaxPercentageIncrementForYear,
PercentageIncrementFromSubordinates, Discriminator)
VALUES
(1, NULL, 'Alan', 'Kay', 8000, '20.10.1970', 3, 30, 0, 'Salesman'),
(2, 1, 'Grady', 'Booch', 3000, '20.10.1980', 3, 30, 0, 'Employee'),
(3, 1, 'Anders', 'Hejlsberg ', 3000, '20.10.1990', 3, 30, 0, 'Employee'),
(4, 1, 'Jeffrey', 'Richter', 3000, '01.01.2000', 3, 30, 0, 'Employee'),
(5, 1, 'Tim', 'Berners-Lee', 4000, '20.10.1995', 5, 40, 0.5, 'Manager'),
(6, 5, 'Ada', 'Lovelace', 2000, '10.10.1975', 3, 30, 0, 'Employee'),
(7, 5, 'Larry', 'Page', 7000, '01.01.2000', 3, 30, 0.3, 'Salesman'),
(8, 7, 'Steve', 'Wozniak', 2000, '16.10.1990', 3, 30, 0, 'Employee'),
(9, 7, 'Bill', 'Gates', 2000, '16.10.1990', 3, 30, 0, 'Employee')