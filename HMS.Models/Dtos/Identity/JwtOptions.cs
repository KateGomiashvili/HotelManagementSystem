

namespace HMS.Models.Dtos.Identity
{
    public class JwtOptions
    {
        public string Key { get; set; } = "1D03F090-5E9F-44D0-84C8-F43BD3CD80E7-C2C5E299-56C7-4C2E-9E31-769638E70D08";
        public string Issuer { get; set; } = "hms - auth - api";
        public string Audience { get; set; } = "hms-client";
        public int ExpireMinutes { get; set; } = 0;
    }
}
