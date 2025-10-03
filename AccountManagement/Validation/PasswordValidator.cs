using System.Text.RegularExpressions;

namespace AccountManagement.API.Validation   
{
    public static class PasswordValidator
    {
        //Pass must be in 8 chars, at least 1 lowercase, 1 uppercase, 1 digit, 1 special char
        private static readonly Regex StrongPasswordRegex =
            new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");

        public static bool IsStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            return StrongPasswordRegex.IsMatch(password);
        }
    }
}
