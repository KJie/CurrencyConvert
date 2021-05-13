using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConvert.DTOs
{
    public class CurrencyConvertDto
    {
        public string CurrCode { get; set; }
        public decimal Amount { get; set; }
        public string TargetCode { get; set; }
    }
}
