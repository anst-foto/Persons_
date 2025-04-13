using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using Persons;

const string connectionString = @"Data Source=D:\persons.db;";
var db = new SqliteConnection(connectionString);

db.Open();

var lastName = "Ivanov";
var firstName = "Ivan";
var sql = $"""
           INSERT INTO table_persons (last_name, first_name) 
           VALUES ('{lastName}', '{firstName}'),
                  ('Ivanov', 'Petr')
           """;
var command = new SqliteCommand()
{
    Connection = db,
    CommandText = sql
};
var result = command.ExecuteNonQuery();
if (result == 0) throw new Exception("Данные не добавлены!");

const string sql2 = "SELECT id, last_name, first_name FROM table_persons";
var command2 = new SqliteCommand()
{
    Connection = db,
    CommandText = sql2
};
var reader = command2.ExecuteReader();

if (!reader.HasRows) throw new Exception("Таблица пустая!");

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