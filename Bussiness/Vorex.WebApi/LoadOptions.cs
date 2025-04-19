using FluentValidation;

namespace Vorex.Application;

public record LoadOptions
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public string? SearchValue { get; set; }
}
    
public class LoadOptionsValidator : AbstractValidator<LoadOptions>
{
    public LoadOptionsValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0);

        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0);
    }
}

public record AdvanceLoadOptions : LoadOptions
{
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class AdvanceLoadOptionsValidator : AbstractValidator<AdvanceLoadOptions>
{
    public AdvanceLoadOptionsValidator()
    {
        Include(new LoadOptionsValidator());
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .When(x => x.EndDate.HasValue);
    }
}