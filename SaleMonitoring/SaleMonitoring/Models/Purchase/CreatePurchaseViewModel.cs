using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleMonitoring.Models.Purchase
{
    public class CreatePurchaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты")]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ClientName { get; set; }

        [Required]
        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Неверно введен номер")]
        public string ClientTelephone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Неверный ввод цены")]
        [Range(0,100,ErrorMessage = "Выход за диопозон от 0 до 100")]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ManagerName { get; set; }

        public SelectList Managers;
    }
}