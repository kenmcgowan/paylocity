﻿using Newtonsoft.Json;

namespace Paylocity.Benefits.Registration.Api.Models
{
    /// <summary>
    /// Contains information for a registered dependent.
    /// </summary>
    public class Dependent
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal AnnualBenefitsCost { get; set; }

        public string Notes { get; set; }
    }
}
