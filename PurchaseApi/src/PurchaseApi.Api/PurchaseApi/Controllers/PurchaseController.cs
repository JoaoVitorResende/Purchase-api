using Microsoft.AspNetCore.Mvc;
using PurchaseApi.Application.UseCases.Purchases.Get;
using PurchaseApi.Application.UseCases.Purchases.Register;
using PurchaseApi.Communication;
using PurchaseApi.Communication.Request;
using PurchaseApi.Communication.Response;

namespace PurchaseApi.Controllers
{
    [Route("api/purchases")]
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
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ResponsePurchase>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetPurchasesUseCase useCase)
        {
            var response = await useCase.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponsePurchase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponsesErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetPurchasesUseCase useCase,
            [FromRoute] Guid id)
        {
            var response = await useCase.GetById(id);
            return Ok(response);
        }
    }
}
