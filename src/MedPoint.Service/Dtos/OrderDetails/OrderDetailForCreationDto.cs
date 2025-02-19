using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.OrderDetails
{
    public class OrderDetailForCreationDto
    {
        public decimal Discount { get; set; }
        public int MedicationId { get; set; } 
        public int OrderId { get; set; }  
    }
}
