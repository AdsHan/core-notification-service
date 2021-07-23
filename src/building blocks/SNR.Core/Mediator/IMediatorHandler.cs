using SNR.Core.Commands;
using SNR.Core.Communication;
using System.Threading.Tasks;

namespace SNR.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<BaseResult> SendCommand<T>(T command) where T : Command;
        Task<object> SendQuery<T>(T query);
    }
}