using FluentValidation;
using System;
using TotvsChallengePoC.Core.Helpers;

namespace TotvsChallengePoC.Core.Request.Reports.FindClientInfoById
{
    public class FindClientInfoByIdRequestValidator : AbstractValidator<FindClientInfoByIdRequest>
    {
        public FindClientInfoByIdRequestValidator()
        {
            RuleFor(x => x.ClientId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío")
                .NotNull().WithMessage("{PropertyName} no puede estar vacío")
                .Must(IsValidGuid).WithMessage("{PropertyName} tiene un formato de Guid inválido");
        }
        private bool IsValidGuid(string id)
            => Guid.NewGuid().ValidateGuid(id);
    }
}
