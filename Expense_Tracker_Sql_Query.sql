use DemoDB

create table Expense_Tracker(
	sl_no int identity primary key,
	Title varchar(20),
	Description varchar(50),
	Amount decimal,
    Date date
)

--DROP TABLE Expense_Tracker
select * from Expense_Tracker