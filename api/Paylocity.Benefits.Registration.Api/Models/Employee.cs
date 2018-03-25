using Newtonsoft.Json;

namespace Paylocity.Benefits.Registration.Api.Models
{
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

        public override bool Equals(object obj)
        {
            var otherPerson = obj as Employee;
            if (otherPerson == null)
            {
                return false;
            }

            return
                (Id == otherPerson.Id) &&
                (FirstName == otherPerson.FirstName) &&
                (LastName == otherPerson.LastName) &&
                (AnnualSalary == otherPerson.AnnualSalary) &&
                (AnnualBenefitsCost == otherPerson.AnnualBenefitsCost) &&
                (Notes == otherPerson.Notes);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
