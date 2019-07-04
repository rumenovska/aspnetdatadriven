using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DapperDemo
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=PALMYRA17;Initial Catalog=TodoApp;User ID=sa;Password=Password1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        static void Main(string[] args)
        {
             using (connection)
             connection.Open();

            //InsertTodo();
            foreach (var t in GetAllTodos())
            {
                Console.WriteLine($"#{t.Id}, {t.Description}, {   (t.IsCompleted ? "completed" : "not completed") }");
            }
            Console.WriteLine("Hello World!");
            Console.Read();
        }

        static List<Todo> GetAllTodos()
        {
            var todos = connection.Query<Todo>("select * from Todos").ToList();
            return todos;
            
        }

        static void InsertTodo(Todo todo)
        {
            string sql = $"INSERT INTO Todos (Title, Description, DueDate, IsCompleted) Values (@title,@description,@dueDate,@isCompleted)";

            var affectedRows= connection.Execute(sql,
              new[] {
              new {title= todo.Title, description= todo.Description, dueDate= todo.DueDate, isCompleted= todo.IsCompleted}
                 
              }
             );
            
        }
    }
}
