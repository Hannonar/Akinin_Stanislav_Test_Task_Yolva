using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akinin_Stanislav_Test_Task_Yolva.Models
{
    public class OSMRegionExternalMultyPolygon
    {
        //public int place_id { get; set; }
        //public string licence { get; set; }
        //public string osm_type { get; set; }
        //public int osm_id { get; set; }
        //public IList<string> boundingbox { get; set; }
        //public string lat { get; set; }
        //public string lon { get; set; }
        public string display_name { get; set; }
        //public string Class { get; set; }
        //public string type { get; set; }
        //public double importance { get; set; }
        //public string icon { get; set; }
        public GeojsonMP geojson { get; set; }
    }

    public class GeojsonMP
    {
        public string type { get; set; }
        public IList<IList<IList<IList<float>>>> coordinates { get; set; }
        //массив полигонов > полигон > (внешний контур, внутреннее кольцо/дырки) > точки
    }
}
