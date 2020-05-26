using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TotvsChallenge.Core.Helpers;

namespace TotvsChallenge.Core.Requests.Report.FindByOperationId
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
