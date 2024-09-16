create database demo
GO
use demo
GO
create table Persons
(
  Id Uniqueidentifier primary key DEFAULT newid(),
  FirstName varchar(250) null,
  LastName varchar(250) not null,
)
GO
insert Persons Values(Newid(), 'Bart', 'Vries')
insert Persons Values(Newid(), 'Bart%', 'Vries Sr.')
insert Persons Values(Newid(), 'Ruud', 'Boerboom')
insert Persons Values(Newid(), 'Herman', 'Huijgen')
insert Persons Values(Newid(), 'Peter', 'Mols')