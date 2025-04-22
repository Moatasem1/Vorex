using FluentValidation;

namespace Vorex.Application;

public record BasicLoadOptions
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public string? SearchValue { get; set; }
}
    
public class LoadOptionsValidator : AbstractValidator<BasicLoadOptions>
{
    public LoadOptionsValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0);

        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0);
    }
}

public record PeriodLoadOptions
{
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}


public class PeriodLoadOptionsValidator : AbstractValidator<PeriodLoadOptions>
{
    public PeriodLoadOptionsValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .When(x => x.EndDate.HasValue);
    }
}

public record AdvanceLoadOptions : BasicLoadOptions
{
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
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