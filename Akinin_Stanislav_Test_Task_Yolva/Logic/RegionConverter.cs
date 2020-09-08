using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Akinin_Stanislav_Test_Task_Yolva.Models
{
    public static class RegionConverter
    {
        public static RegionInternal ProcessMessage(string msg) 
        {
            RegionInternal result;
            var RegList = JsonConvert.DeserializeObject<IList<OSMTypeOnly>>(msg);
            var RegType = RegList[0].geojson.type;

            if (RegType == "Polygon")
                result = ConvertOSMPolToInternal(JsonConvert.DeserializeObject<IList<OSMRegionExternalPolygon>>(msg)[0]);
            else if (RegType == "MultiPolygon")
                result = ConvertOSMMultyPolToInternal(JsonConvert.DeserializeObject< IList<OSMRegionExternalMultyPolygon>>(msg)[0]);
            else result = new RegionInternal();

            return result;
        }

        private static RegionInternal ConvertOSMMultyPolToInternal(OSMRegionExternalMultyPolygon OSMMP) 
        {
            var polygons = OSMMP.geojson.coordinates.Select(
                polygon => polygon.Select(outline =>
                    outline.Select(coordList => ListToPoint(
                        coordList)).ToList()).ToList()).ToList();

            var regInternal = new RegionInternal(OSMMP.display_name, polygons);

            return regInternal;
        }

        private static RegionInternal ConvertOSMPolToInternal(OSMRegionExternalPolygon OSMP)
        {
            var polygon = OSMP.geojson.coordinates.Select(outline =>
                    outline.Select(coordList => ListToPoint(
                        coordList)).ToList()).ToList();

            List<List<List<PointF>>> polygons = new List<List<List<PointF>>>() { polygon };


            var regInternal = new RegionInternal(OSMP.display_name, polygons);

            return regInternal;
        }

        private static PointF ListToPoint(IList<float> lst)
        {
            return new PointF(lst[0], lst[1]);
        } 
    }
}
