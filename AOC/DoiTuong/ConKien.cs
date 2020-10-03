using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOC.ThuVien;

namespace AOC.DoiTuong
{
    class ConKien
    {
        public double TongDuongDi{get; private set;}
        public List<ThanhPho> HanhTrinh { get; private set; }
        List<ThanhPho> DSThanhPhoChuaQua;
        public int ID { get; private set; } 
        private ThanhPho ThanhPhoXuatPhat,ThanhPhoHienTai;

        public ConKien(ThanhPho tpXuatPhat, int id)
        {
            DSThanhPhoChuaQua = new List<ThanhPho>();
            ThanhPhoXuatPhat = tpXuatPhat;
            ThanhPhoHienTai = tpXuatPhat;
            TongDuongDi = 0;
            HanhTrinh = new List<ThanhPho>();
            HanhTrinh.Add(tpXuatPhat);
            ID = id;            
        }
        private void LayDSThanhPhoChuaToi()
        {
            List<ThanhPho> DSTP = new List<ThanhPho>();
            foreach (var tp in BanDo.DanhSachThanhPho)
            {
                if (!HanhTrinh.Contains(tp))
                {
                    DSTP.Add(tp);
                }
            }
            DSThanhPhoChuaQua= DSTP;
        }

        private void TimThanhPhoTiepTheo()
        {
            ThanhPho TPTiepTheo;

            try
            {
                if (HanhTrinh.Count == BanDo.SoThanhPho)
                {
                    TPTiepTheo = ThanhPhoXuatPhat;
                    // het thanh pho , tro ve thoi

                }
                else
                {
                    double q = CacThongSo.BoSinhSoNgauNhien.NextDouble();
                    LayDSThanhPhoChuaToi();
                    if (q < CacThongSo.SacXuatChon)
                    {

                        double Nij = 0;// ThanhPhoHienTai.KhoangCach(DSThanhPhoChuaQua[0]);
                        double pMax = 0;// DanKien.MaTranMui[ThanhPhoHienTai.ID, DSThanhPhoChuaQua[0].ID] * Math.Pow(Nij, CacThongSo.Belta);
                        double pij = 0;
                        int indexMax = 0;
                        for (int i = 0; i < DSThanhPhoChuaQua.Count; i++)
                        {
                            Nij = ThanhPhoHienTai.KhoangCach(DSThanhPhoChuaQua[i]);
                            pij = DanKien.MaTranMui[ThanhPhoHienTai.ID, DSThanhPhoChuaQua[i].ID] * Math.Pow(Nij, CacThongSo.Belta);
                            if (i == 0)
                                pMax = pij;
                            else
                            {
                                if (pij > pMax)
                                {
                                    pMax = pij;
                                    indexMax = i;
                                }
                            }
                        }

                        TPTiepTheo = DSThanhPhoChuaQua[indexMax];
                    }
                    else
                    {
                        //double prob = Math.random() * sumProb;
                        //j = 0;
                        //double p = selectionProbability[j];
                        //while (p < prob)
                        //{
                        //    j++;
                        //    p += selectionProbability[j];
                        //}
                        double TongXacXuat = 0;
                        foreach (var tp in DSThanhPhoChuaQua)
                        {
                            TongXacXuat += XacXuatChonDinhTiepTheo(tp.ID);
                        }

                        int indexChon = 0;
                        double prob = CacThongSo.BoSinhSoNgauNhien.NextDouble() * TongXacXuat;
                        double p = XacXuatChonDinhTiepTheo(DSThanhPhoChuaQua[indexChon].ID);
                        while (p > prob && indexChon < DSThanhPhoChuaQua.Count - 1)
                        {
                            indexChon++;
                            p += XacXuatChonDinhTiepTheo(DSThanhPhoChuaQua[indexChon].ID);
                        }

                        TPTiepTheo = DSThanhPhoChuaQua[(indexChon < DSThanhPhoChuaQua.Count ? indexChon : indexChon - 1)];
                    }
                }
                HanhTrinh.Add(TPTiepTheo);
                TongDuongDi += ThanhPhoHienTai.KhoangCach(TPTiepTheo);
                ThanhPhoHienTai = HanhTrinh[HanhTrinh.Count - 1];
            }
            catch (Exception ex) 
            {

                throw ex;
            }
            
        }

        public void TimDuong()
        {
            for (int i = 0; i < BanDo.SoThanhPho; i++)
            {
                TimThanhPhoTiepTheo();
            }
        }

        private double XacXuatChonDinhTiepTheo( int j)
        {
            try
            {
                double TuSo = BanDo.TyLeHaiThanhPho(ThanhPhoHienTai.ID, j);
                double MauSo = 0;
                foreach (var tp in DSThanhPhoChuaQua)
                {
                    MauSo += BanDo.TyLeHaiThanhPho(ThanhPhoHienTai.ID, tp.ID);
                }
                return TuSo / MauSo;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
    }
}
