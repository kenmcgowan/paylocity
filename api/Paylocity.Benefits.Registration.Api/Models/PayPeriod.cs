using Newtonsoft.Json;

namespace Paylocity.Benefits.Registration.Api.Models
{
    public class PayPeriod
    {
        public int Number { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal GrossPay { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal Deductions { get; set; }

        [JsonConverter(typeof(DecimalToStringJsonConverter))]
        public decimal NetPay { get; set; }

        public override bool Equals(object obj)
        {
            var otherPayPeriod = obj as PayPeriod;

            if (otherPayPeriod == null)
            {
                return false;
            }

            return
                (Number == otherPayPeriod.Number) &&
                (GrossPay == otherPayPeriod.GrossPay) &&
                (Deductions == otherPayPeriod.Deductions) &&
                (NetPay == otherPayPeriod.NetPay);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
    }
}
