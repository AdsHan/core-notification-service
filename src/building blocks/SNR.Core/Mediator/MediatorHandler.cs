using MediatR;
using SNR.Core.Commands;
using SNR.Core.Communication;
using System.Threading.Tasks;

namespace SNR.Core.Mediator
{

    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BaseResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task<object> SendQuery<T>(T query)
        {
            return await _mediator.Send(query);
        }
    }
}