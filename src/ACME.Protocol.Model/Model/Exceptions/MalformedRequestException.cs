namespace TG_IT.ACME.Protocol.Model.Exceptions
{
    public class MalformedRequestException : AcmeException
    {
        public MalformedRequestException(string message)
            : base(message)
        { }

        public override string ErrorType => "malformed";
    }

    public class NotFoundException : MalformedRequestException
    {

    }

    public class ConflictRequestException : MalformedRequestException
    {
        public ConflictRequestException(AccountStatus status)
        {

        }

        public ConflictRequestException(OrderStatus status)
        {

        }

        public ConflictRequestException(AuthorizationStatus status)
        {

        }

        public ConflictRequestException(ChallengeStatus status)
        {

        }
    }
}
