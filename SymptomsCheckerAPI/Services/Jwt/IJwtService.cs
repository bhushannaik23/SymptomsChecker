namespace Symptoms_Checker.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string email, string userType);
    }
}
