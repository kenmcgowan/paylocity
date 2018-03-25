using Newtonsoft.Json;

namespace Paylocity.Benefits.Registration.Api.Models
{
    public class Dependent
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal AnnualBenefitsCost { get; set; }

        public string Notes { get; set; }

        public override bool Equals(object obj)
        {
            var otherPerson = obj as Dependent;
            if (otherPerson == null)
            {
                return false;
            }

            return
                (Id == otherPerson.Id) &&
                (EmployeeId == otherPerson.EmployeeId) &&
                (FirstName == otherPerson.FirstName) &&
                (LastName == otherPerson.LastName) &&
                (AnnualBenefitsCost == otherPerson.AnnualBenefitsCost) &&
                (Notes == otherPerson.Notes);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
