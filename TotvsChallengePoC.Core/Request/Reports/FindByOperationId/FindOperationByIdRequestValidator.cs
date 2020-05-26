using FluentValidation;
using System;
using TotvsChallengePoC.Core.Helpers;

namespace TotvsChallengePoC.Core.Request.Reports.FindByOperationId
{
    public class FindOperationByIdRequestValidator : AbstractValidator<FindOperationByIdRequest>
    {
        public FindOperationByIdRequestValidator()
        {
            RuleFor(x => x.OperationId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío")
                .NotNull().WithMessage("{PropertyName} no puede estar vacío")
                .Must(IsValidGuid).WithMessage("{PropertyName} tiene un formato de Guid inválido");
        }
        private bool IsValidGuid(string id)
            => Guid.NewGuid().ValidateGuid(id);
    }
}
