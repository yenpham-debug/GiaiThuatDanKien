using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AOC.ThuVien;
using AOC.DoiTuong;
using System.Threading;

namespace AOC.BieuMau
{
    public partial class frmChinh : Form
    {
        public frmChinh()
        {
            InitializeComponent();
        }
        
            
         
        DanKien danKien;
        List<Point> MangToaDoThanhPho;
        Thread thread;
        private void frmChinh_Load(object sender, EventArgs e)
        {
            LayCacThongSo();
            MangToaDoThanhPho = new List<Point>();
        }

        public void LayCacThongSo()
        {
            numTocDoBayHoi.Value =(decimal) CacThongSo.ThamSoBayHoi;
            numAlphal.Value = (decimal)CacThongSo.AlPhal;
            numBelta.Value = (decimal)CacThongSo.Belta;
           // numSoVongLap.Value = (decimal)CacThongSo.SoVongLap;
            numSoKien.Value = (decimal)CacThongSo.SoLuongKien;
           numXacXuatChon.Value = (decimal)CacThongSo.SacXuatChon;

        }
        public void CaiCacThongSo()
        {
            CacThongSo.ThamSoBayHoi= (double)numTocDoBayHoi.Value ;
            CacThongSo.AlPhal = (double)numAlphal.Value;
            CacThongSo.Belta = (double)numBelta.Value;
            //CacThongSo.SoVongLap=(int)numSoVongLap.Value  ;
            CacThongSo.SoLuongKien=(int)numSoKien.Value  ;
            CacThongSo.SacXuatChon = (double)numXacXuatChon.Value;

        }
        ThreadStart threadStart;
        private void btnChay_Click(object sender, EventArgs e)
        {
            CaiCacThongSo();
            KhoiTaoACO();
            timDongHo.Enabled = true;
            //if (thread == null)
            //{
                threadStart = new ThreadStart(TimDuong);
                thread = new Thread(threadStart);//new ThreadStart(TimDuong));
                thread.Start();
            //}
            //else
            //{
            //    thread.Join();
            //}
           // while (!thread.IsAlive) ;
            btnChay.Enabled = false;
            btnDung.Enabled = true;
        }
               
        private void btnLayFile_Click(object sender, EventArgs e)
        {
            if (ofdMoFile.ShowDialog() == DialogResult.OK)
            {
                txtFileDuLieu.Text = ofdMoFile.FileName;
                ptbBanDo.Image = XuLyAnh.Ve(ptbBanDo.Size, DocFileTxt.LayToaDo(txtFileDuLieu.Text),true);
            }
        }
        private void KhoiTaoACO()
        {
            if (ckbSuDungFile.Checked)
            {
                BanDo.KhoiTao(txtFileDuLieu.Text);
                danKien = new DanKien();
                MangToaDoThanhPho = BanDo.DSToaDoThanhPho;
            }
            else
            {
                BanDo.KhoiTao(MangToaDoThanhPho);
                danKien = new DanKien();
            }
        }
        private void TimDuong()
        {
            danKien.ChayDanKien();
        }
        
        private void timDongHo_Tick(object sender, EventArgs e)
        {
            if (danKien == null)
            {
                //this.timDongHo.Enabled = false;
                return;
            }
            
            txtQuangDuongNgan.Text = danKien.QuangDuongNganNhat.ToString();
            txtVongLap.Text = danKien.VongLapHienTai.ToString();

            string HanhTrinhACO = "";
            foreach (var tp in danKien.HanhTrinhNganNhat)
            {
                HanhTrinhACO += "->"+ tp.Ten;
            }
            txtHanhTrinhACO.Text = HanhTrinhACO;


            txtCnn.Text = CacThongSo.Snn.ToString() ;
            string HanhTrinhTinhBangNN= "";
            foreach (var tp in CacThongSo.HanhTrinhTinhBangNN)
            {
                HanhTrinhTinhBangNN += "->" + tp.Ten;
            }
            txtHanhTrinhTinhBangNN.Text = HanhTrinhTinhBangNN;




            ptbBanDo.Image = XuLyAnh.VeHanhTrinh(ptbBanDo.Size,BanDo.DanhSachThanhPho,danKien.HanhTrinhNganNhat, ckbSuDungFile.Checked);
            //if (danKien.VongLapHienTai == CacThongSo.SoVongLap)
            //{
            //    timDongHo.Enabled = false;
            //}
        }

        private void ckSuDungFile_CheckedChanged(object sender, EventArgs e)
        {
            btnLayFile.Enabled = ckbSuDungFile.Checked;
            MangToaDoThanhPho = new List<Point>();
        }
        
        private void ptbBanDo_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ckbSuDungFile.Checked)
            {
                Point p = new Point(e.X, e.Y);
                MangToaDoThanhPho.Add(p);
                ptbBanDo.Image = XuLyAnh.Ve(ptbBanDo.Size, MangToaDoThanhPho,false);
            }
        }

        private void btnDung_Click(object sender, EventArgs e)
        {
            
            //thread.Abort();
            danKien.YeuCauDung();
            MangToaDoThanhPho.Clear();
            timDongHo.Enabled = false;
            btnChay.Enabled = true;
            btnDung.Enabled = false;
        }

         

        
    }
}
