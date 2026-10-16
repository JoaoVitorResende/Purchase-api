using Microsoft.AspNetCore.Mvc;
using PurchaseApi.Application.UseCases.Purchases.Get;
using PurchaseApi.Application.UseCases.Purchases.Register;
using PurchaseApi.Communication;
using PurchaseApi.Communication.Request;
using PurchaseApi.Communication.Response;

namespace PurchaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponsePurchase), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponsesErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
       [FromServices] IRegisterPurchaseUseCase useCase,
       [FromBody] RequestPurchaseItem request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponsePurchase), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory(
            [FromServices] IGetPurchasesUseCase useCase)
        {
            var response = await useCase.GetAll();
            return Ok(response);
        }
    }
}
