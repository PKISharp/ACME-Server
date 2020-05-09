namespace TG_IT.ACME.Protocol.HttpModel
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
