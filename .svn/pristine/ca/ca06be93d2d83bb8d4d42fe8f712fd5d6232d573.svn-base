using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ZLERP.Model.ViewModels
{
    public class GeometryModels
    {
    }
    //点
    [DataContract]
    public class Point
    {
        [DataMember]
        public string Id{ set; get; }
        [DataMember]
        public double X { set; get; }
        [DataMember]
        public double Y { set; get; }
        public Point(double x, double y) { this.X = x; this.Y = y; }
        public Point() { }
        /*附属的参数*/
        [DataMember]
        public decimal Spd { get; set; }
        [DataMember]
        public decimal Oil { get; set; }
        [DataMember]
        public string Time { set; get; }
        [DataMember]
        public string Acc { get; set; }
    }
    //线
    public class Line
    {
        public Point a { set; get; }public Point b { set; get; }
        public Line(Point start, Point end) { this.a = start; this.b = end; }
    }
    //多边形
    public class Polygon
    {
        public List<Line> Plines { get; set; }
    }
}