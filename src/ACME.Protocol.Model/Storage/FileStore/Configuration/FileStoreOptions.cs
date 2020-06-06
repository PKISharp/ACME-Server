using TGIT.ACME.Protocol.Model.Exceptions;

namespace TGIT.ACME.Protocol.Storage.FileStore.Configuration
{
    public class FileStoreOptions
    {
        private string? _noncePath;
        private string? _accountPath;
        private string? _workingPath;
        private string? _orderPath;

        public string NoncePath
        {
            get => _noncePath ?? throw new NotInitializedException();
            set => _noncePath = value;
        }

        public string AccountPath
        {
            get => _accountPath ?? throw new NotInitializedException();
            set => _accountPath = value;
        }

        public string OrderPath { 
            get => _orderPath ?? throw new NotInitializedException();
            set => _orderPath = value; 
        }

        public string WorkingPath
        {
            get => _workingPath ?? throw new NotInitializedException();
            set => _workingPath = value;
        }

    }
}
