using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.BL.Enums;

namespace SaleMonitoring.BL.Models
{
    public class PurchaseDto 
    {
        public int Id { get; set; }

        public ClientDto Client { get; set; }

        public ProductDto Product { get; set; }

        public ManagerDto Manager { get; set; }

        public DateTime Date { get; set; }
    }
}
