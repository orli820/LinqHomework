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

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();

            Random rd = new Random();

            students_scores = new List<Student>()
                                         {
                                            new Student { Name = "廖家毅",  Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Male" },
                                            new Student { Name = "鄭凱",    Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Male" },
                                            new Student { Name = "李沛軒",  Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Male" },
                                            new Student { Name = "陳苡錚",  Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                            new Student { Name = "王婷薇",  Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                            new Student { Name = "洪暐婷",  Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                            new Student { Name = "游曉雯",  Chi = rd.Next(101), Eng = rd.Next(101), Math = rd.Next(101), Gender = "Female" },
                                          };

        }

        List<Student> students_scores;

       

        // split=> 數學成績 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from s in students_scores
                    select new
                    {
                        姓名 = s.Name,
                        國文 = s.Chi,
                        英文 = s.Eng,
                        數學 = s.Math,
                        總分 = s.Sum,
                        平均 = s.Avg,
                        最高分 = Math.Max(Math.Max(s.Chi, s.Eng), s.Math),
                        最低分 = Math.Min(Math.Min(s.Chi, s.Eng), s.Math)
                    };

            this.dataGridView1.DataSource = q.ToList();

            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "姓名";
            this.chart1.Series[0].YValueMembers = "國文";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            this.chart1.Series[1].XValueMember = "姓名";
            this.chart1.Series[1].YValueMembers = "英文";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            this.chart1.Series[2].XValueMember = "姓名";
            this.chart1.Series[2].YValueMembers = "數學";
            this.chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            this.chart1.Series[3].XValueMember = "姓名";
            this.chart1.Series[3].YValueMembers = "總分";
            this.chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chart1.Series[3].Color = Color.BlueViolet;
            this.chart1.Series[3].BorderWidth = 2;

            this.chart1.Series[4].XValueMember = "姓名";
            this.chart1.Series[4].YValueMembers = "平均";
            this.chart1.Series[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chart1.Series[4].Color = Color.Green;
            this.chart1.Series[4].BorderWidth = 2;

            //group s by Mykey((s.Chi +s.Eng + s.Math) / 3) into g
            //foreach (var group in q)
            //{
            //    string s = $"{group.國文}({group.英文})";
            //    TreeNode x = this.treeView1.Nodes.Add(s/*group.Mykey.ToString()*/);
            //    foreach (var item in group.mygroup)
            //    {
            //        x.Nodes.Add(item.ToString());
            //    }
            //}

        }

        private string Mykey(int n)
        {
            if (n < 60)
                return "待加強";
            else if (n < 89)
                return "佳";
            else
                return "優良";
        }

        private void button4_Click(object sender, EventArgs e)          //國文
        {
            var q = from s in students_scores group s by Mykey(s.Chi) into g select new { 標準 = g.Key, 人數 = g.Count() };
            var q1 = from s in students_scores
                    group s by Mykey(s.Chi) into g
                    select new
                    {
                        標準 = g.Key,
                        人數 = g.Count(),
                        mygroup = g                 //你分了三群 這三群 每一群 包括了 
                        //最高分=g.Max(s => s.Chi),
                        //最低分=g.Min(s => s.Chi)
                    };

            this.dataGridView2.DataSource = q.ToList();

            this.treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string s = $"{group.標準}({group.人數})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.mygroup)
                {
                    string y = $"{item.Name}({item.Chi})";  //包含了item in group.mygroup =  group s by into g <==g
                    x.Nodes.Add(y);

                }

                this.chart1.DataSource = q.ToList();
                this.chart1.Series[0].XValueMember = "標準";
                this.chart1.Series[0].YValueMembers = "人數";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
        }
        private void button3_Click(object sender, EventArgs e)          //英文
        {
            var q = from s in students_scores group s by Mykey(s.Chi) into g select new { 標準 = g.Key, 人數 = g.Count() };
            var q1 = from s in students_scores
                    group s by Mykey(s.Eng) into g
                    select new
                    {
                        標準 = g.Key,
                        人數 = g.Count(),
                        mygroup = g
                    };

            this.dataGridView2.DataSource = q.ToList();

            this.treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string s = $"{group.標準}({group.人數})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.mygroup)
                {
                    string y = $"{item.Name}({item.Eng})";
                    x.Nodes.Add(y);

                }

                this.chart1.DataSource = q.ToList();
                this.chart1.Series[0].XValueMember = "標準";
                this.chart1.Series[0].YValueMembers = "人數";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
        }
        private void button2_Click(object sender, EventArgs e)          //數學
        {
            var q = from s in students_scores group s by Mykey(s.Chi) into g select new { 標準 = g.Key, 人數 = g.Count() };
            var q1 = from s in students_scores
                    group s by Mykey(s.Math) into g
                    select new
                    {
                        標準 = g.Key,
                        人數 = g.Count(),
                        mygroup = g,
                    };

            this.dataGridView1.DataSource = q.ToList();

            this.treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string s = $"{group.標準}({group.人數})";
                TreeNode x = this.treeView1.Nodes.Add(s);
                foreach (var item in group.mygroup)
                {
                    string y = $"{item.Name}({item.Math})";
                    x.Nodes.Add(y);

                }
                
                this.chart1.DataSource = q.ToList();
                this.chart1.Series[0].XValueMember = "標準";
                this.chart1.Series[0].YValueMembers = "人數";
                this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)  //選學生
        {
            var q = from y in students_scores
                    orderby y.Name ascending
                    select y.Name;
            comboBox1.DataSource = q.ToList();


        }

        private void button5_Click(object sender, EventArgs e)            //各科成績
        {
            chart1.Series.Clear();
            if (comboBox2.Text == "Chi")
            {
                //if (comboBox2.Text == "") { return; }
                var q = this.students_scores.Where(s => s.Chi >= 0).Select(p => new { p.Name, p.Chi });
                this.dataGridView2.DataSource = q.ToList();


                this.chart1.DataSource = q.ToList();
                string obj = comboBox2.Text;
                this.chart1.Series.Add(obj);
                this.chart1.Series[0].XValueMember = "Name";
                this.chart1.Series[0].YValueMembers = "Chi";
            }
            else if (comboBox2.Text == "Eng")
            {

                var q = this.students_scores.Where(s => s.Eng >= 0).Select(p => new { p.Name, p.Eng });
                this.dataGridView2.DataSource = q.ToList();


                this.chart1.DataSource = q.ToList();
                string obj = comboBox2.Text;
                this.chart1.Series.Add(obj);
                this.chart1.Series[0].XValueMember = "Name";
                this.chart1.Series[0].YValueMembers = "Eng";
            }
            else if (comboBox2.Text == "Math")
                {
                    var q = this.students_scores.Where(s => s.Math >= 0).Select(p => new { p.Name, p.Math });
                    this.dataGridView2.DataSource = q.ToList();


                    this.chart1.DataSource = q.ToList();
                    string obj = comboBox2.Text;
                    this.chart1.Series.Add(obj);
                    this.chart1.Series[0].XValueMember = "Name";
                    this.chart1.Series[0].YValueMembers = "Math";
                }
            }

        private void button37_Click(object sender, EventArgs e)         //個人成績
        {
           

            var q = from y in students_scores
                    where y.Name == (comboBox1.Text)
                    select /*y;*/

                    new { Name=y.Name, Chi = y.Chi, Eng = y.Eng, y.Math, Sum = y.Sum, Avg = y.Avg, 
                        Max = Math.Max(Math.Max(y.Chi, y.Eng), y.Math), Min = Math.Min(Math.Min(y.Chi, y.Eng), y.Math) };

            this.dataGridView2.DataSource = q.ToList();


            this.chart1.Series.Clear();
            this.chart1.DataSource = q.ToList();

            //this.chart1.Series[0].


            this.chart1.DataSource = q.ToList();
            string obj = comboBox1.Text;
            //string a = "Chi";
            //string b = "Eng";
            //string c = "Math";
            this.chart1.Series.Add(obj);


            //this.chart1.Series[0].AxisLabel = a;
            //this.chart1.Series[0].YValueMembers = "Chi";

            //this.chart1.Series[1].AxisLabel = b;
            //this.chart1.Series[1].YValueMembers = "Eng";
            //this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            foreach (var a in q)
            {
                this.chart1.Series[obj].Points.AddXY("Chi", a.Chi);
                this.chart1.Series[obj].Points.AddXY("Eng", a.Eng);
                this.chart1.Series[obj].Points.AddXY("Math", a.Math);
            }
        }
        
    }
}
