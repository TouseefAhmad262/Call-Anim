using System.Collections;
using System.IO;
using UnityEngine;

public class FilePickerSystem : MonoBehaviour
{
    public string FinalPath;


    public void LoadImage()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("png");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation Cancelled");
            }
            else
            {
                FinalPath = path;
                GetComponent<StartUpPanel>().InstentiateBG(GetImage(FinalPath), true);
                Debug.Log("Picked file: " + FinalPath);
            }
        }, new string[] { FileType });

    }

    public void LoadCSV()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("csv");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation Cancelled");
            }
            else
            {
                FinalPath = path;
                GetComponent<StartUpPanel>().CSVPath = FinalPath;
                Debug.Log("Picked file: " + FinalPath);
            }
        }, new string[] { FileType });

    }

    public void LoadVideo()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("mp4");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation Cancelled");
            }
            else
            {
                FinalPath = path;
                gameObject.GetComponent<VideosSetup>().AddVideo(path);
            }
        }, new string[] { FileType });
    }

    Texture2D LoadTextureFromFile(string path)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        if (!texture.LoadImage(fileData))
        {
            Debug.LogError("Failed to load image data from file path: " + path);
            return null;
        }
        return texture;
    }

    Sprite GetImage(string path)
    {
        Texture2D texture = LoadTextureFromFile(path);

        if(texture != null)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            
            return sprite;
        }
        else
        {
            return null;
        }
    }
}