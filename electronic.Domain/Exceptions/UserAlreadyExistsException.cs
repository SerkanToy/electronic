namespace electronic.Domain.Exceptions
{
    public class UserAlreadyExistsException(string email) : Exception($"'{email}' E-Posta adresine sahip kullanıcı zaten var.");
}
