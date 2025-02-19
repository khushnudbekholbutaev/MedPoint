using MedPoint.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Payments
{
    public class PaymentForCreationDto
    {
        public string PaymentMethod { get; set; }
        public int OrderId { get; set; }
    }
}
