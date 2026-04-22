namespace electronic.Domain.Exceptions
{
    public class LoginFailedException(string email) : Exception($"Geçersiz E-Posta: {email} veya Şifre : {email.Replace(email, "*")}");
}
