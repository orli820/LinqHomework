using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqLabs.作業;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(nDataSet11.Orders);
            this.order_DetailsTableAdapter1.Fill(nDataSet11.Order_Details);
            this.productsTableAdapter2.Fill(nDataSet11.Products);

            
            Random rd = new Random();

            students_scores = new List<Students>()
                                         { 
                                            new Students { Name = "廖家毅", Class = "CS_101", Chi =rd.Next(101), Eng =rd.Next(101), Math =rd.Next(101), Gender = "Male" },
                                            new Students { Name = "鄭凱", Class = "CS_102", Chi =rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Male" },
                                            new Students { Name = "李沛軒", Class = "CS_102", Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Male" },
                                            new Students { Name = "陳苡錚", Class = "CS_101", Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                            new Students { Name = "王婷薇", Class = "CS_102", Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                            new Students { Name = "洪暐婷", Class = "CS_101", Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                            new Students { Name = "游曉雯", Class = "CS_102", Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                          };
        }

       

        List<Students> students_scores;
        //private int button12_Click;


        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        int page = 0;
        //int count;
        private void button12_Click_1(object sender, EventArgs e)//上一頁
        {
            //if (count - 1 <= 0)
            //    return;
            //count--;
            //var h1 = from y in this.nDataSet11.Products.Take(count * (Convert.ToInt32(textBox1.Text))).Skip(10 * (count - 1))
            //         select y;


            //this.dataGridView2.DataSource = h1.ToList();

            this.dataGridView2.DataSource= this.nDataSet11.Products.Skip(10 * page).Take(10).ToList();

            page -= 1;
        }

        private void button13_Click(object sender, EventArgs e)  //下一頁
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10+(10*click.count)) 

            //Distinct()

            //======================================================================

            //count++;
            //var h = from y in this.nDataSet11.Products.Take(count * (Convert.ToInt32(textBox1.Text))).Skip(10 * (count - 1))
            //        select y;


            //this.dataGridView2.DataSource = h.ToList();

            this.dataGridView2.DataSource = this.nDataSet11.Products.Skip(10 * page).Take(10).ToList();

            page += 1;
        }

        private void button14_Click(object sender, EventArgs e)  //找log
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files;

            var d = from L in files
                    where L.Name.Contains(".log")
                    select L;

            dataGridView1.DataSource = d.ToList();
        }

        private void button2_Click(object sender, EventArgs e)   //找2017
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var y = from yr in files
                    where yr.CreationTime.Year >= 2017
                    orderby yr.CreationTime.Year ascending
                    select yr;

            dataGridView1.DataSource = y.ToList();
        }

        private void button4_Click(object sender, EventArgs e)   //找大檔
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var b = from p in files
                    where p.Length > 10000
                    orderby p.Length descending
                    select p;

            dataGridView1.DataSource = b.ToList();
        }

        private void button6_Click(object sender, EventArgs e)   //找所有訂單
        {
            var o = from a in this.nDataSet11.Orders
                    where a.OrderID >= 1 /*&& a.ShippedDate<=DateTime.Now*/
                    //不寫條件會出來全部
                    //會一直跳出視窗因為它會顯示空值
                    select a;

            this.dataGridView1.DataSource = o.ToList();

            var d = from a in this.nDataSet11.Order_Details
                    where a.OrderID >= 1
                    select a;
            this.dataGridView2.DataSource = d.ToList();
        }

        private void button1_Click(object sender, EventArgs e)   //找年份訂單
        {
            var h = from y in this.nDataSet11.Orders
                    where y.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                    select y;
            this.dataGridView1.DataSource = h.ToList();

            var g = from oy in this.nDataSet11.Order_Details
                    join y in this.nDataSet11.Orders
                    on oy.OrderID equals y.OrderID
                    where y.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                    select oy;

            this.dataGridView2.DataSource = g.ToList();
        }

        private void button36_Click(object sender, EventArgs e)  // 共幾個 學員成績 ?	
        {
            this.dataGridView2.DataSource = students_scores;
            var q = from s in students_scores
                    select s;
            this.dataGridView2.DataSource = q.ToList();
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?	

            // 找出 前面三個 的學員所有科目成績					
            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	


            // 數學不及格 ... 是誰 
            #endregion

        }

        private void button3_Click(object sender, EventArgs e)   // 找出 前面三個 的學員所有科目成績		
        {
            this.dataGridView2.DataSource = students_scores;
            var q = from s in students_scores.Take(3)
                    select s;
            this.dataGridView2.DataSource = q.ToList();

        }

        private void button5_Click(object sender, EventArgs e)   //找出 後面兩個 的學員所有科目成績		
        {
            this.dataGridView2.DataSource = students_scores;
            var q = from s in students_scores.Skip(4)
                    select s;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)   // 找出 Name 'aaa','bbb','ccc' 的學成績		
        {
            this.dataGridView2.DataSource = students_scores;
            var q = from s in students_scores
                    where s.Name == "廖家毅" || s.Name == "李沛軒" || s.Name == "王婷薇"
                    select s;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)   // 找出學員 'bbb' 的成績
        {
            this.dataGridView2.DataSource = students_scores;
            var q = from s in students_scores
                    where s.Name == "洪暐婷"
                    select s;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)   // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
        {
            this.dataGridView2.DataSource = students_scores;
            var q = from s in students_scores
                    where s.Name != "陳苡錚"
                    select s;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)  // 數學不及格 ... 是誰 
        {
            this.dataGridView2.DataSource = students_scores;
            var q = from s in students_scores
                    where s.Math < 60
                    select s;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button37_Click(object sender, EventArgs e)  //每個人個人成績
        {
            Students st = new Students();
            this.dataGridView2.DataSource = students_scores;
            var q = students_scores.Where(s => s.Math > 0)
                .Select(s => new { s.Name, 總分 = (s.Chi + s.Eng + s.Math),
                    平均 = (s.Chi+s.Eng+s.Math)/3, s.Chi, s.Eng, s.Math ,
                    最高分 = Math.Max(Math.Max(s.Chi, s.Eng), s.Math),
                    最低分=Math.Min(Math.Min(s.Chi,s.Eng),s.Math)
                });
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button11_Click(object sender, EventArgs e)  //aaa,bbb,ccc國文數學成績
        {
            this.dataGridView2.DataSource = students_scores;
            var q = students_scores.Where(s => s.Name == "王婷薇" || s.Name == "洪暐婷" || s.Name == "游曉雯")
                .Select(s => new { s.Name, s.Chi, s.Math });
            this.dataGridView2.DataSource = q.ToList();
        }
        
    }
}