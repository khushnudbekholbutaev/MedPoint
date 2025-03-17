using MedPoint.Domain.Commons;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Domain.Entities.Users
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Salt { get; set; }
        public string Address { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Customer;
        public ICollection<Order> Orders { get; set; }
    }
}
