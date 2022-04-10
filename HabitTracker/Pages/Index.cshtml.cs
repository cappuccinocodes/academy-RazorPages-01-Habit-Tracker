using System.ComponentModel.DataAnnotations;
using System.Globalization;
using HabitTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Pages;

public class IndexModel : PageModel
{
    private string ConnectionString = @"Data Source=habit-Tracker.db";
    public List<DrinkingWaterModel> Records { get; set; }

    public void OnGet()
    {
        CreateDatabase();
        GetAllRecords();
        Records = GetAllRecords();
    }

    void CreateDatabase()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {

            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS drinking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                        )";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }

    }

    List<DrinkingWaterModel> GetAllRecords()
    {
        Console.Clear();
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM drinking_water ";

            var tableData = new List<DrinkingWaterModel>();
            SqliteDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tableData.Add(
                    new DrinkingWaterModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Quantity = reader.GetInt32(2)
                    }); ;
                }
            }
            else
            {
                Console.WriteLine("No rows found");
            }

            return tableData;
        }
    }
}


