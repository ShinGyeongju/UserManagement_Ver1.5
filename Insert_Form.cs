using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _026_01_통합미션
{
    public partial class Insert_Form : Form
    {
        MySqlConnection mscn;
        MySqlCommand mscm;

        public Insert_Form()
        {
            InitializeComponent();
        }

        private void Insert_Form_Load(object sender, EventArgs e)
        {
            mscn = new MySqlConnection("Server=192.168.56.101;Database=sql_db;Uid=winuser;Pwd=p@ssw0rd;Charset=UTF8");
            mscn.Open();
            mscm = new MySqlCommand("", mscn);
        }

        private void Insert_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            mscn.Close();
        }

        private void bt_insert_Click(object sender, EventArgs e)
        {
            mscm.CommandText = "INSERT INTO user_tb VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', " + textBox3.Text + ", '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', " + textBox7.Text + ", now());";
            try
            {
                mscm.ExecuteNonQuery();
                MessageBox.Show("[성공]  가입되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("[실패]  잘못된 값입니다.");
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
