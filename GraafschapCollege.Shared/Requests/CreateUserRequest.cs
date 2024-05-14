using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraafschapCollege.Shared.Requests
{
    internal class CreateUserRequest
    {
        public string name { get; set;}
        public string email { get; set;}
        public string password { get; set;}
        public string repeatPassword { get; set;}
        public List<string> roles { get; set;} 
    }
}
