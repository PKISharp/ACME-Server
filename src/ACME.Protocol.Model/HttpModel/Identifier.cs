using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized.

namespace TGIT.ACME.Protocol.HttpModel
{
    public class Identifier
    {
        // This object can be used as part of a request, it needs an empty ctor.
        private Identifier() { }

        public Identifier(Model.Identifier model)
        {
            if (model is null)
                throw new System.ArgumentNullException(nameof(model));

            Type = model.Type;
            Value = model.Value;
        }

        public string Type { get; set; }
        public string Value { get; set; }
    }
}
