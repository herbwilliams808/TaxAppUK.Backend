using Shared.Models.Incomes.NonSavingsIncomes;
using Shared.Models.Incomes.NonSavingsIncomes.Employments;
using Shared.Models.Incomes.NonSavingsIncomes.NonPayeEmploymentIncome;
using Shared.Models.Incomes.NonSavingsIncomes.OtherEmploymentIncome;

namespace Shared.Models.Incomes;

public class Incomes
{
    public List<Employment> Employments { get; set; } = new();
    public NonPayeEmploymentIncome NonPayeEmploymentIncome { get; set; } = new();
    public OtherEmploymentIncome OtherEmploymentIncome { get; set; } = new();
    public UkPropertyBusinessIncome? UkPropertyBusiness { get; set; } = new();

}