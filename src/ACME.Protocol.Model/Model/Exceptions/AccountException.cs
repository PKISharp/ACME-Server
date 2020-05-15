namespace TG_IT.ACME.Protocol.Model.Exceptions
{
    public class AccountException : AcmeException
    {
        public override string ErrorType { get; }

        public AccountException()
            : base("The request specified an account that does not exist") 
        {
            ErrorType = "accountDoesNotExist";
        }

        public AccountException(AccountStatus accountStatus)
            : base($"The request specified an account in a bad state ({accountStatus})")
        {
            UrnBase = "urn:custom-errors";
            ErrorType = "bad-account-state";
        }
    }

    public class InvalidStateException : AcmeException
    {
        public override string ErrorType { get; }

        public InvalidStateException()
            :base()
        {
            UrnBase = "urn:custom-errors";
            ErrorType = "bad-state";
        }
    }
}
