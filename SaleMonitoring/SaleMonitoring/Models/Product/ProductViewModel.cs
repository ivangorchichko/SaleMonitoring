using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleMonitoring.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название продукта")]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите цену продукта")]
        [DataType(DataType.Currency, ErrorMessage = "Неверный ввод цены")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Пожалуйста, ввыберите дату")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты")]
        public DateTime Date { get; set; }
    }
}