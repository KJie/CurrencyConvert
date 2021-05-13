using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using CurrencyConvert.DTOs;

namespace CurrencyConvert.Controllers
{
    [ApiController]
    [Route("api/currencyconvert")]
    public class CurrencyConvertController : Controller
    {
        private EFDbContext _context;
        public CurrencyConvertController(EFDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public TargetAmount ConvertCurrency([FromBody]CurrencyConvertDto currencyConvertRequest)
        {
            IEnumerable<decimal> currExchangeRates = from rate in _context.ExchangeRates
                                                    where rate.CurrencyCode == currencyConvertRequest.CurrCode
                                                    select rate.ExchangeRates;

            IEnumerable<string> symbols = from currencyProp in _context.CurrencyProps
                                          where currencyProp.CurrencyCode == currencyConvertRequest.TargetCode
                                          select currencyProp.Symbol;

            if (!currExchangeRates.Any() || !symbols.Any())
            {
                return null;
            }

            decimal currExchangeRate = currExchangeRates.First();

            IEnumerable<decimal> tarExchangeRates = from rate in _context.ExchangeRates
                                                    where rate.CurrencyCode == currencyConvertRequest.TargetCode
                                                   select rate.ExchangeRates;

            decimal tarAmount = (currencyConvertRequest.Amount / currExchangeRates.First()) * tarExchangeRates.First();
            return new TargetAmount
            {
                Result = symbols.First() + " " +  tarAmount.ToString()
            };
        }
    }
}
