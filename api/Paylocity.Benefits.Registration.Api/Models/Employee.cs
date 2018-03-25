using Newtonsoft.Json;

namespace Paylocity.Benefits.Registration.Api.Models
{
    /// <summary>
    /// Contains information for a registered employee.
    /// </summary>
    public class Employee
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal AnnualSalary { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal AnnualBenefitsCost { get; set; }

        public string Notes { get; set; }
    }
}
