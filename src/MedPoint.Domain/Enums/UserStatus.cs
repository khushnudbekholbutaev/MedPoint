using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedPoint.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter<UserStatus>))]
    public enum UserStatus
    {
        Admin = 1,
        Customer,
    }
}
