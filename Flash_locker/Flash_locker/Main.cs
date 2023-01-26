namespace Flash_locker

{

    public partial class Form1

    {

        struct Folder

        {

            public string folderPath;

            public string usbID;



            public Folder(string folder_path, string usb_id)

            {

                this.folderPath = folder_path;

                this.usbID = usb_id;

            }

        }

    }

}