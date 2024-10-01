using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class HelloWorldReceiver : MonoBehaviour
{
    // Replace with your server's IP address or domain
    public string serverAddress = "http://127.0.0.1:8000";
    public string messageToSend = "Hello Server";

    [Header("Ui Part")]
    public TextMeshProUGUI ServerReply;
    public TMP_InputField MyInputField;

    void Start()
    {
        StartCoroutine(GetHelloWorldText(messageToSend));
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            StartCoroutine (GetHelloWorldText(MyInputField.text));
        }
    }

    IEnumerator GetHelloWorldText(string Data)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("message", Data);

        UnityWebRequest www = UnityWebRequest.Post(serverAddress, formData);

        print(formData);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            ServerReply.text = www.downloadHandler.text;
            Debug.Log("Received from server: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Failed to send message: " + www.error);
        }
    }
}