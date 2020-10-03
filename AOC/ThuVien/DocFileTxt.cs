using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace AOC.ThuVien
{
    class DocFileTxt
    {
        private static string LayChuTuFile(string duongDan)
        {

            FileStream fs = new FileStream(duongDan, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string str;
            //doc tat ca du lieu trong file luu vao str;
            str = sr.ReadToEnd();
            //set text cua textbox1 = str;
            
            sr.Close();
            fs.Close();
            return str;
        }
        public static List<Point> LayToaDo(string path)
        {
            string str = LayChuTuFile(path);
            str = str.Trim();
            str = str.Replace('\n', ' ');
            str = str.Replace("  ", " ");
            string[] MangStr = str.Split(new char[] {' '});
            List<Point> KetQua = new List<Point>();
            Point pt;
            if (MangStr.Length % 2 == 1)
            {
                System.Windows.Forms.MessageBox.Show("File dữ liệu bị lỗi. kiểm tra lại các tọa độ");
                return null;
            }
            try
            {
                for (int i = 0; i < MangStr.Length; i = i + 2)
                {
                    pt = new Point(int.Parse(MangStr[i]), int.Parse(MangStr[i + 1]));
                    KetQua.Add(pt);
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return null;
            }
            return KetQua;
        }
    }
}
