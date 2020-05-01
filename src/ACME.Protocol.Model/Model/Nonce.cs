using TG_IT.ACME.Protocol.Model.Exceptions;

namespace TG_IT.ACME.Protocol.Model
{
    public class Nonce
    {
        private string? _token;

        public string Token {
            get => _token ?? throw new NotInitializedException();
            set => _token = value; }
    }
}
