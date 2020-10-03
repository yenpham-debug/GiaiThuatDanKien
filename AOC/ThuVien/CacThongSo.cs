using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOC.DoiTuong;

namespace AOC.ThuVien
{
    class CacThongSo
    {
        
        public static double ThamSoBayHoi = 0.2d;//p
        public static double AlPhal = 0.6d;//a
        public static double Belta = 0.4d;//b
        //public static int SoVongLap = 50;//NC
        public static int SoLuongKien = 6;//m
        public static double SacXuatChon = 0.3;//q

        public static double KCSieuLon = 100000000000;
        private static List<ThanhPho> HanhTrinhNN;//{get; private set;}
        public static List<ThanhPho> HanhTrinhTinhBangNN;
        public static double Snn ;
        public static double tMax =0;
        public static double tMin =0;
        
        public static Random BoSinhSoNgauNhien;

        public static void KhoiTao()
        {
             BoSinhSoNgauNhien = new Random();             
             tMax = (1.0 * SoLuongKien) / (ThamSoBayHoi * TimDuongDiNganNhatNN());//TimDuongDiNganNhatNN(); //1 / 
             tMin = tMax / (2 * BanDo.SoThanhPho);
        }
        public static double TimDuongDiNganNhatNN()
        {
            double DuongDiNN = 0;
            HanhTrinhNN = new List<ThanhPho>();
            HanhTrinhTinhBangNN = new List<ThanhPho>();
            HanhTrinhNN.Add(BanDo.DanhSachThanhPho[0]);
            ThanhPho GanNhat = BanDo.DanhSachThanhPho[1];
            ThanhPho TPHienTai=BanDo.DanhSachThanhPho[0];

            double KC=0;
            double KCNganNhat = KCSieuLon;//TPHienTai.KhoangCach(GanNhat);
            for (int i = 1; i < BanDo.SoThanhPho; i++)
            {
                KCNganNhat = KCSieuLon;
                for (int j = 0; j < BanDo.SoThanhPho; j++)
                {

                    if (i != j && !HanhTrinhNN.Contains(BanDo.DanhSachThanhPho[j]))
                    {
                        KC=BanDo.DanhSachThanhPho[j].KhoangCach(TPHienTai);
                        if (KC < KCNganNhat)
                        {
                            KCNganNhat = KC;
                            GanNhat = BanDo.DanhSachThanhPho[j];
                        }
                    }
                }

                TPHienTai = GanNhat;
                HanhTrinhNN.Add(TPHienTai);
                DuongDiNN += KCNganNhat;
            }
            DuongDiNN += TPHienTai.KhoangCach(BanDo.DanhSachThanhPho[0]);
            TPHienTai = BanDo.DanhSachThanhPho[0];
            HanhTrinhNN.Add(TPHienTai);
            
            Snn = DuongDiNN;
            HanhTrinhTinhBangNN = HanhTrinhNN;
            return DuongDiNN ;
        }
    }
}
