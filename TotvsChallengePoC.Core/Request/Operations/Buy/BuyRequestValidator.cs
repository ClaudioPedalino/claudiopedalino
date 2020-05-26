using FluentValidation;
using System;
using TotvsChallengePoC.Core.Helpers;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Core.Request.Buy
{
    public class BuyRequestValidator : AbstractValidator<BuyRequest>
    {
        public BuyRequestValidator()
        {
            RuleFor(x => x.ClientId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío")
                .NotNull().WithMessage("{PropertyName} no puede estar vacío")
                .Must(IsValidGuid).WithMessage("{PropertyName} tiene un formato de Guid inválido");

            RuleFor(x => x.PaymentType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío")
                .NotNull().WithMessage("{PropertyName} no puede estar vacío")
                .LessThanOrEqualTo(MoneyHelper.PaymentTypesDic.Count).WithMessage("{PropertyName} no contiene un id válido")
                .GreaterThan(0).WithMessage("{PropertyName} no contiene un id válido");

            RuleFor(x => x.TotalAmount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío")
                .NotNull().WithMessage("{PropertyName} no puede estar vacío")
                //Note: I cant create a validator to check TotalAmount > ClientPaymentAmount
                .GreaterThan(0).WithMessage("{PropertyName} no puede ser negativo");

            RuleFor(x => x.ClientPaymentAmount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío")
                .NotNull().WithMessage("{PropertyName} no puede estar vacío")
                //Note: I cant create a validator to check ClientPaymentAmount < TotalAmount 
                .GreaterThan(0).WithMessage("{PropertyName} no puede ser negativo");

        }

        private bool IsValidGuid(string id)
            => Guid.NewGuid().ValidateGuid(id);
    }
}
