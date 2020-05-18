using System.Globalization;
using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.HttpModel
{
    public class Challenge
    {
        public Challenge(Model.Challenge model, string challengeUrl)
        {
            if (model is null)
                throw new System.ArgumentNullException(nameof(model));

            if (string.IsNullOrEmpty(challengeUrl))
                throw new System.ArgumentNullException(nameof(challengeUrl));

            Type = model.Type;
            Status = model.Status.ToString().ToLowerInvariant();
            Url = challengeUrl;

            Validated = model.Validated?.ToString("o", CultureInfo.InvariantCulture);
            Error = model.Error;
        }


        public string Type { get; }

        public string Status { get; }

        public string? Validated { get; }
        public Error? Error { get; }

        public string Url { get; }
    }
}
