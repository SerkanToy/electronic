namespace electronic.Domain.Exceptions
{
    public class RegistrationFailedException(IEnumerable<string> errors)
        : Exception($"Kayıt İşlemi Bararısız (Oluşan Hatalar) : {string.Join(Environment.NewLine, errors)}");
}
