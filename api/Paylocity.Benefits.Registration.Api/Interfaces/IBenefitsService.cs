using Paylocity.Benefits.Registration.Api.Models;

namespace Paylocity.Benefits.Registration.Api.Interfaces
{
    public interface IBenefitsService
    {
        BenefitsInfo GetAnnualEmployeeBenefitsCost(Person employee);
        BenefitsInfo GetAnnualDependentBenefitsCost(Person dependent);
    }
}
