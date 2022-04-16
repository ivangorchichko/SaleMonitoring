using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleMonitoring.Models.Manager
{
    public class ManagerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите имя менеджера")]
        [DataType(DataType.Text)]
        public string ManagerName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите телефон менеджера")]
        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Неверно введен номер")]
        public string ManagerTelephone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите ранг менеджера")]
        [DataType(DataType.Text)]
        public string ManagerRank { get; set; }

        [Required(ErrorMessage = "Пожалуйста, ввыберите дату")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты")]
        public DateTime Date { get; set; }

    }
}