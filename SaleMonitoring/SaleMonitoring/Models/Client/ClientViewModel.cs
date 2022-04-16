using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleMonitoring.Models.Client
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите имя клиента")]
        [DataType(DataType.Text)]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите номер телефона")]
        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Неверно введен номер")]
        public string ClientTelephone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, ввыберите дату")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты")]
        public DateTime Date { get; set; }
    }
}