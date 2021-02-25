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
    public partial class Form1 : Form
    {
        MySqlConnection mscn;
        MySqlCommand mscm;
        MySqlDataReader msdr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mscn = new MySqlConnection("Server=192.168.56.101;Database=sql_db;Uid=winuser;Pwd=p@ssw0rd;Charset=UTF8");
            mscn.Open();
            mscm = new MySqlCommand("", mscn);

            select_user();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            mscn.Close();
        }

        private void bt_insert_Click(object sender, EventArgs e)
        {
            Insert_Form insert_form = new Insert_Form();
            insert_form.ShowDialog();

            select_user();
        }

        private void bt_update_Click(object sender, EventArgs e)
        {
            if (listBox1.Text == "")
            {
                MessageBox.Show("수정할 회원을 선택해주세요.");
                return;
            }

            String[] user_ID = listBox1.Text.Split(' ');

            Update_Form update_form = new Update_Form(user_ID[3]);
            update_form.ShowDialog();

            select_user();
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            if (listBox1.Text == "")
            {
                MessageBox.Show("삭제할 회원을 선택해주세요.");
                return;
            }

            String[] user_ID = listBox1.Text.Split(' ');

            Delete_Form delete_form = new Delete_Form(user_ID[3]);
            delete_form.ShowDialog();

            select_user();
        }

        void select_user()
        {
            mscm.CommandText = "SELECT * FROM user_tb ORDER BY user_Date;";
            msdr = mscm.ExecuteReader();

            listBox1.Items.Clear();
            while (msdr.Read())
            {
                String user_Date = ((DateTime)msdr["user_Date"]).ToString();
                String Phone;
                if ((msdr["Phone1"] is System.DBNull) && (msdr["Phone2"] is System.DBNull))
                    Phone = "-\t";
                else
                    Phone = msdr["Phone1"] + "-" + msdr["Phone2"];

                String str = "   " + msdr["user_ID"] + " \t" + msdr["user_Name"] + "\t" + msdr["Birth"] + "\t" + msdr["user_Addr"] + "\t" + Phone + "\t" + msdr["Height"] + "\t" + user_Date.Substring(0, 10);
                listBox1.Items.Add(str);
            }

            msdr.Close();
        }
    }
}
