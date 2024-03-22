namespace EmailServiceDemo
{
    public class TokenGenerator
    {
        public string GenerateToken(string prefix = "token", int length = 25)
        {
            if (prefix.Length > length)
            {
                throw new ArgumentException("Prefix length should not exceed the total length.");
            }

            string randomPart = Guid.NewGuid().ToString("N").Substring(0, length - prefix.Length);

            string token = $"{prefix}{randomPart}";
            return token;
        }
    }
}
