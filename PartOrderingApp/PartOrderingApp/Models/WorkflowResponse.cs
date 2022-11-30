using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class WorkflowResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }

        public decimal OrderTotal { get; set; } 
    }
}
