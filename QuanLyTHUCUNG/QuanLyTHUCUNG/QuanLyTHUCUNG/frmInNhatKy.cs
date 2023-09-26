using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTHUCUNG
{
    public partial class frmInNhatKy : Form
    {
        public frmInNhatKy()
        {
            InitializeComponent();
        }
        public  long id;
        private void frmInNhatKy_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.DataTable2' table. You can move, or remove it, as needed.
            this.DataTable2TableAdapter.Fill(this.DataSet1.DataTable2, id);

            this.reportViewer1.RefreshReport();
        }
    }
}
