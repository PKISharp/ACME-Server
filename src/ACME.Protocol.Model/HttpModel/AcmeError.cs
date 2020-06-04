using System.Collections.Generic;
using System.Linq;

namespace TGIT.ACME.Protocol.HttpModel
{
    public class AcmeError
    {
        public AcmeError(Model.AcmeError model)
        {
            if (model is null)
                throw new System.ArgumentNullException(nameof(model));

            Type = model.Type;
            Detail = model.Detail;

            if(model.Identifier.HasValue)
                Identifier = new Identifier(model.Identifier.Value);

            Subproblems = model.SubErrors?
                .Select(x => new AcmeError(x))
                .ToList();
        }

        public string Type { get; set; }
        public string Detail { get; set; }
        
        public List<AcmeError>? Subproblems { get; set; }
        public Identifier? Identifier { get; set; }
    }
}
