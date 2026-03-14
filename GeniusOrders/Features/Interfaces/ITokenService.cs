using GeniusOrders.Entities;

namespace GeniusOrders.Features.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, IList<string> roles);
    }
}