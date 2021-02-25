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
    public partial class Delete_Form : Form
    {
        MySqlConnection mscn;
        MySqlCommand mscm;
        MySqlDataReader msdr;
        String user_ID;

        public Delete_Form(String user_ID)
        {
            InitializeComponent();

            this.user_ID = user_ID;
        }

        private void Delete_Form_Load(object sender, EventArgs e)
        {
            mscn = new MySqlConnection("Server=192.168.56.101;Database=sql_db;Uid=winuser;Pwd=p@ssw0rd;Charset=UTF8");
            mscn.Open();
            mscm = new MySqlCommand("", mscn);

            mscm.CommandText = "SELECT user_Name, Birth, user_Addr, Phone1, Phone2, Height FROM user_tb WHERE user_ID='" + user_ID + "';";
            msdr = mscm.ExecuteReader();
            msdr.Read();

            textBox1.Text = user_ID;
            textBox2.Text = (String)msdr["user_Name"];
            textBox3.Text = ((int)msdr["Birth"]).ToString();
            textBox4.Text = (String)msdr["user_Addr"];
            if (!(msdr["Phone1"] is System.DBNull))
                textBox5.Text = (String)msdr["Phone1"];
            if (!(msdr["Phone2"] is System.DBNull))
                textBox6.Text = (String)msdr["Phone2"];
            textBox7.Text = ((short)msdr["Height"]).ToString();

            msdr.Close();
        }

        private void Delete_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            mscn.Close();
        }

        private void bt_insert_Click(object sender, EventArgs e)
        {
            mscm.CommandText = "DELETE FROM user_tb WHERE user_ID='" + user_ID + "';";

            try
            {
                mscm.ExecuteNonQuery();
                MessageBox.Show("[성공]  삭제되었습니다.");
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
