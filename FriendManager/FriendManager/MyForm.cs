using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendManager
{
    public partial class MyForm : Form
    {

        BindingList<Friend> friends = new BindingList<Friend>();
        Friend friend = null;
        public MyForm()
        {
            InitializeComponent();

        }

        //save button
        private void button2_Click(object sender, EventArgs e)
        {
            if (friend == null)
            {

                friend = new Friend();
                friend.Name = txt_Name.Text;
                friend.Address = txt_Address.Text;
                int age;
                int.TryParse(txt_age.Text, out age);
                friend.Age = age;
                friend.Phone = txt_Phone.Text;
                string data = txt_Name.Text;

                if (FriendIsValid(friend))
                {
                    friends.Add(friend);
                    MessageBox.Show("Added");
                }
                else
                {
                    MessageBox.Show("not Added");
                }


            }
            else
            {

                friend.Name = txt_Name.Text;
                friend.Address = txt_Address.Text;
                int age;
                int.TryParse(txt_age.Text, out age);
                friend.Age = age;
                friend.Phone = txt_Phone.Text;
                string data = txt_Name.Text;
                MessageBox.Show("Saved");
            }

            btn_SaveFile.PerformClick();

        }

        private bool FriendIsValid(Friend newFriend)
        {
            //check for invlaid name
            if (friend.Name.Any(c => char.IsDigit(c)))
            {
                return false;
            }

            //check for invalid phone number
            if (!friend.Phone.All(c => char.IsDigit(c)))
            {
                return false;
            }



            //check for duplication
            for (int i = 0; i < friends.Count; i++)
            {
                Friend friend = friends[i];
                if (friend.Name == newFriend.Name || (friend.Phone == newFriend.Phone && friend.Phone != ""))
                {
                    return false;
                }
            }

            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txt_Name.Text = "";
        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {

        }

        // go to button
        private void button1_Click_1(object sender, EventArgs e)
        {
            int selextedIndex = 0;
            if (int.TryParse(textBox1.Text, out selextedIndex))
            {
                if (selextedIndex > 0 && selextedIndex <= friends.Count)
                {
                    friend = friends[selextedIndex - 1];
                    txt_Address.Text = friend.Address;
                    txt_Name.Text = friend.Name;
                    txt_age.Text = friend.Age.ToString();
                    txt_Phone.Text = friend.Phone;


                }



            }
        }
        //new button
        private void button3_Click(object sender, EventArgs e)
        {
            friend = null;
            txt_Phone.Text = string.Empty;
            txt_Name.Text = string.Empty;
            txt_age.Text = string.Empty;
            txt_Address.Text = string.Empty;

        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = friends;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index >= 0)
            {
                textBox1.Text = (dataGridView1.SelectedRows[0].Index + 1).ToString();
            }
        }

        static void SaveToFile(string fileName, List<Friend> list)
        {

            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, list);
            stream.Flush();
            stream.Close();
        }


        static List<Friend> LoadFromFile(string fileName)
        {
            List<Friend> list = new List<Friend>();
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                list = (List<Friend>)formatter.Deserialize(stream);
                stream.Close();
                stream.Dispose();
            }
            return list;
        }

        private void btn_SaveFile_Click(object sender, EventArgs e)
        {
            SaveToFile("myData.dat", friends.ToList());
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            friends = new BindingList<Friend>(LoadFromFile("myData.dat"));

            dataGridView1.DataSource = friends;
        }

        private void MyForm_Load_1(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click_1(object sender, EventArgs e)
        {
            if (friend == null)
            {
                MessageBox.Show("no friend selected");
                return;
            }
            DialogResult = MessageBox.Show("ARE YOU SURE????", "!!!!!!!!warning!!!!!!", MessageBoxButtons.OKCancel);


            if (DialogResult == DialogResult.OK)
            {
                friends.Remove(friend);
            }
        }
    }


}
