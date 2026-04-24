using System;

namespace HMS.BL
{
    public class ValidationBL
    {
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            foreach (char c in name)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            if (password.Length < 8) return false;

            if (password.Contains(" ")) return false;

            return true;
        }
    }
}