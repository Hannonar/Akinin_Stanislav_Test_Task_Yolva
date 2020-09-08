using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akinin_Stanislav_Test_Task_Yolva.Models
{
    public class RegionInternal
    {
        private string display_name;
        private IEnumerable<IEnumerable<Point>> polygons;

        public RegionInternal(string Name, IEnumerable<IEnumerable<IEnumerable<PointF>>> Polygons)
        {
            this.Name = display_name;
            this.Polygons = Polygons.Select(outline => outline.Select(coordList => coordList.ToList()).ToList()).ToList();
        }

        public string Name { get; private set; }
        public List<List<List<PointF>>> Polygons { get; private set; }//Список полигонов, состоящих из контуров, состоящих из точек

        public void SimplifyAllPolygons(int PointFrequency)
        {
            for (var i = 0; i < Polygons.Count; i++)
                SimplifyPolygon(PointFrequency, i);
        }

        public void SimplifyPolygon(int PointFrequency, int PolygonId)
        {
            if (PolygonId < 0 || PolygonId >= Polygons.Count)
                throw new IndexOutOfRangeException();

            if (PointFrequency <= 1 || Polygons[PolygonId].Count / 3.0 < 3)
                throw new Exception("Частота точек слишком мала или оставшихся точек недостаточно для формирования полигона");


            var Points = Polygons[PolygonId].Where((point, i) => (i + 1) % PointFrequency == 0).ToList();
            Polygons[PolygonId] = Points;

            if(Polygons[PolygonId].First() != Polygons[PolygonId].Last())
                Polygons[PolygonId].Add(Polygons[PolygonId].First());
        }
    }
}
