using System.Globalization;
using TG_IT.ACME.Protocol.Model.Exceptions;

namespace TG_IT.ACME.Protocol.HttpModel
{
    public class Challenge
    {
        public Challenge(Model.Challenge model)
        {
            Type = model.Type;
            Status = model.Status.ToString().ToLowerInvariant();

            Validated = model.Validated?.ToString("o", CultureInfo.InvariantCulture);
            Error = model.Error;
        }


        public string Type { get; }

        public string Status { get; }

        public string? Validated { get; set; }
        public Error? Error { get; set; }

        private string? _url;
        public string Url
        {
            get => _url ?? throw new NotInitializedException();
            set => _url = value;
        }
    }
}
