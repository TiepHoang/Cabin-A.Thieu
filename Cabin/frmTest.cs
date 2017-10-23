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
using System.Windows.Forms.DataVisualization.Charting;

namespace Cabin
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        List<iType> lst = new List<iType>();


        public class iType
        {
            public int Value { get; set; }
            public DateTime Time { get; set; }
        }
        private void frmTest_Load(object sender, EventArgs e)
        {
            chart.Titles.Add("CHART TEST");
            chart.Series.Clear();

            chart.Series.Add(new Series()
            {
                LegendText = "value",
                ChartType = SeriesChartType.Spline,
                Color = Color.Green
            });
            chart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 100;
            chart.ChartAreas[0].AxisY.LineColor = Color.Green;
            //chart.ChartAreas[0].AxisY.CustomLabels.Add(0, 5, "Group1", 1, LabelMarkStyle.None);
            chart.Series[0].ToolTip = "Total Entries: #VALY\n Name : #VALX";
            chart.ChartAreas[0].AxisY.CustomLabels.Add(new CustomLabel()
            {
                FromPosition = 22,
                Text = "đây là 22"
            });
            chart.ChartAreas[0].AxisY.CustomLabels.Add(new CustomLabel()
            {
                FromPosition = 33,
                Text = "đây là 33"
            });
            chart.ChartAreas[0].AxisY.CustomLabels.Add(new CustomLabel()
            {
                FromPosition = 44,
                Text = "đây là 44"
            });
            chart.ChartAreas[0].AxisY.CustomLabels.Add(new CustomLabel()
            {
                FromPosition = 55,
                Text = "đây là 55",
                ForeColor = Color.Green
            });

            chart.Series.Add(new Series()
            {
                LegendText = "speed",
                ChartType = SeriesChartType.Spline,
                Color = Color.Red
            });

            //timer1.Start();


            listView1.View = View.Details;
            listView1.Columns.Add("Name");
            listView1.Columns.Add("LastAccessTime");
            listView1.Columns.Add("LastWriteTime");
            foreach (var item in new DirectoryInfo("C:/").GetDirectories())
            {
                listView1.Items.Add(new ListViewItem(new[] { item.Name,item.LastAccessTime.ToString(),item.LastWriteTime.ToString()}));
            }
            listView1.Items[1] = new ListViewItem(new[] { "oke1","oke2","oke3" });
        }

        Random _rd = new Random();

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (_rd.Next() % 2 == 0)
            {
                chart.Series[0].Color = Color.Tan;
            }
            else
            {
                chart.Series[0].Color = Color.Teal;
            }
        }
        void addnew()
        {
            chart.Series[0].Points.AddXY(DateTime.Now.ToLongTimeString(), _rd.Next() % 100);
            if (chart.Series[0].Points.Count > 30) chart.Series[0].Points.RemoveAt(0);

            chart.Series[1].Points.AddXY(DateTime.Now.ToLongTimeString(), _rd.Next() % 100);
            if (chart.Series[1].Points.Count > 30) chart.Series[1].Points.RemoveAt(0);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            addnew();
        }
    }
}
