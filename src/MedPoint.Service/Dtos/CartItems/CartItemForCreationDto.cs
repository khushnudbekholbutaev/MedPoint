using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.CartItems
{
    public class CartItemForCreationDto
    {
        public int UserId { get; set; }
        public int MedicationId { get; set;}
    }
}
