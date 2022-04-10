using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HabitTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Pages
{
	public class DeleteModel : PageModel
    {
        private string ConnectionString = @"Data Source=habit-Tracker.db";

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);

            return Page();
        }

        private DrinkingWaterModel GetById(int id)
        {
            var drinkingWaterRecord = new DrinkingWaterModel();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_water WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        drinkingWaterRecord.Id = reader.GetInt32(0);
                        drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                        drinkingWaterRecord.Quantity = reader.GetInt32(2);
                    }
                    return drinkingWaterRecord;
                }
                else
                {
                    return null;
                }
            }
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE from drinking_water WHERE Id = {id}";

                int rowCount = tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
