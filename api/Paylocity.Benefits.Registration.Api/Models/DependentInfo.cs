namespace Paylocity.Benefits.Registration.Api.Models
{
    /// <summary>
    /// Contains information for a dependent who is not yet registered in the system.
    /// </summary>
    public class DependentInfo
    {
        public long EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualBenefitsCost { get; set; }
        public string Notes { get; set; }
    }
}
