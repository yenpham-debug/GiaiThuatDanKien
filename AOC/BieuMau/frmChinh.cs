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
using System.IO;

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
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            StringBuilder sb = new StringBuilder();
            // Calculate S for fileName
            foreach(ThanhPho tp in danKien.HanhTrinhNganNhat)
            {
                if (!string.IsNullOrEmpty(sb.ToString()))
                    sb.Append("_");

                sb.Append(tp.Ten);
            }

            saveFileDialog.FileName = string.Format("{0}_{1}.txt", sb.ToString(), danKien.QuangDuongNganNhat);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (Point s in MangToaDoThanhPho)
                        {
                            sw.WriteLine(string.Format("{0} {1}", s.X, s.Y));
                        }
                    }

                    MessageBox.Show("Save file succees ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }

        }

        private void ChayThuatToan()
        {
            timDongHo.Enabled = true;
            //if (thread == null)
            //{
            threadStart = new ThreadStart(TimDuong);
            thread = new Thread(threadStart);//new ThreadStart(TimDuong));
            thread.IsBackground = true;
            thread.Start();

            //}
            //else
            //{
            //    thread.Join();
            //}
            // while (!thread.IsAlive) ;
        }

        private void btnChay_Click(object sender, EventArgs e)
        {
            BanDo.IsChange = false;
            if (btnChay.Text == "Run")
            {
                CaiCacThongSo();
                if (!KhoiTaoACO())
                {
                    return;
                }
                ChayThuatToan();
                btnDung.Enabled = true;
                btnChay.Text = "Reset";
                btnDung.Text = "Stop";
                groupBox1.Enabled = false;
            }
            else
            {
                
                Reset();
                
                ptbBanDo.Image = null;
                timDongHo.Enabled = false;
                btnDung.Enabled = false;
                btnDung.Text = "Continue";
                btnExport.Enabled = false;
                groupBox1.Enabled = true;
                btnChay.Text = "Run";
            }

        }

        private void Reset()
        {
            danKien.YeuCauDung();
            BanDo.Reset();
            danKien.Reset();
            timDongHo.Enabled = false;
            MangToaDoThanhPho.Clear();
        }

        private void btnDung_Click(object sender, EventArgs e)
        {
            try
            {
                //thread.Abort();
                if (btnDung.Text == "Stop")
                {
                    danKien.YeuCauDung();
                    btnDung.Text = "Continue";
                    btnExport.Enabled = true;
                    timDongHo.Enabled = false;
                    MessageBox.Show("Số vòng lặp tốt nhất là: " + danKien.SoVongLapTotNhat, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    danKien.YeuCauTiepTuc();
                    timDongHo.Enabled = true;
                    btnDung.Text = "Stop";
                    if (BanDo.IsChange)
                    {
                        KhoiTaoBanDo();
                        danKien = new DanKien();
                    }

                    ChayThuatToan();

                    btnExport.Enabled = false;
                    BanDo.IsChange = false;
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void btnLayFile_Click(object sender, EventArgs e)
        {
            if (ofdMoFile.ShowDialog() == DialogResult.OK)
            {
                txtFileDuLieu.Text = ofdMoFile.FileName;
                ptbBanDo.Image = XuLyAnh.Ve(ptbBanDo.Size, DocFileTxt.LayToaDo(txtFileDuLieu.Text),true);
            }
        }

        private void KhoiTaoBanDo()
        {
            if (ckbSuDungFile.Checked)
            {
                BanDo.KhoiTao(txtFileDuLieu.Text);
                MangToaDoThanhPho = BanDo.DSToaDoThanhPho;
            }
            else
            {
                BanDo.KhoiTao(MangToaDoThanhPho);
            }
        }

        private bool KhoiTaoACO()
        {
            KhoiTaoBanDo();

            if (MangToaDoThanhPho.Count < 2)
            {
                MessageBox.Show("Quãng đường đi phải có ít nhất 2 điểm", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            danKien = new DanKien();
            return true;
                
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
            if (ckbSuDungFile.Checked)
            {
                MessageBox.Show("Chế độ đọc từ File ko hỗ trợ thêm điểm!"
                        , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnDung.Text == "Stop")
            {
                MessageBox.Show("Chương Trình đang chạy, Phải Stop nó mới thêm điểm mới được!"
                    , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Point p = new Point(e.X, e.Y);
            MangToaDoThanhPho.Add(p);
            ptbBanDo.Image = XuLyAnh.Ve(ptbBanDo.Size, MangToaDoThanhPho, false);
            BanDo.IsChange = true;
        }
    }
}
