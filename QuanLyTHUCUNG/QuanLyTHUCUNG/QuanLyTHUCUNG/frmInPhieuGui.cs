﻿using System;
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
    public partial class frmInPhieuGui : Form
    {
        public frmInPhieuGui()
        {
            InitializeComponent();
        }
        public  long id;
        private void frmInPhieuGui_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.DataSet1.DataTable1, id);

            this.reportViewer1.RefreshReport();
        }
    }
}
