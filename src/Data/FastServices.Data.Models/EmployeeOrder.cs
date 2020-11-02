using HomeServices.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastServices.Data.Models
{
    public class EmployeeOrder
    {
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }
    }
}
