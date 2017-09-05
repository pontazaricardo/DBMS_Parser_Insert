# DBMS_Parser_Insert

This is a DBMS project that creates DataTable objects based on standard SQL CREATE and INSERT queries.

![demo00](/images/demo02.gif)

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

### Note

Inside the project, there is a **QueriesExample.txt**. If you want to modify the **Load sample queries** button, just replace the queries in this file for your own ones.

## Code

This project creates a custom *Table* object that uses a *DataTable* as a datastructure and has two additional functions: **CreateTable** and **Insert**.

### CreateTable

The code for this function is the following:
```c#
/// <summary>
/// Creates a Table object based on the columns names and types. Returns true if the creation was successful and false if there is an error.
/// </summary>
/// <param name="listOfNames"></param>
/// <param name="listOfTypes"></param>
/// <returns></returns>
public bool CreateTable(string[] listOfNames, Type[] listOfTypes)
{
	table = new DataTable();
	if ((listOfNames == null) || (listOfTypes == null))
	{
		return false;
	}

	if(listOfNames.Length != listOfTypes.Length)
	{
		return false;
	}

	for(int i = 0; i < listOfNames.Length; i++)
	{
		try
		{
			table.Columns.Add(listOfNames[i], listOfTypes[i]);
		}
		catch(Exception e)
		{
			Console.WriteLine(e.Message);

			table = new DataTable();
			return false;
		}
	}

	return true;
}
```
This function creates an instance of a *DataTable*, and creates, given the list of column names and types, the table, returning a **True** if it is successful or **False** otherwise.

### Insert

The code for this function is the following:
```c#
/// <summary>
/// Inserts the data in the given columns. Returns true if the data was inserted successfully and false otherwise.
/// </summary>
/// <param name="values"></param>
/// <param name="columns"></param>
/// <returns></returns>
public bool Insert(List<string> values, List<string> columns = null)
{
	if((values == null) || (values.Count == 0))
	{
		return false;
	}
	if (columns!= null)
	{
		if(values.Count != columns.Count)
		{
			return false;
		}
	}
	else
	{
		//columns == null
		if(values.Count != table.Columns.Count)
		{
			return false;
		}
	}

	//At this point we insert a new row
	try
	{
		DataRow row = table.NewRow();

		if (columns == null)
		{
			for (int i = 0; i < table.Columns.Count; i++)
			{
				row[table.Columns[i].ColumnName] = values[i];
			}
		}
		else
		{
			for(int i = 0; i < columns.Count; i++)
			{
				row[columns[i]] = values[i];
			}
		}

		table.Rows.Add(row);
		table.AcceptChanges();

	}
	catch(Exception e)
	{
		return false;
	}

	return true;
}
```
