using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized.

namespace TGIT.ACME.Protocol.HttpModel
{
    public class Identifier
    {
        private Identifier() { }

        public Identifier(Model.Identifier model)
        {
            Type = model.Type;
            Value = model.Value;
        }

        public string Type { get; set; }
        public string Value { get; set; }
    }
}
