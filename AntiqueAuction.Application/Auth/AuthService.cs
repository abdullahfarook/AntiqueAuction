using System;
using System.Threading.Tasks;
using AntiqueAuction.Application.Auth.Dtos;
using AntiqueAuction.Core.Repository;

namespace AntiqueAuction.Application.Auth
{
    public interface IAuthService
    {
        Task Handle(UpdateMaxBid command);
    }
    public class AuthService:ServiceBase,IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(UpdateMaxBid command)
        {
            Validate(command);
            var user = await _userRepository.Get(command.UserId);
            user.UpdateMaxBid(command.Amount);
            await _userRepository.Update(user);
        }
    }
}
