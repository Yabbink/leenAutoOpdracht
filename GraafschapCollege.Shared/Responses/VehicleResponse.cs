﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraafschapCollege.Shared.Responses
{
    public class VehicleResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
