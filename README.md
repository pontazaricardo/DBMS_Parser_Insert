# DBMS_Parser_Insert

This is a DBMS project that creates DataTable objects based on standard SQL CREATE and INSERT queries.

## Usage

Just run the project in Visual Studio. The main form will then appear.

![mainform](/images/pic00.png)

In this form you will see three main buttons:
1. **Load sample queries:** This button loads a set of sample queries that show how to use this DBMS.
2. **Execute queries:** This button executes whatever query is in the main window, and displays the result at the bottom.
3. **Clear main window:** This button clears all the instructions in the main window.

## SQL code example

You can use standard SQL *CREATE* and *INSERT* instructions in the main window, as
```sql
create table user1 (userId int, name varchar, userLocation varchar);
create table tweets (twid int, tweet varchar, utcDate varchar, city varchar, userId int);

INSERT INTO user1 VALUES (811883,'regisb','Paris France') ;
INSERT INTO user1 VALUES (8055532,'goodevilgenius','Rockville MD USA') ;
INSERT INTO user1 VALUES (8229592,'minoic','Spain') ;

INSERT INTO tweets VALUES (185437272,'Damn its incredibl','Aug  3 2007 10:50PM','Hsinchu' ,811883) ;
INSERT INTO tweets VALUES (203133822,'Im trying NOT to pr','Aug 13 2007  9:44AM','Hsinchu' ,811883) ;
INSERT INTO tweets VALUES (278146952,'wondering what big p','Sep 19 2007  2:58AM','Hsinchu' ,8055532) ;
```
or just click the **Load sample queries** button to load a list of queries already preloaded in the project.

![demo01](/images/gif_01.gif)