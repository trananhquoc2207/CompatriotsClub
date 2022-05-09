using ViewModel.common;

namespace ViewModel.System
{
#nullable disable
    public class UserViewModel
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
    public class UserResponseViewModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginViewModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class UserFilter : PagingFilter
    {
        public string Keyword { get; set; }
    }

    public class ResponseLoginModel : LoginViewModel
    {

        public List<string> PermissionList { get; set; }
    }
}
