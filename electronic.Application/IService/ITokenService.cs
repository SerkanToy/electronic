namespace electronic.Application.IService
{
    public interface ITokenService
    {
        string CreateToken(object user, List<string> roles);
    }
}
