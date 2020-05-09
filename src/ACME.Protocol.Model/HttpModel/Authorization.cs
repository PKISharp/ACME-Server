using System.Collections.Generic;
using System.Globalization;
using TG_IT.ACME.Protocol.Model;

namespace TG_IT.ACME.Protocol.HttpModel
{
    public class Authorization
    {
        public Authorization(Model.Authorization model)
        {
            if (model is null)
                throw new System.ArgumentNullException(nameof(model));

            Status = model.Status.ToString().ToLowerInvariant();

            Expires = model.Expires?.ToString("o", CultureInfo.InvariantCulture);
            Wildcard = model.IsWildcard;

            Identifier = new Identifier(model.Identifier);
            Challenges = new List<Challenge>();
        }

        public string Status { get; }

        public Identifier Identifier { get; }
        public string? Expires { get; }
        public bool? Wildcard { get; }

        public List<Challenge> Challenges { get; }
    }
}
