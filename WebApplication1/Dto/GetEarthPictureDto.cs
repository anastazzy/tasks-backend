using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Dto
{
    public class GetEarthPictureDto
    {
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Date { get; set; }
        public string Dim { get; set; }
    }
}
