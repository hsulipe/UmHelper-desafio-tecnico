using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions.Base;
using Domain.Models.Dtos.Transactions;
using Domain.Models.Entities;
using Infrastructure.Repositories.Transactions;
using Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using UmHelpFinanceiro.Services.Transactions;


namespace UmHelpFinanceiro.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionRepository transactionRepository, IUserAccountRepository userAccountRepository, ITransactionService transactionService)
        {
            _transactionRepository = transactionRepository;
            _userAccountRepository = userAccountRepository;
            _transactionService = transactionService;
        }

        [HttpGet("users/{userId}")]
        public ActionResult<IEnumerable<TransactionDto>> ListByDateRange(
            [FromRoute]Guid userId, 
            [FromQuery]DateTime? start,
            [FromQuery]DateTime? end
            )
        {
            if (start == null || end == null) return BadRequest();
            return Ok(_transactionRepository.ListByUserAndDateRange(userId, (DateTime) start, (DateTime) end));
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody]RegisterTransactionRequest request)
        {
            var userFrom = await _userAccountRepository.FindAsync(request.From);
            var userTo = await _userAccountRepository.FindAsync(request.To);

            if (userTo == null || userFrom == null) return NotFound();
            if (request.Value <= 0 || userFrom.CurrentBalance < request.Value) return BadRequest();
            
            try
            {
                await _transactionService.RegisterAsync((Transaction) request);
            }
            catch (DomainExceptionBase err)
            {
                return BadRequest(err.Message);
            }
            catch (ArgumentException err)
            {
                return BadRequest(err.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Revert(Guid id)
        {
            var transaction = await _transactionRepository.FindAsync(id);
            if (transaction == null) return NotFound();

            var userFrom = await _userAccountRepository.FindAsync(transaction.From);
            var userTo = await _userAccountRepository.FindAsync(transaction.To);

            if (userTo.CurrentBalance < transaction.Value) return BadRequest();

            try
            {
                await _transactionService.RevertAsync(id);
            }
            catch (DomainExceptionBase err)
            {
                return BadRequest(err.Message);
            }
            catch (ArgumentException err)
            {
                return BadRequest(err.Message);
            }

            return Ok();
        }
    }
}
