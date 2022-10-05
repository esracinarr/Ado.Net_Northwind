using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adonet_Nortwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            /*Datagridview Property default
             1-AutosizeColumnMode->Fill
             2-BackgroundColor->Control
             3-ContextMenustrip ->ContextMenustrip1
             4-Modifiers -> public
             5-ReadOnly ->True
             6-SelectionMode->FullRowSelect
             */
            groupBox1.Visible = false;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Db.GridFill("");
        }

        bool isInsert = true;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isInsert)
                Db.AddEmployees(txtName.Text, txtSurname.Text, txtCity.Text, txtCountry.Text);

            else
                Db.UpdateEmployees(id, txtName.Text, txtSurname.Text, txtCity.Text, txtCountry.Text);

            TextBoxClear();
        }

        private void TextBoxClear()
        {
            foreach (var item in this.Controls)
            {
                if (item is TextBox)
                    (item as TextBox).Clear();
            }
        }

        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Ekle";
            isInsert = true;
            groupBox1.Visible = true;
        }

        int id;
        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Güncelle";
            isInsert = false;
            groupBox1.Visible = true;

            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtCity.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtCountry.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Db.DeleteEmployees(id);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string where_param = $"where FirstName like '%{txtSearch.Text}%' or LastName like '{txtSearch.Text}%'";
            Db.GridFill(where_param);
        }
    }
}
