using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        public DateTime Date { get; set; }

        public int Quantity { get; set; }
    }
}

