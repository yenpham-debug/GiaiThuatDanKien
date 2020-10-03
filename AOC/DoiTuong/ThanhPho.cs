using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AOC.DoiTuong
{
    class ThanhPho
    {
        public string Ten { get; private set; }
        public Point ToaDo { get; private set; }
        public int ID { get; private set; }
        public ThanhPho(Point pt, string ten,int id)
        {
            this.ToaDo = pt;
            this.Ten = ten;
            this.ID = id;
        }
        public double KhoangCach(ThanhPho tp)
        {
            double KC = Math.Pow((this.ToaDo.X - tp.ToaDo.X), 2);
            KC += Math.Pow((this.ToaDo.Y - tp.ToaDo.Y), 2);
            KC = Math.Sqrt(KC);
            return KC;
        }
        
    }
}
