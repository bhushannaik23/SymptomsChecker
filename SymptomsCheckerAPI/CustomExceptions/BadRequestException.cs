
namespace Symptoms_Checker.CustomExceptions
{

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}