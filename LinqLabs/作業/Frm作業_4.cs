using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();

            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
        }

        Random rd = new Random();
        private void button4_Click(object sender, EventArgs e)   //陣列分類
        {
            int[] nums = { rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), 
                           rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101)};
            //string[] name = { "A", "B", "C", "D", "E", "F", "G","H","I","J","K","L","M","N","O","P","Q","R", "S" ,"T"};
            var q = from n in nums orderby n ascending select n;
            var q2 = from n in nums group n by ad(n) into r orderby r.Key select new { Key = r.Key, Count = r.Count(),Avg=$"{r.Average():n2}" };
            var q1 = from n in nums group n by ad(n) into r orderby r.Key select new { Mykey = r.Key, MyCount = r.Count(), MyGroup = r };
            //var q3 = from m in name select m;
            
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
            //dataGridView1.DataSource = q.ToList();
            dataGridView1.DataSource = q2.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string t = $"{group.Mykey}({group.MyCount})";
                TreeNode x = treeView1.Nodes.Add(t);
                foreach (var item in group.MyGroup)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }

        private string ad (int n)
        {
            if (n < 40) return "低標";
            else if (n < 70) return "均標";
            else return "前標";
        }

        private void button38_Click(object sender, EventArgs e)  //依 檔案大小 分組檔案 (大=>小)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            
            var q2 = from f in files orderby f.Length descending select f;
            var q1 = from f in files group f by fl(f.Length) into j orderby j.Key ascending select new { Key = j.Key, Count = j.Count() };
            var q = from f in files group f by fl(f.Length) into j orderby j.Key ascending select new { Key = j.Key, Group = j, Count = j.Count() };
            this.dataGridView1.DataSource = q1.ToList();
            this.dataGridView2.DataSource = q2.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.Group)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }
        private string fl (long n)
        {
            if (n<= 1000) return "small";
            else if (n <= 100000) return "medium";
            else return "large";
        }

        private void button6_Click(object sender, EventArgs e)     //  依 年 分組檔案 (大=>小)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q2 = from f in files orderby f.CreationTime.Year descending select f;
            var q1 = from f in files group f by f2(f.CreationTime.Year) into j orderby j.Key ascending select new { Key = j.Key, Count = j.Count() };
            var q = from f in files group f by f2(f.CreationTime.Year) into j orderby j.Key ascending select new { Key = j.Key, Group = j, Count = j.Count() };
            this.dataGridView1.DataSource = q1.ToList();
            this.dataGridView2.DataSource = q2.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.Group)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }
        private string f2(long n)
        {
            if (n <= 2010) return "2010年以前";
            else if (n <= 2020) return "2020以前";
            else return "2020以後";
        }

        NorthwindEntities nw = new NorthwindEntities();
        private void button8_Click(object sender, EventArgs e)     //NW Products 低中高 價產品 
        {
            var q=from p in nw.Products where p.UnitPrice >0 orderby p.UnitPrice ascending select p;
            this.dataGridView2.DataSource = q.ToList();
            var q1 = from p in nw.Products.AsEnumerable()
                     group p by po((double)(p.UnitPrice)) into u
                     select new { Key = u.Key,Count=u.Count() };
            this.dataGridView1.DataSource = q1.ToList(); 
            var q2 = from p in nwDataSet1.Products group p by po(Convert.ToDouble(p.UnitPrice)) into o orderby o.Key descending select new { mkey = o.Key, mgroup = o, mcount = o.Count() };

            treeView1.Nodes.Clear();
            foreach (var group in q2)
            {
                string g = $"{group.mkey}({group.mcount})";
                TreeNode x = treeView1.Nodes.Add(g);
                foreach (var item in group.mgroup)
                {
                    string y = $"{"ID: ".PadLeft(5) + item.ProductID + "".PadRight(10)}{"Product Name:".PadRight(15) + item.ProductName}({item.UnitPrice:n2})";
                    x.Nodes.Add(y);
                }
            }
        }
        private string po (double n)
        {
            if (n <= 25) return "Low Price";
            else if (n<=60) return "Just Ok";
            else return "High Price";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var q1 = from y in nw.Orders.AsEnumerable()
                     orderby y.OrderDate.Value.Year
                     select y;
            var q = from y in nw.Orders.AsEnumerable()
                    group y by yl(y.OrderDate.Value.Year) into o
                    select new { key = o.Key, count = o.Count(), };
            this.dataGridView2.DataSource = q1.ToList();
            this.dataGridView1.DataSource = q.ToList();
            var q2 = from y in nw.Orders.AsEnumerable()
                    group y by yl(y.OrderDate.Value.Year) into o
                    select new { key = o.Key, count = o.Count(), house = o };
            treeView1.Nodes.Clear();
            foreach (var group in q2)
            {
                string s = $"{group.key}({group.count})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.house)
                {
                    string y = $"{"Order ID: ".PadLeft(5) + item.OrderID + "".PadRight(10)}{"Order Date:".PadRight(15) + item.OrderDate}";
                    x.Nodes.Add(y);
                }
            }
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            var q = (from y in nwDataSet1.Orders
                     orderby y.OrderDate.Year ascending
                     select y.OrderDate.Year).Distinct();
            comboBox1.DataSource = q.ToList();
        }
        private string yl(int n)
        {
            if (n == 1996) return "1996";
            else if (n == 1997) return "1997";
            else return "1998";
        }

        private string yk(int n)
        {
            if (n == 1) return "1";
            else if (n == 2) return "2";
            else if (n == 3) return "3";
            else if (n == 4) return "4";
            else if (n == 5) return "5";
            else if (n == 6) return "6";
            else if (n == 7) return "7";
            else if (n == 8) return "8";
            else if (n == 9) return "9";
            else if (n == 10) return "10";
            else if (n == 11) return "11";
            else return "12";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q1 = this.nw.Orders.OrderBy(p => p.OrderDate.Value.Year).ThenBy(p => p.OrderDate.Value.Month);            
            this.dataGridView2.DataSource = q1.ToList();
            var q = this.nw.Orders.AsEnumerable().GroupBy(p => yl(p.OrderDate.Value.Year)).Select(g => new { key = g.Key, Count = g.Count() });
            this.dataGridView1.DataSource = q.ToList();
            var q2 = this.nw.Orders.AsEnumerable().GroupBy(p => yl(p.OrderDate.Value.Year)).Select(g => new { key = g.Key, Count = g.Count()/*, house = g*/ });
            //var q2 = from p in nw.Orders
            //         group p by yl(p.OrderDate.Value.Month) into g
            //         select new
            //         {
            //             key = g.Key,
            //             gc = g.Count(),
            //             h = g,
            //             d = from c in g
            //                 group c by yk(c.OrderDate.Value.Year) into b
            //                 select new
            //                 {
            //                     bkey = b.Key,
            //                     bc=b
            //                 }
            //         };
            var q3 = this.nw.Orders.AsEnumerable().GroupBy(p => yk(p.OrderDate.Value.Month)).Select(y => new { key = y.Key, gCount = y.Count(), yhouse = y });
            //var q1 = this.nw.Orders.OrderBy(p => p.OrderDate.Value.Year).ThenBy(p => p.OrderDate.Value.Month);

            //nw.Orders.AsEnumerable().GroupBy(p=>yl(p.OrderDate.Value.Year) = treeView1.SelectedNode.Text;
            treeView1.Nodes.Clear();
            foreach (var type in q2)
            {
                string s = $"{type.key}({type.Count})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var group in q3)
                {
                    string n = $"{group.key}({group.gCount})";
                    TreeNode y = treeView1.Nodes.Add(n);
                    foreach (var item in group.yhouse)
                    {
                        string m = $"{group.yhouse}";
                            /*$"{"Order ID: ".PadLeft(5) + item.OrderID + "".PadRight(10)}{"Order Date:".PadRight(15) + item.OrderDate}";*/
                        x.Nodes.Add(m);
                    }
                }
            }
        }
    }
}
