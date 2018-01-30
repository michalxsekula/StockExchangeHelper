using System;
using System.ComponentModel.DataAnnotations;

namespace StockExchangeHelper.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string Currency { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z]{3}", ErrorMessage = "Required 3 letter code of currency.")]
        public string Code { get; set; }

        public double AverageRate { get; set; }
        public double StandardDeviation { get; set; }

        [Required]
        [DateLessThan("EndDate")]
        [Display(Name = "Start Date Time")]
        public DateTime StartDate { get; set; } = DateTime.Today - TimeSpan.FromDays(7);

        [Required]
        [Display(Name = "End Date Time")]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public DateTime SaveDate { get; set; }

        public override string ToString()
        {
            return
                $"{Currency} - {Code}, Average : {AverageRate}, StandardDeviation : {StandardDeviation}, from : {StartDate.ToShortDateString()} to : {EndDate.ToShortDateString()}, Saved at : {SaveDate}";
        }
    }
}