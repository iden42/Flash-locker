void readData()

{

    if (File.Exists("data"))
    {

        StreamReader streamReader = new StreamReader("data");

        List<Folder> newArr = new List<Folder>();

        Folder temp = new Folder();

        while (!streamReader.EndOfStream)

        {

            temp.folderPath = streamReader.ReadLine();

            temp.usbID = streamReader.ReadLine();

            newArr.Add(temp);

        }

        streamReader.Close();

        arr = newArr;

    }

}



bool checkFolderRepeat(string path)

{

    bool result = false;



    for (int i = 0; i < arr.Count; i++)

    {

        if (arr[i].folderPath == path)

        {

            result = true;

            break;

        }

    }



    return result;

}



bool thatFolderIsLocked(string path)

{

    bool result = false;



    for (int i = 0; i < arr.Count; i++)

    {

        if (arr[i].folderPath == path)

        {

            result = true;

            break;

        }

    }



    return result;

}



void removeFolderFromArr(string path)

{

    List<Folder> newArr = new List<Folder>();

    for (int i = 0; i < arr.Count; i++)

    {

        if (arr[i].folderPath != path)

            newArr.Add(arr[i]);

    }

    arr = newArr;

}

