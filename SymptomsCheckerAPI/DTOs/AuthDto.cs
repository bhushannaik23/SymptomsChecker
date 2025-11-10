namespace Symptoms_Checker.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } // "patient", "doctor", "admin"
    }

    public class RegisterPatientDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

    public class RegisterDoctorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public List<int> SpecialtyIds { get; set; }
    }

    public class RegisterAdminDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string UserType { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
