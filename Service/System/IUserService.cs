namespace Service.Catalogue
{
    public interface IUserService
    {
        Task<LoginViewModel> Login(UserLoginRequest request);
    }
    public class UserService : IUserService
    {
    }
}
