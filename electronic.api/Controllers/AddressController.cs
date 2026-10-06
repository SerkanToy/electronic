using electronic.Domain.Entities.Employees.Addresses;
using electronic.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace electronic.api.Controllers
{
    [Route("address/[action]")]
    [ApiController]
    public class AddressController : ApiBaseController
    {
        [HttpGet]
        [ActionName("addresses")]
        public async Task<ActionResult<ResponseModel>> GetAddresses([FromServices] ResponseModel<IEnumerable<Addresses>> responseModel)
        {

            responseModel.Data = await unitOfWork.GetRepository<Addresses>().GetAllAsync();
            responseModel.IsSuccess = true;
            responseModel.Message = new List<string> { "Addresses retrieved successfully." };


            return Ok(responseModel);
        }
        [HttpPost]
        public IActionResult CreateAddress([FromBody] string address)
        {
            // Logic to create a new address in the database
            // For demonstration, we just return the created address
            return CreatedAtAction(nameof(GetAddresses), new { address }, address);
        }
    }
}
