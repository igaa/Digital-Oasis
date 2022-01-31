using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using soal_nomor_05.Model;
using System.IO;

namespace soal_nomor_05
{
    public partial class MainForm : Form
    {
        private connection conn;
        private SqlConnection scon;
        private bool is_new = false;
        private SqlCommand cmd;
        private DataTable dt;

        private pegawaimodel dto;

        private string id = string.Empty; 

        private List<pegawaimodel> model;
        private List<pegawaimodel> modellist;

        public MainForm()
        {
            InitializeComponent();
            load_data();
        }
        private void load_data()
        {
            conn = new connection();
            var con_str = conn.connection_str();

            scon = new SqlConnection(con_str);

            show_data(); 
            //prosess(1); 

        }

        private void show_image()
        {
            
        }

        private void show_data()
        {
            if (scon.State == ConnectionState.Open)
            {
                scon.Close();
            }


            scon.Open();
            dt = new DataTable(); 

            SqlDataAdapter sda = new SqlDataAdapter("select * from dbo.pegawai", scon);
            sda.Fill(dt);

            model = new List<pegawaimodel>(); 

            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i <  dt.Rows.Count; i++)
                {
                    model.Add(new pegawaimodel
                    {
                        nama = dt.Rows[i][1].ToString(),
                        email = dt.Rows[i][2].ToString(),
                        gender = Convert.ToInt32(dt.Rows[i][3]),
                        nip = Convert.ToInt32(dt.Rows[i][4]),
                        hoby = dt.Rows[i][5].ToString(),
                        photo = dt.Rows[i][6].ToString() == "" ? new Byte[0] : (byte[])dt.Rows[i][6]

                    }) ;
                }
               
            }

            delete.Enabled = false;

            sda.Dispose();

