namespace Domain.Models.Dtos.Users
{
    public class UserAuthenticateResponseDto
    {
        public UserAccountDto User { get; }
        public string Token { get; }

        public UserAuthenticateResponseDto(UserAccountDto user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
