namespace ACME.Protocol.Model.Exceptions
{
    public class MalformedRequestException : AcmeException
    {
        public MalformedRequestException(string message)
            : base(message)
        { }

        public override string ErrorType => "malformed";
    }
}
