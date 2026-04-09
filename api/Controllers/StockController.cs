using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Stock> stock = _context.Stocks.ToList();
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Stock? stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stock);
            }

        }

    }
}