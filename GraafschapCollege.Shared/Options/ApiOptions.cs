using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraafschapCollege.Shared.Options
{
    public class ApiOptions
    {
        public const string SectionName = "Api";
        public string BaseUrl { get; set; } = default!;
    }
}
