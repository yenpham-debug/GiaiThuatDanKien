using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AOC.DoiTuong;

namespace AOC.ThuVien
{
    class XuLyAnh
    {
        public static double tyLeHienThi = 1;
        private static Bitmap Ve(Size sz,List<ThanhPho> DSThanhPho, bool DieuChinhDuLieu)
        {
            Bitmap AnhVe;
            AnhVe = new Bitmap(sz.Width,sz.Height);
            Graphics g = Graphics.FromImage(AnhVe);
            Pen penBlack = new Pen(Color.Black);
            Brush bFill= new SolidBrush(Color.White);           
            g.FillRectangle(bFill,new Rectangle(new Point(0,0), sz));

            Pen penRed = new Pen(Color.Red,3);
            Brush b = new SolidBrush(Color.Green);


            Font font =new Font("Microsoft Sans Serif", 15F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(0)));
            double maxWidth ;
            double maxHeight ;
            double maxDulieu ;
            double maxHienThi ;

            tyLeHienThi=1;
            if (DieuChinhDuLieu)
            {
                maxWidth = DSThanhPho[0].ToaDo.X;
                maxHeight = DSThanhPho[0].ToaDo.Y;
                foreach (var tp2 in DSThanhPho)
                {
                    if (tp2.ToaDo.X > maxWidth)
                        maxWidth = tp2.ToaDo.X;
                    if (tp2.ToaDo.Y > maxHeight)
                        maxHeight = tp2.ToaDo.Y;
                }

                maxDulieu = Math.Max(maxWidth, maxHeight) + 1;
                maxHienThi = Math.Min(sz.Width, sz.Height);
                tyLeHienThi = maxHienThi / maxDulieu;
            }

            foreach (var tp2 in DSThanhPho)
            {
                g.DrawRectangle(penRed,(int)(tp2.ToaDo.X * tyLeHienThi)-5,(int)( tp2.ToaDo.Y * tyLeHienThi)-5, 10, 10);
                g.DrawString(tp2.ID.ToString(), font, b, (int)(tp2.ToaDo.X * tyLeHienThi)  , (int)(tp2.ToaDo.Y * tyLeHienThi)  );
            }
            foreach (var tp1 in DSThanhPho)
            {
                foreach (var tp2 in DSThanhPho)
                {
                    g.DrawLine(penBlack, ChuyenDoiTyLe(tp1.ToaDo,tyLeHienThi), ChuyenDoiTyLe(tp2.ToaDo,tyLeHienThi));
                }
            }
            g.Dispose();

            return AnhVe;
        }

        public static Bitmap Ve(Size sz, List<Point> DSToaDo, bool DieuChinhDuLieu)
        {
            Bitmap AnhVe;
            AnhVe = new Bitmap(sz.Width, sz.Height);
            Graphics g = Graphics.FromImage(AnhVe);
            Pen penBlack = new Pen(Color.Black);
            List<ThanhPho> DSThanhPho = new List<ThanhPho>();
            DSThanhPho = BanDo.KhoiTaoThanhPho(DSToaDo);
            return Ve(sz,DSThanhPho,DieuChinhDuLieu);
        }
        public static Bitmap VeHanhTrinh(Size sz, List<ThanhPho> DSToanThanhPho, List<ThanhPho> HanhTrinh, bool DieuChinhDuLieu)
        {
            Bitmap AnhVe = Ve(sz, DSToanThanhPho, DieuChinhDuLieu);
            if (HanhTrinh == null)
                return AnhVe;
            Graphics g = Graphics.FromImage(AnhVe);
            Pen penGreen = new Pen(Color.Red, 3);
            
            for (int i = 0; i < HanhTrinh.Count-1;i=i+1 )
            {
                g.DrawLine(penGreen, ChuyenDoiTyLe(HanhTrinh[i].ToaDo, tyLeHienThi), ChuyenDoiTyLe(HanhTrinh[i+1].ToaDo, tyLeHienThi));
            }
            g.Dispose();

            return AnhVe;

        }

       
         
        private static Point ChuyenDoiTyLe(Point ptGoc, double tyLe)
        {
            return new Point((int)(ptGoc.X*tyLe), (int)(ptGoc.Y*tyLe));
        }
    }
}
