
using Shared.Data.Entities.Contracts;
namespace AuthenticationService.Data.Entities
{
    public class UserEntity:GuidEntity
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
