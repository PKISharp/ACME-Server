using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace TGIT.ACME.Storage.FileStore.Configuration
{
    public class FileStoreOptions : IValidatableObject
    {
        public string NoncePath { get; set; } = null!;

        public string AccountPath { get; set; } = null!;

        public string OrderPath { get; set; } = null!;

        public string WorkingPath { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(NoncePath) || !Directory.Exists(NoncePath))
                yield return new ValidationResult("NoncePath was empty or did not exist.", new[] { nameof(NoncePath) });

            if (string.IsNullOrWhiteSpace(AccountPath) || !Directory.Exists(AccountPath))
                yield return new ValidationResult("AccountPath was empty or did not exist.", new[] { nameof(AccountPath) });
            
            if (string.IsNullOrWhiteSpace(OrderPath) || !Directory.Exists(OrderPath))
                yield return new ValidationResult("OrderPath was empty or did not exist.", new[] { nameof(OrderPath) });
            
            if (string.IsNullOrWhiteSpace(WorkingPath) || !Directory.Exists(WorkingPath))
                yield return new ValidationResult("WorkingPath was empty or did not exist.", new[] { nameof(WorkingPath) });
        }
    }
}
