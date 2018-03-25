namespace Paylocity.Benefits.Registration.Api.Models
{
    /// <summary>
    /// Contains information for an employee who is not yet registered in the system.
    /// </summary>
    public class EmployeeInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal AnnualBenefitsCost { get; set; }
        public string Notes { get; set; }
    }
}
