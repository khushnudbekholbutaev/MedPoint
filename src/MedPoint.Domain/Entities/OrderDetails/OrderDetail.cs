using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.CartItems;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Entities.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.OrderDetails
{
    public class OrderDetail : Auditable
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public Medication Medication { get; set; }
        public int MedicationId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }  // Foreign Key to Orders table
    }
}