            grv.DataSource = dt;
            grv.AllowUserToAddRows = false; 
            grv.Columns[0].Visible = false;
            grv.Columns[6].Visible = false;
            grv.Columns[7].Visible = false;


        }

        private void prosess(int p)
        {
            if (scon.State == ConnectionState.Open)
            {
                scon.Close();
            }

            try
            {
                scon.Open();

                cmd = new SqlCommand("dbo.crud_pegawai", scon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = dto.id;
                cmd.Parameters.Add("@nama", SqlDbType.VarChar).Value = dto.nama;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = dto.email;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = dto.gender;
                cmd.Parameters.Add("@nip", SqlDbType.Int).Value = dto.nip;
                cmd.Parameters.Add("@hoby", SqlDbType.VarChar).Value = dto.hoby;

                switch (p)
                {
                    case 1:
                        cmd.Parameters.Add("@action", SqlDbType.VarChar).Value = "save";
                        break;
                    case 2:
                        cmd.Parameters.Add("@action", SqlDbType.VarChar).Value = "delete";
                        break;
                }

                cmd.ExecuteNonQuery();

                cmd.Dispose();

                show_data(); 
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void filltxb(pegawaimodel dtos)
        {
            if (dtos != null)
            {
                txbNama.Text = dtos.nama;
                txbEmail.Text = dtos.email;
                if (dtos.gender == 1)
                {
                    male.Checked = true; 

                }else if (dtos.gender == 2)
                {
                    female.Checked = true;
                }
                nip.Text = dtos.nip.ToString();
                cbxhoby.SelectedItem = dtos.hoby;

                if(dto.photo.Length > 0)
                {
                    byte[] bImage = (byte[])dto.photo;
                    MemoryStream ms = new MemoryStream(bImage);
                    pbx.Image = Image.FromStream(ms); 
                }

                delete.Enabled = true;
            }
            
            
        }


        private void btnupload_Click(object sender, EventArgs e)
        {
            string filepath = string.Empty; 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;"; 
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog.FileName;
            }
            if(filepath != string.Empty)
            {
                byte[] filebyte = File.ReadAllBytes(filepath);

                var toKB = filebyte.Length / 1024;

                if (toKB > 1024)
                {
                    MessageBox.Show("Photo tidak boleh lebih dari 1 mb");

                    return;
                }

                conn = new connection();
                var con_str = conn.connection_str();

                SqlConnection connection = new SqlConnection(con_str);

                if (scon.State == ConnectionState.Open)
                {
                    scon.Close();
                }


                if (id == string.Empty)
                {
                    MessageBox.Show("input data pegawai dahulu !");
                    return;
                }

                connection.Open();

                string sql = "update [dbo].[pegawai] set [file] = @file, [filepath] = @path where id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@file", SqlDbType.VarBinary).Value = filebyte;
                cmd.Parameters.Add("@path", SqlDbType.VarChar).Value = Path.GetFileName(filepath);
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = Guid.Parse(id);

                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("success");

                show_data(); 
            }
            


        }

        private void grv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dto = new pegawaimodel(); 

            
            if (grv.Rows[e.RowIndex].Cells.Count > 0)
            {
                id = grv.Rows[e.RowIndex].Cells[0].Value.ToString(); 
                dto.nama = grv.Rows[e.RowIndex].Cells[1].Value.ToString();
                dto.email = grv.Rows[e.RowIndex].Cells[2].Value.ToString(); 
                dto.gender = Convert.ToInt32(grv.Rows[e.RowIndex].Cells[3].Value.ToString());
                dto.nip = Convert.ToInt32(grv.Rows[e.RowIndex].Cells[4].Value.ToString());
                dto.hoby = grv.Rows[e.RowIndex].Cells[5].Value.ToString();
                dto.photo = (byte[])grv.Rows[e.RowIndex].Cells[6].Value;
            }

            filltxb(dto);
        }

        private bool validate()
        {
            bool is_true = true; 

            if (txbNama.Text.Trim().ToString() == string.Empty)
            {
                txbNama.Tag = "Required"; 
                is_true = false; 
            }

            if (txbEmail.Text.Trim().ToString() == string.Empty)
            {
                txbEmail.Tag = "Required";
                is_true = false;
            }

            if (male.Checked == false && female.Checked == false)
            {
                MessageBox.Show("Choose Gender");
                is_true = false; 
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(nip.Text, "[^0-9]"))
            {
                MessageBox.Show("Only Number are Allowed");
                is_true = false;
            }

            if (cbxhoby.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Select one Hoby");
                is_true = false; 
            }

            return is_true; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dto = new pegawaimodel();

            var is_valid = validate(); 

            if (!is_valid)
            {
                return; 
            }

            var data = model.Where(s => s.nip == Convert.ToInt32(nip.Text.Trim())).ToList(); 
            if (data.Count == 0)
            {
                is_new = true; 
            }

            if (is_new)
            {
                dto.id = Guid.NewGuid(); 
                dto.nama = txbNama.Text;
                dto.email = txbEmail.Text;

                if (male.Checked)
                {
                    dto.gender = 1;
                }
                else if (female.Checked)
                {
                    dto.gender = 2;
                }

                dto.nip = Convert.ToInt32(nip.Text);
                dto.hoby = cbxhoby.SelectedItem.ToString();

            }else
            {
                dto.id = Guid.Parse(id); 
                dto.nama = txbNama.Text;
                dto.email = txbEmail.Text;

                if (male.Checked)
                {
                    dto.gender = 1;
                }
                else if (female.Checked)
                {
                    dto.gender = 2;
                }

                dto.nip = Convert.ToInt32(nip.Text);
                dto.hoby = cbxhoby.SelectedItem.ToString();
            }

            if (dto != null)
            {
                prosess(1);
            }
            
        }

        private void add_Click(object sender, EventArgs e)
        {
            id = string.Empty; 
            txbNama.Text = string.Empty;
            txbEmail.Text = string.Empty;
            male.Checked = false;
            female.Checked = false;
            nip.Text = string.Empty;
            cbxhoby.SelectedItem = string.Empty;

            delete.Enabled = false; 
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (id != string.Empty)
            {
                prosess(2); 
            }
        }
    }
}
