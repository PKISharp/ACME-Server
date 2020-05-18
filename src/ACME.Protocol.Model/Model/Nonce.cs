using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Model
{
    public class Nonce
    {
        private string? _token;

        public string Token {
            get => _token ?? throw new NotInitializedException();
            set => _token = value; }
    }
}
