using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akinin_Stanislav_Test_Task_Yolva.Models
{
    public static class RegionConverter
    {
        public static RegionInternal ConvertOSMToInternal(OSMRegionExternal OSM) 
        {
            var polygons = OSM.geojson.coordinates.Select(
                polygon => polygon.Select(outline =>
                    outline.Select(coordList => ListToPoint(
                        coordList)).ToList()).ToList()).ToList();

            var regInternal = new RegionInternal(OSM.display_name, polygons);

            return regInternal;
        }

        private static PointF ListToPoint(IList<float> lst)
        {
            return new PointF(lst[0], lst[1]);
        }
    }
}
