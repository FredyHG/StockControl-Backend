using FluentValidation;
using StockControl.Application.Requests.Supplier;

namespace StockControl.Presentation.Controllers.Validation;

public class SupplierRequestValidation : AbstractValidator<SupplierPostRequest>
{
    public SupplierRequestValidation()
    {
        RuleFor(x => x.Contacts)
            .Must(x => x is { Count: <= 3 })
            .WithMessage("Please specify at least 3 contacts");

        RuleFor(x => x.Addresses)
            .Must(x => x is { Count: <= 3 })
            .WithMessage("Please specify at least 3 addresses");
    }
}