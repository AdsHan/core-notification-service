using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNR.Core.Controllers;
using SNR.Core.Mediator;
using SNR.Notification.API.Application.Messages.Commands.NotificationCommand;
using System.Threading.Tasks;

namespace SNR.Notification.API.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/")]
    [ApiController]
    public class NotificationController : BaseController
    {

        private readonly IMediatorHandler _mediator;

        public NotificationController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        // POST api/1.0/user-created
        /// <summary>
        /// Gera uma notificação de usuário criado
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST / Notificação
        ///     {
        ///         "name": "Théo da Silva",
        ///         "email": "adshan@gmail.com",
        ///     }        
        ///     
        /// </remarks>        
        /// <returns>Retorna notificação gerada</returns>                
        /// <response code="201">A notificação foi gerada com sucesso</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost("user-created")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ActionName("GenerateNotification")]
        public async Task<IActionResult> PostAsync([FromBody] UserCreatedNotificationCommand command)
        {
            var result = await _mediator.SendCommand(command);

            return result.ValidationResult.IsValid ? CreatedAtAction("GenerateNotification", new { id = result.response }, command) : CustomResponse(result.ValidationResult);
        }

    }
}
