using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleMonitoring.BL.Models
{
    public class ClientDto
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string ClientTelephone { get; set; }

        public DateTime Date { get; set; }
    }
}
