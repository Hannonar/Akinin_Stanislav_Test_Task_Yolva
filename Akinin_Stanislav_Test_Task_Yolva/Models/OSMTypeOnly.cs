using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akinin_Stanislav_Test_Task_Yolva.Models
{
    public class OSMTypeOnly
    {
        public GeojsonNoCoords geojson { get; set; }
    }

    public class GeojsonNoCoords
    {
        public string type { get; set; }
    }
}
