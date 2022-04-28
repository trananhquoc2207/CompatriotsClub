namespace ViewModel.System
{
#nullable disable
    public class UserViewModel
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class UserResponseViewModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginViewModel
    {
        public int Id { get; set; }
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
}
