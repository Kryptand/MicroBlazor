
using Shared.Data.DataClasses.Contracts;
namespace AuthenticationService.DataClasses
{
    public class UserEntity:GuidEntity
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
