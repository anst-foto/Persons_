using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using Persons;

var db = new SqliteConnection(@"Data Source=D:\persons.db;");
db.Open();

var sql = "SELECT id, last_name, first_name FROM table_persons";
var command = new SqliteCommand(sql, db);
var reader = command.ExecuteReader();

if (!reader.HasRows) throw new Exception("Table is empty");

var persons = new List<Person>();
while (reader.Read())
{
    var person = new Person()
    {
        Id = reader.GetInt32("id"),
        LastName = reader.GetString("last_name"),
        FirstName = reader.GetString("first_name")
    };
    persons.Add(person);
}

db.Close();

foreach (var person in persons)
{
    Console.WriteLine($"{person.Id}: {person.LastName} {person.FirstName}");
}