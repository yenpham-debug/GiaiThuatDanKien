using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOC.ThuVien;

namespace AOC.DoiTuong
{
    class DanKien
    {
        public static double[,] MaTranMui { get; private set; }
        public List<ConKien> DSDanKien { get; private set; }
        public double QuangDuongNganNhat { get; private set; }
        public List<ThanhPho> HanhTrinhNganNhat { get; private set; }                
        public DanKien()
        {
            KhoiTaoMaTranMui();
            //DSDanKien = new List<ConKien>();
            QuangDuongNganNhat = CacThongSo.KCSieuLon*BanDo.SoThanhPho;//CacThongSo.Snn;//TimDuongDiNganNhatNN();
            HanhTrinhNganNhat = new List<ThanhPho>();//CacThongSo.HanhTrinhNN;//new List<ThanhPho>();
            //ConKien kien;
            //Random rand= new Random();
            //for (int i = 0; i < CacThongSo.SoLuongKien; i++)
            //{
            //    kien = new ConKien(BanDo.DanhSachThanhPho[rand.Next(0, BanDo.DanhSachThanhPho.Count - 1)],i);
            //    DSDanKien.Add(kien);
            //}
        }
        private void TaoDanKien()
        {
            DSDanKien = new List<ConKien>();
            ConKien kien;
            Random rand = new Random();
            for (int i = 0; i < CacThongSo.SoLuongKien; i++)
            {
                kien = new ConKien(BanDo.DanhSachThanhPho[rand.Next(0, BanDo.DanhSachThanhPho.Count - 1)], i);
                DSDanKien.Add(kien);
            }

        }
        public void KhoiTaoMaTranMui()
        {
            MaTranMui = new double[BanDo.SoThanhPho, BanDo.SoThanhPho];
            CacThongSo.KhoiTao();
            for( int i=0;i< BanDo.SoThanhPho;i++ )
                for (int j = 0; j < BanDo.SoThanhPho; j++)
                {
                    MaTranMui[i, j] = CacThongSo.tMax;
                }
        }
        public int VongLapHienTai = 0;
        private volatile bool DungLai=false;
        public void YeuCauDung()
        {
            DungLai = true;
        }
        public void ChayDanKien()
        {

            //ConKien conKienTimDuongNganNhat;;// = DSDanKien[0];
            //QuangDuongNganNhat = conKienTimDuongNganNhat.TongDuongDi;
            //string Hanh = "";
            double preNN = Double.MaxValue;
            int countKoThayDoi = 100;
            while (!DungLai)
            {
                VongLapHienTai++;
                TaoDanKien();
                //conKienTimDuongNganNhat= DSDanKien[0];
                for (int i = 0; i < CacThongSo.SoLuongKien; i++)
                {
                    DSDanKien[i].TimDuong();
                    if (DSDanKien[i].TongDuongDi < QuangDuongNganNhat)
                    {
                        QuangDuongNganNhat = DSDanKien[i].TongDuongDi;
                        HanhTrinhNganNhat = DSDanKien[i].HanhTrinh;
                        //conKienTimDuongNganNhat = DSDanKien[i];
                        //Hanh = "";
                        //foreach (var tp in HanhTrinhNganNhat)
                        //{
                        //    Hanh += tp.ID + "->";
                        //}
                    }
                }
               // HanhTrinhNganNhat = //conKienTimDuongNganNhat.HanhTrinh;
                
                CapNhatMui( QuangDuongNganNhat, HanhTrinhNganNhat);
                if (QuangDuongNganNhat < preNN)
                {
                    preNN = QuangDuongNganNhat;
                }
                else
                {
                    countKoThayDoi--;
                    if (countKoThayDoi == 0)
                    {
                        DungLai = true;
                    }

                }
            }
        }
        public void CapNhatMui(double TongDuongDi, List<ThanhPho> HanhTrinh)//ConKien conKienTotNhat)
        {
            for(int i=0;i< BanDo.SoThanhPho;i++)
                for (int j = 0; j < BanDo.SoThanhPho; j++)
                {
                    MaTranMui[i,j]=MaTranMui[i,j]*(1-CacThongSo.ThamSoBayHoi);
                }
            double delta = 1 * 1.0 /  TongDuongDi;
            double Tij=0;

            for (int i = 0; i < HanhTrinh.Count-1; i++)
            {
                Tij=MaTranMui[ HanhTrinh[i].ID,  HanhTrinh[i + 1].ID]+ delta;
                if (Tij < CacThongSo.tMin)
                    Tij = CacThongSo.tMin;
                if (Tij > CacThongSo.tMax)
                    Tij = CacThongSo.tMax;
                MaTranMui[ HanhTrinh[i].ID,  HanhTrinh[i + 1].ID] = Tij;
                MaTranMui[ HanhTrinh[i + 1].ID , HanhTrinh[i].ID] = Tij;
            }
        }


      
    }
}
