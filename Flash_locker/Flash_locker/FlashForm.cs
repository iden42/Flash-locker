using System;

using System.Collections.Generic;

using System.Management;

using System.Windows.Forms;
namespace Flash_locker
{

    public partial class formUSB : Form

    {

        public string id = "";

        List<USBDeviceInfo> listDevices;

        static List<USBDeviceInfo> GetUSBDevices()

        {

            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();



            ManagementObjectCollection collection;

            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))

                collection = searcher.Get();



            foreach (var device in collection)

            {

                devices.Add(new USBDeviceInfo(

                (string)device.GetPropertyValue("DeviceID"),

                (string)device.GetPropertyValue("PNPDeviceID"),

                (string)device.GetPropertyValue("Description")

                ));

            }



            collection.Dispose();

            return devices;

        }



        public formUSB()

        {

            InitializeComponent();

            listDevices = GetUSBDevices();

            for (int i = 0; i < listDevices.Count; i++)

            {
                listView1.Items.Add(listDevices[i].DeviceID);

            }

        }



        private void buttonDone_Click(object sender, EventArgs e)

        {

            try

            {
                id = listDevices[listView1.SelectedIndices[0]].DeviceID;

                this.Close();

            }

            catch

            {

                MessageBox.Show("Choose USB device!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }



        private void buttonCansel_Click(object sender, EventArgs e)

        {

            this.Close();

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // formUSB
            // 
            this.ClientSize = new System.Drawing.Size(355, 284);
            this.Name = "formUSB";
            this.Load += new System.EventHandler(this.formUSB_Load);
            this.ResumeLayout(false);

        }

        private void formUSB_Load(object sender, EventArgs e)
        {

        }
    }

}