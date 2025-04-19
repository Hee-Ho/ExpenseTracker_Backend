using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Repo;
using ExpenseTrackerAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/Transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly TransactionsRepo _transactionRepo;

        public TransactionController (TransactionsRepo transactionsRepo)
        {
            _transactionRepo = transactionsRepo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTransaction([FromBody]TransactionAddDTO transaction)
        {
            try
            {
                if (!ParameterCheck.validateDateFormat(transaction.date))
                {
                    return BadRequest("Invalid Date Format(Use yyyy-MM-dd)");
                }
                if (!ParameterCheck.validateMoneyFormat(transaction.amount))
                {
                    return BadRequest("Invalid ammount format(Use 2 decimals place)");
                }

                int status = await _transactionRepo.AddTransactionAsync(transaction);
                return Created();

            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTransaction([FromBody]TransactionDeleteDTO transaction)
        {
            try
            {
                int status = await _transactionRepo.DeleteTransactionAsync(transaction);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("edit")]
        public async Task<IActionResult> UpdateTransaction([FromBody]TransactionUpdateDTO transaction)
        {
            try
            {
                if (transaction.new_amount != null && !ParameterCheck.validateMoneyFormat(transaction.new_amount))
                {
                    return BadRequest("Invalid ammount format(Use 2 decimals place)");
                }
                if (transaction.new_date != null && !ParameterCheck.validateDateFormat(transaction.new_date))
                {
                    return BadRequest("Invalid Date Format(Use yyyy-MM-dd)");
                }
                int status = await _transactionRepo.UpdateTransactionAsync(transaction);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
