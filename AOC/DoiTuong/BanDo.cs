using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOC.ThuVien;
using System.Drawing;

namespace AOC.DoiTuong
{
    class BanDo
    {
        public static List<ThanhPho> DanhSachThanhPho { get; private set; }
        public static List<Point> DSToaDoThanhPho { get; private set; }
       // static int _soThanhPho;

        public static int SoThanhPho
        {
            get { return DanhSachThanhPho.Count; }
           // private set { _soThanhPho = value; }
        }
        public static double[,] MaTranKhoangCach { get; private set; }
       // public double Cnn { get; private set; }
        public static void KhoiTao(List<Point> MangToaDo)
        {

            DanhSachThanhPho = KhoiTaoThanhPho(MangToaDo) ;
            DSToaDoThanhPho = MangToaDo;
            KhoiTaoKhoangCach();

        }
        public static void KhoiTao(string DuongDanFileTxt)
        {
            DSToaDoThanhPho = DocFileTxt.LayToaDo(DuongDanFileTxt);
            DanhSachThanhPho = KhoiTaoThanhPho(DSToaDoThanhPho);
            KhoiTaoKhoangCach();
        }
        private static void KhoiTaoKhoangCach()
        {
            MaTranKhoangCach = new double[SoThanhPho, SoThanhPho];
            for (int i = 0; i < SoThanhPho; i++)
            {
                for (int j = 0; j < SoThanhPho; j++)
                {
                    if (i == j)
                        MaTranKhoangCach[i, j] = 0d;
                    else
                    {
                        MaTranKhoangCach[i, j] = DanhSachThanhPho[i].KhoangCach(DanhSachThanhPho[j]);
                    }
                }
            }
        }
        public static List<ThanhPho> KhoiTaoThanhPho(List<Point> MangToaDo)
        {
            List<ThanhPho> KetQua = new List<ThanhPho>();
            int i = 0;
            ThanhPho tp;
            foreach (var pt in MangToaDo)
            {
                tp= new ThanhPho(pt,i.ToString(),i);
                KetQua.Add(tp);
                i++;
            }
            return KetQua;
        }
        public static double TyLeHaiThanhPho(int i, int j) // (Tij)^a*(Nij)^b
        {
            double Nij= 1/(BanDo.MaTranKhoangCach[i,j]);
            return Math.Pow(DanKien.MaTranMui[i, j], CacThongSo.AlPhal) * Math.Pow(Nij, CacThongSo.Belta);
        }
    }
}
