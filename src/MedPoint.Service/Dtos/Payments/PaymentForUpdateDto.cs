using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Payments
{
    public class PaymentForUpdateDto
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
