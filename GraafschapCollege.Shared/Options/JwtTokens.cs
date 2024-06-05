using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraafschapCollege.Shared.Options
{
    public class JwtTokens
    {
        public const string SectionName = "Jwt";
        public string Key { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }
}
