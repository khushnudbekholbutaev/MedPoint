using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Exceptions
{
    public class MedPointException(int code, string message) : Exception(message)
    {
        public int StatusCode { get; set; } = code;
    }
}
