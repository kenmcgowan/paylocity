using Newtonsoft.Json;

namespace Paylocity.Benefits.Registration.Api.Models
{
    /// <summary>
    /// Provides summary information for a single pay period.
    /// </summary>
    public class PayPeriod
    {
        public int Number { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal GrossPay { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal Deductions { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal NetPay { get; set; }
    }
}
