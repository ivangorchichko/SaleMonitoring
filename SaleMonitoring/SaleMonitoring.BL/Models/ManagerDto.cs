using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleMonitoring.BL.Models
{
    public class ManagerDto
    {
        public int Id { get; set; }

        public string ManagerName { get; set; }

        public string ManagerTelephone { get; set; }

        public DateTime Date { get; set; }

        public string ManagerRank { get; set; }

    }
}
