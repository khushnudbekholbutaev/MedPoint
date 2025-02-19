using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Enums;
using MedPoint.Service.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Users
{
    
    public class UserForResultDto
    {
        public int Id { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ICollection<OrderForResultDto> Orders { get; set; }
    }
}
