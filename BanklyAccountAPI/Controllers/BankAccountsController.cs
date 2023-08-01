using BanklyAccountAPI.Models;
using BanklyAccountAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanklyAccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        // Assume the data access service is injected via Dependency Injection
        private readonly IBankAccountService _bankAccountService;
        private readonly ILogger<BankAccountsController> _logger;

        public BankAccountsController(IBankAccountService bankAccountService, ILogger<BankAccountsController> logger)
        {
            _bankAccountService = bankAccountService;
            _logger = logger;
        }

        // POST: api/BankAccounts
        [HttpPost("Add-Account")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BankAccount))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BankAccount>> AddBankAccount([FromBody] BankAccount bankAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data. Please provide valid bank account details.");
                }

                var createdAccount = await _bankAccountService.AddAccount(bankAccount);
                if (createdAccount == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not create account.contact Admin");
                }
                else
                {
                    return CreatedAtAction(nameof(GetBankAccountById), new { id = createdAccount.Id }, createdAccount);

                }

            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }


        }

        // GET: api/BankAccounts
        [HttpGet("Get-All-Accounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetAllBankAccounts()
        {
            var bankAccounts = await _bankAccountService.GetAllAccounts();
            return Ok(bankAccounts);
        }

        // GET: api/BankAccounts/5
        [HttpGet("Get-Account/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BankAccount>> GetBankAccountById([FromRoute] int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Invalid Company Id");
                }
                var bankAccount = await _bankAccountService.GetAccountById(id);

                if (bankAccount == null)
                {
                    return NotFound("Bank account not found.");
                }

                return Ok(bankAccount);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }


        [HttpPut("Update-Account/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<BankAccount>>> UpdateBankAccount(int id, [FromBody] BankAccount updatedBankAccount)
        {
            try
            {
                if (updatedBankAccount == null || id != updatedBankAccount.Id)
                {
                    return BadRequest("Invalid data. Please provide valid bank account details.");
                }

                var existingBankAccount = await _bankAccountService.GetAccountById(id);

                if (existingBankAccount == null)
                {
                    return NotFound("Bank account not found.");
                }

                var UpdatedAccounts = await _bankAccountService.UpdateAccount(updatedBankAccount);


                return Ok(UpdatedAccounts);
            }
           catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/BankAccounts/5
        [HttpDelete("Delete-Account{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<BankAccount>>> DeleteBankAccount(int id)
        {
            try
            {
                var accountToDelete = await _bankAccountService.GetAccountById(id);
                if (accountToDelete == null)
                {
                    return NotFound("Bank account not found.");
                }

                var UpdatedAccounts = await _bankAccountService.DeleteAccount(id);
                return Ok(UpdatedAccounts);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
