using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOcbc.Models;

namespace TestOcbc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly bankContext _context;

        public TransactionsController(bankContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction(long id)
        {
            var transaction = await _context.Transactions.Where<Transaction>(trs => trs.CustomersId == id).ToListAsync<Transaction>();

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(long id, Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            Transaction dataTran = new Transaction
            {
                CustomersId = transaction.CustomersId,
                Amount = transaction.Amount,
                Description = transaction.Description,
                TransDate = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), "dd-MM-yyyy HH:mm:ss", null),
                point = CountPoint(transaction.Amount, transaction.Description)
            };
            _context.Transactions.Add(dataTran);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Transaction>>> searchData(ByDate data)
        {
            var query = from wo in _context.Transactions select wo;

            if (data.startDate != null && data.endDate != null)
            {
                var startDate = DateTime.ParseExact(data.startDate, "dd-MM-yyyy", null);
                var endDate = DateTime.ParseExact(data.endDate, "dd-MM-yyyy", null);
                query = query.Where(q => q.TransDate >= startDate && q.TransDate <= endDate);
            }

            query = query.OrderByDescending(q => q.TransDate);
            var transaction = await query.ToListAsync<Transaction>();
            if (transaction == null)
            {
                return NotFound();
            }
            else
            {
                return transaction;
            }
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> DeleteTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        private bool TransactionExists(long id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }

        private int CountPoint(double amount, string tipe)
        {
            int gainPoin = 0;
            switch (tipe)
            {
                case "bp":
                    gainPoin = CountPointPulsa(amount);
                    break;

                case "bt":
                    gainPoin = CountPointToken(amount);
                    // code block
                    break;

                default:
                    gainPoin = 0;
                    break;
            }
            return gainPoin;
        }

        private int CountPointPulsa(double amount)
        {
            int poin = 0;
            if (amount > 100000)
            {
                poin = Convert.ToInt32(((amount - 100000) / 1000) * 2);
            }
            else if (amount > 10000 && amount <= 30000)
            {
                poin = Convert.ToInt32((amount / 1000) * 2);
            }
            return poin;
        }

        private int CountPointToken(double amount)
        {
            int poin = 0;
            if (amount > 100000)
            {
                poin = Convert.ToInt32(((amount - 100000) / 2000) * 2);
            }
            else if (amount > 10000 && amount <= 30000)
            {
                poin = Convert.ToInt32((amount / 2000) * 2);
            }
            return poin;
        }
    }
}