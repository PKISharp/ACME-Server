using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Storage.FileStore.Configuration
{
    public class FileStoreOptions
    {
        private string? _noncePath;
        private string? _accountPath;

        public string NoncePath {
            get => _noncePath ?? throw new NotInitializedException(); 
            set => _noncePath = value; 
        }

        public string AccountPath { 
            get => _accountPath ?? throw new NotInitializedException(); 
            set => _accountPath = value; 
        }
    }
}
