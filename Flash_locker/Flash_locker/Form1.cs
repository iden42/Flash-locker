using System;

using System.Collections.Generic;

using System.IO;

using System.Security.AccessControl;

using System.Windows.Forms;



namespace Flash_locker

{

    public partial class Form1 : Form

    {

        List<Folder> arr = new List<Folder>();

        string folderPath = "null";

        string selectedUSB = "null";



        public Form1()

        {

            InitializeComponent();

        }



        private void buttonFolder_Click(object sender, EventArgs e)

        {

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)

                folderPath = folderBrowserDialog.SelectedPath;

            else

                folderPath = "null";

            labelPath.Text = folderPath;

        }



        private void buttonFlash_Click(object sender, EventArgs e)

        {

            formUSB newForm = new formUSB();

            newForm.ShowDialog();

            selectedUSB = newForm.id;

            if (selectedUSB == "")

                selectedUSB = "null";

            labelFlash.Text = selectedUSB;

        }



        private void buttonDone_Click(object sender, EventArgs e)

        {

            if (folderPath == "null" || selectedUSB == "null")

                MessageBox.Show("Not enough information!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (checkFolderRepeat(folderPath))

                MessageBox.Show("This folder is already protected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else

            {

                try

                {

                    string adminUserName = Environment.UserName;// getting your adminUserName 

                    DirectorySecurity ds = Directory.GetAccessControl(folderPath);

                    FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);



                    ds.AddAccessRule(fsa);

                    Directory.SetAccessControl(folderPath, ds);

                    Folder temp = new Folder(folderPath, selectedUSB);

                    arr.Add(temp);

                    folderPath = "null";

                    selectedUSB = "null";

                    labelPath.Text = folderPath;

                    labelFlash.Text = selectedUSB;



                    MessageBox.Show("Locked!", "Sucess!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    writeDataToFile();

                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message);

                }

            }

        }



        private void buttonClean_Click(object sender, EventArgs e)

        {

            folderPath = "null";

            selectedUSB = "null";

            labelPath.Text = folderPath;

            labelFlash.Text = selectedUSB;

        }



        private void buttonUnlock_Click(object sender, EventArgs e)

        {

            if (arr.Count == 0)

                MessageBox.Show("You haven't protected any folder yet!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (folderPath == "null" || selectedUSB == "null")

                MessageBox.Show("Not enough information!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (!thatFolderIsLocked(folderPath))

                MessageBox.Show("That folder is not locked!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (!checkCorrectChoise(folderPath, selectedUSB))

                MessageBox.Show("Not correct choise!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else

            {

                try

                {

                    string adminUserName = Environment.UserName;// getting your adminUserName 

                    DirectorySecurity ds = Directory.GetAccessControl(folderPath);

                    FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);



                    ds.RemoveAccessRule(fsa);

                    Directory.SetAccessControl(folderPath, ds);



                    removeFolderFromArr(folderPath);

                    writeDataToFile();

                    folderPath = "null";

                    selectedUSB = "null";

                    labelPath.Text = folderPath;

                    labelFlash.Text = selectedUSB;



                    MessageBox.Show("UnLocked", "Sucess!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message);

                }

            }

        }



        private void Form1_Load(object sender, EventArgs e)

        {

            readDataFromFile();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }

}