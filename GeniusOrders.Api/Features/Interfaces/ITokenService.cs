using GeniusOrders.Api.Entities;

namespace GeniusOrders.Api.Features.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user ,IList<string> roles);
    }
}