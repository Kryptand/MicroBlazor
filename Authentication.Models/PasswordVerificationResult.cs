
namespace Authentication.Models
{
    public enum PasswordVerificationResult
    {
        Failed,
        Success,
        SuccessRehashNeeded,
    }
}
