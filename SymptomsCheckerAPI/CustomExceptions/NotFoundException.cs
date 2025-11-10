namespace Symptom_Checker.CustomExceptions
{


    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}