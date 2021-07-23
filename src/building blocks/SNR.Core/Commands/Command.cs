using MediatR;
using SNR.Core.Communication;
using System;

namespace SNR.Core.Commands
{
    public abstract class Command : IRequest<BaseResult>
    {
        protected Command()
        {
            BaseResult = new BaseResult();
        }

        public BaseResult BaseResult { get; set; }

        public virtual bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}