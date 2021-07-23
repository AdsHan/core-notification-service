﻿using FluentValidation.Results;
using SNR.Core.Communication;

namespace SNR.Core.Commands
{
    public abstract class CommandHandler
    {
        protected BaseResult BaseResult;

        protected CommandHandler()
        {
            BaseResult = new BaseResult();
        }

        protected void AddError(string message)
        {
            BaseResult.ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

    }
}