using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.CartItems
{
    public class CartItem : Auditable
    {
        public int Count { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Medication Medication { get; set; }
        public int MedicationId { get; set; }
    }
}
