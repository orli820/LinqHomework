using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();

            this.productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
        }

        private void button5_Click(object sender, EventArgs e)             //找某年腳踏車
        {
            var q = from y in awDataSet1.ProductPhoto
                    where y.ModifiedDate.Year == Convert.ToInt32(comboBox3.Text)
                    select y;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void comboBox3_MouseDown(object sender, MouseEventArgs e)  //combo讀取年分
        {
            //var q = (from y in awDataSet1.ProductPhoto
                     //select new { Year = y.ModifiedDate.Year }).Distinct();
            var q = (from y in awDataSet1.ProductPhoto
                     orderby y.ModifiedDate.Year ascending
                     select  y.ModifiedDate.Year ).Distinct();
            comboBox3.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)            //全部腳踏車
        {
            var q = from y in awDataSet1.ProductPhoto
                    where y.ModifiedDate.Year !=null
                    select y;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)            //找季腳踏車
        {
            
                if (comboBox2.Text == "第一季")
                {
                    s1();
                }
                else if (comboBox2.Text == "第二季")
                {
                    s2();
                }
                else if (comboBox2.Text == "第三季")
                {
                    s3();
                }
                else if (comboBox2.Text == "第四季")
                {
                    s4();
                }
            
           
        }

        void s1()
        {
            var q = from y in awDataSet1.ProductPhoto
                    where (y.ModifiedDate.Year == Convert.ToInt32(comboBox3.Text) &
                    (y.ModifiedDate.Month == Convert.ToInt32("1") || y.ModifiedDate.Month == Convert.ToInt32("2") || y.ModifiedDate.Month == Convert.ToInt32("3")))
                    select y;
            this.dataGridView1.DataSource = q.ToList();
        }
        void s2()
        {
            var q = from y in awDataSet1.ProductPhoto
                    where (y.ModifiedDate.Year == Convert.ToInt32(comboBox3.Text) &
                    (y.ModifiedDate.Month == Convert.ToInt32("4") || y.ModifiedDate.Month == Convert.ToInt32("5") || y.ModifiedDate.Month == Convert.ToInt32("6")))
                    select y;
            this.dataGridView1.DataSource = q.ToList();
        }
        void s3()
        {
            var q = from y in awDataSet1.ProductPhoto
                    where (y.ModifiedDate.Year == Convert.ToInt32(comboBox3.Text) &
                    (y.ModifiedDate.Month == Convert.ToInt32("7") || y.ModifiedDate.Month == Convert.ToInt32("8") || y.ModifiedDate.Month == Convert.ToInt32("9")))
                    select y;
            this.dataGridView1.DataSource = q.ToList();
        }
        void s4()
        {
            var q = from y in awDataSet1.ProductPhoto
                    where (y.ModifiedDate.Year == Convert.ToInt32(comboBox3.Text) &
                    (y.ModifiedDate.Month == Convert.ToInt32("10") || y.ModifiedDate.Month == Convert.ToInt32("11") || y.ModifiedDate.Month == Convert.ToInt32("12")))
                    select y;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)            //找區間腳踏車
        {
            var q = from n in awDataSet1.ProductPhoto
                    where n.ModifiedDate >= dateTimePicker1.Value & n.ModifiedDate <= dateTimePicker2.Value
                    select n;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)  //超煩得找圖片不想做
        {
            byte[] ByteData = (byte[])dataGridView1.CurrentRow.Cells["LargePhoto"].Value;
            MemoryStream memoryStream = new MemoryStream(ByteData);
            pictureBox1.Image = Image.FromStream(memoryStream);
            memoryStream.Close();
        }

       
    }  
}
