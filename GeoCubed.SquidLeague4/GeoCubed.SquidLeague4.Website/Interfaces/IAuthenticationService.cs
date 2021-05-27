using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string username, string password);

        Task<bool> Register(string username, string password);

    }
}
