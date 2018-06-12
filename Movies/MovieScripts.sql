create database movie;
GO

use movie;
GO

IF NOT EXISTS(
Select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' and TABLE_NAME='producer'
)
BEGIN
Create table producer(
  producerId varchar(50) NOT NULL ,
  name VARCHAR(100) NOT NULL,
  sex CHAR(1) NOT NULL,
  dob DATETIME NOT NULL,
  bio NVARCHAR(max) NULL,
  PRIMARY KEY (producerId));
END
GO

IF NOT EXISTS(
Select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' and TABLE_NAME='actor'
)
BEGIN
Create table actor(
  actorId varchar(50) NOT NULL ,
  name VARCHAR(100) NOT NULL,
  sex CHAR(1) NOT NULL,
  dob DATETIME NOT NULL,
  bio NVARCHAR(max) NULL,
  PRIMARY KEY (actorId));
END
GO

IF NOT EXISTS(
Select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' and TABLE_NAME='movie'
)
BEGIN
Create table movie(
  movieId varchar(50) NOT NULL,
  name VARCHAR(100) NOT NULL,
  releaseYear INT NOT NULL,
  plot NVARCHAR(max) NULL,
  posterFileName varchar(20) NULL,
  producerId varchar(50) NOT NULL,
  PRIMARY KEY (movieId),
  Foreign key(producerId) references producer(producerId)
  );
END
GO

IF NOT EXISTS(
Select 1 from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='dbo' and TABLE_NAME='movieActorLink'
)
BEGIN
Create table movieactorlink(
   movieId varchar(50) NOT NULL,
  actorId varchar(50)  NOT NULL,
  remuneration INT NULL,
  PRIMARY KEY (movieId,actorId),
  Foreign key(movieId) references movie(movieId),
   Foreign key(actorId) references actor(actorId)
  );
END
GO


