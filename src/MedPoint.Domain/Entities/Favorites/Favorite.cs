using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.Favorites
{
    public class Favorite : Auditable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }
    }
}
