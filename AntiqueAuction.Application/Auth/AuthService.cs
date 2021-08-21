using System;
using System.Threading.Tasks;
using AntiqueAuction.Application.Auth.Dtos;

namespace AntiqueAuction.Application.Auth
{
    public interface IAuthService
    {
        Task<string> Handle(GenerateToken command);
    }
    public class AuthService:ServiceBase,IAuthService
    {
        public Task<string> Handle(GenerateToken command)
        {
            throw new NotImplementedException();
        }
    }
}
