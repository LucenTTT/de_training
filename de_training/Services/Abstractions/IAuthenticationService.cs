namespace de_training.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAsync(string name, string password);
        Task RegistrateUserAsync(string name, string password);
    }
}
