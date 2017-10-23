using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabin
{
    public partial class frmMain : Form
    {
        Graphics g_HinhAnhCaBin;
        private float viTriHienTai;
        private float tocDo;
        private float soLanLenXuong;
        private float max_ViTri;
        private float min_ViTri;
        private float max_TocDo;
        private float min_TocDo;
        private bool isDiLen;
        private InfoCabin infoCabin;
        private Description description_ViTri;
        private Description description_TocDoDiLen;
        private Description description_TocDoDiXuong;

        Color SetColorChartTocDo(bool isDiLen)
        {
            Color c = isDiLen ? Color.Green : Color.Red;
            chart.Series["SeriesTocDo"].Color = c;
            return c;
        }

        public float ViTriHienTai
        {
            get
            {
                return viTriHienTai;
            }

            set
            {
                viTriHienTai = value;
                txtViTriHienTai.Text = viTriHienTai.ToString();
                if (viTriHienTai > Max_ViTri) Max_ViTri = viTriHienTai;
                else if (viTriHienTai < Min_ViTri) Min_ViTri = viTriHienTai;
                Description_ViTri.Value = viTriHienTai;
                _updateListViewDescription_ViTri();
            }
        }

        public float TocDo
        {
            get
            {
                return tocDo;
            }

            set
            {
                tocDo = value;
                txtTocDo.Text = tocDo.ToString();
                if (tocDo > Max_TocDo) Max_TocDo = tocDo;
                else if (tocDo < Min_TocDo) Min_TocDo = tocDo;
                _updateListViewDescription_TocDo();
            }
        }

        private void _updateListViewDescription_TocDo()
        {
            if (IsDiLen)
            {
                Description_TocDoDiLen.Value = TocDo;
                if (listViewDescription.Items.Count > 2)
                {
                    listViewDescription.Items[1].ForeColor = Color.Blue;
                    listViewDescription.Items[1] = Description_TocDoDiLen.GetListViewItem("Description_TocDoDiLen", isEnableDiLen);
                }
            }
            else
            {
                Description_TocDoDiXuong.Value = TocDo;
                if (listViewDescription.Items.Count > 3)
                {
                    listViewDescription.Items[2] = Description_TocDoDiXuong.GetListViewItem("Description_TocDoDiXuong", isEnableDiXuong);
                    listViewDescription.Items[2].ForeColor = Color.Teal;
                }
            }
        }

        public float SoLanLenXuong
        {
            get
            {
                return soLanLenXuong;
            }

            set
            {
                soLanLenXuong = value;
                txtSoLanLenXuong.Text = soLanLenXuong.ToString();
            }
        }

        public float Max_ViTri
        {
            get
            {
                return max_ViTri;
            }

            set
            {
                max_ViTri = value;
                Description_ViTri.Max = max_ViTri;
                _updateListViewDescription_ViTri();
            }
        }

        private void _updateListViewDescription_ViTri()
        {
            if (listViewDescription.Items.Count > 0)
            {
                listViewDescription.Items[0] = Description_ViTri.GetListViewItem("Description_ViTri", isEnableViTri);
                listViewDescription.Items[0].ForeColor = Color.Green;
            }
        }

        public float Min_ViTri
        {
            get
            {
                return min_ViTri;
            }

            set
            {
                min_ViTri = value;
                Description_ViTri.Min = min_ViTri;
                _updateListViewDescription_ViTri();
            }
        }

        public float Max_TocDo
        {
            get
            {
                return max_TocDo;
            }

            set
            {
                max_TocDo = value;
                _updateListViewDescription_TocDo();
            }
        }

        public float Min_TocDo
        {
            get
            {
                return min_TocDo;
            }

            set
            {
                min_TocDo = value;
                _updateListViewDescription_TocDo();
            }
        }

        public bool IsDiLen
        {
            get
            {
                return isDiLen;
            }

            set
            {
                isDiLen = value;
                SetColorChartTocDo(isDiLen);
                if (IsDiLen) chart.Series[1].Enabled = isEnableDiLen;
                else chart.Series[1].Enabled = isEnableDiXuong;
                chart.Series[0].Enabled = isEnableViTri;
            }
        }

        public InfoCabin InfoCabin
        {
            get
            {
                return infoCabin;
            }

            set
            {
                infoCabin = value;
            }
        }

        public Description Description_ViTri
        {
            get
            {
                return description_ViTri;
            }

            set
            {
                description_ViTri = value;
            }
        }

        public Description Description_TocDoDiLen
        {
            get
            {
                return description_TocDoDiLen;
            }

            set
            {
                description_TocDoDiLen = value;
            }
        }

        public Description Description_TocDoDiXuong
        {
            get
            {
                return description_TocDoDiXuong;
            }

            set
            {
                description_TocDoDiXuong = value;
            }
        }

        public frmMain()
        {
            InitializeComponent();
            g_HinhAnhCaBin = pnImageCaBin.CreateGraphics();

            //init value
            InfoCabin = new InfoCabin()
            {
                ThongDiep = "Cabin"
            };
            Description_TocDoDiLen = new Description()
            {
                Name = "Tốc độ đi lên"
            };
            Description_TocDoDiXuong = new Description()
            {
                Name = "Tốc độ đi xuống"
            };
            Description_ViTri = new Description()
            {
                Name = "Vị trí hiện tại"
            };
            ViTriHienTai = 0;
            TocDo = 0;
            SoLanLenXuong = 0;
            Max_ViTri = 0;
            Min_ViTri = 0;
            Min_ViTri = 0;
            Max_TocDo = 0;
            Min_TocDo = 0;
            IsDiLen = true;
            SetColorChartTocDo(IsDiLen);

            //create ListView Cabin
            listViewCabin.CheckBoxes = true;
            listViewCabin.View = View.Details;
            listViewCabin.GridLines = true;
            listViewCabin.FullRowSelect = true;
            listViewCabin.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewCabin.Columns.Add("Thông điệp", 270);
            listViewCabin.Columns.Add("Thời gian", 120);
            listViewCabin.Columns.Add("Khoảng thời gian", 120);
            listViewCabin.Columns.Add("Mức độ", 120);
            listViewCabin.Columns.Add("Trang thái", 120);

            listViewCabin.Items.Add(new ListViewItem(new[] { InfoCabin.ThongDiep, InfoCabin.ThoiGian.ToString(), InfoCabin.KhoangThoiGian, InfoCabin.MucDo, InfoCabin.TrangThai }));

            //create ListView Description
            listViewDescription.CheckBoxes = true;
            listViewDescription.View = View.Details;
            listViewDescription.GridLines = true;
            listViewDescription.FullRowSelect = true;
            listViewDescription.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewDescription.Columns.Add("Description", 270);
            listViewDescription.Columns.Add("Value", 120);
            listViewDescription.Columns.Add("Min", 120);
            listViewDescription.Columns.Add("Max", 120);
            listViewDescription.Columns.Add("Average", 120);
            listViewDescription.Items.Add(Description_ViTri.GetListViewItem("Description_ViTri", true));
            listViewDescription.Items.Add(Description_TocDoDiLen.GetListViewItem("Description_TocDoDiLen", true));
            listViewDescription.Items.Add(Description_TocDoDiXuong.GetListViewItem("Description_TocDoDiXuong", true));
            listViewDescription.Items[0].ForeColor = Color.Green;
            listViewDescription.Items[1].ForeColor = Color.Blue;
            listViewDescription.Items[2].ForeColor = Color.Teal;

            //Add Serial to chart
            chart.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chart.Series[1].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chart.Series[0].IsVisibleInLegend = false;
            chart.Series[1].IsVisibleInLegend = false;
        }

        private void _reload()
        {
            timerTime.Start();
            _veBackground();
            _loadListViewDescription();
        }

        private void _loadListViewDescription()
        {

        }

        private void _veBackground()
        {
            Brush b = Brushes.Teal;
            g_HinhAnhCaBin.FillPolygon(b, new Point[] {
                new Point(0,pnImageCaBin.Height),
                new Point(picHouse.Location.X+picHouse.Width,pnImageCaBin.Height),
                new Point(picHouse.Location.X+picHouse.Width,picHouse.Location.Y+picHouse.Height),
                new Point(picHouse.Location.X,picHouse.Location.Y+picHouse.Height)
            });
        }

        public void UpdateTime()
        {
            DateTime now = DateTime.Now;
            lblTime.Text = now.ToString("hh:MM:ss");
            lblDate.Text = now.ToString("dd-mm-yyyy");
            toolStripStatusLabelTime.Text = now.ToString("hh:mm:ss dd-MM-yyyy");
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _reload();
        }

        Random _rd = new Random();
        private void btnAddnewValue_Click(object sender, EventArgs e)
        {
            ViTriHienTai = _rd.Next() % 500;
            SoLanLenXuong += _rd.Next() % 2 == 0 ? 1 : 0;
            TocDo = _rd.Next() % 200;

            chart.Series[0].Points.AddXY(DateTime.Now.ToLongTimeString(), ViTriHienTai);
            chart.Series[1].Points.AddXY(DateTime.Now.ToLongTimeString(), TocDo);
            if (chart.Series[0].Points.Count > 29) chart.Series[0].Points.RemoveAt(0);
            if (chart.Series[1].Points.Count > 29) chart.Series[1].Points.RemoveAt(0);

            IsDiLen = Math.Round((double)_rd.Next() % 2) == 0;
        }

        public bool isEnableDiLen { get; protected set; }
        public bool isEnableDiXuong { get; protected set; }
        public bool isEnableViTri { get; protected set; }
        private void listViewDescription_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            switch (e.Item.Index)
            {
                case 0: //Description_Vitri
                    isEnableViTri = e.Item.Checked;
                    break;
                case 1: //Description_TocDoDiLen
                    isEnableDiLen = e.Item.Checked;
                    break;
                case 2://Description_TocDoDiXuong
                    isEnableDiXuong = e.Item.Checked;
                    break;
                default:
                    break;
            }
        }
    }

    public class InfoCabin
    {
        public string ThongDiep { get; set; }
        public DateTime ThoiGian { get; set; }
        public string KhoangThoiGian { get; set; }
        public string MucDo { get; set; }
        public string TrangThai { get; set; }
        public InfoCabin()
        {
            ThongDiep = "";
            ThoiGian = DateTime.Now;
            KhoangThoiGian = "";
            MucDo = "";
            TrangThai = "ON";
        }
    }

    public class Description
    {
        public string Name { get; set; }
        public float Value { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public float Average { get; set; }
        public Description()
        {
            Name = "";
            Value = 0;
            Min = 0;
            Max = 0;
            Average = 0;
        }

        public ListViewItem GetListViewItem(string key, bool Checked)
        {
            var item = new ListViewItem(new[] { this.Name, this.Value.ToString(), this.Min.ToString(), this.Max.ToString(), this.Average.ToString() });
            item.Name = key;
            item.Checked = Checked;
            return item;
        }
    }
}
