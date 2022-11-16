namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigninKey { get; set; }
        public string IssuerSigninKey { get; set; } = string.Empty;
        public bool ValidateIssuer { get; set; } = true;
        public string ValidIsuer { get; set; } = string.Empty;
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifeTime { get; set; } = true;
    }
}
