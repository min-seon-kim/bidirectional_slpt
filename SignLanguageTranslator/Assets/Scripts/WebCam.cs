using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WebCam : MonoBehaviour
{
    public RawImage display;
    private WebCamTexture camTexture;
    private int currentIndex = 0;
    public int FileCounter = 0;
    private string statusFilePath;
    private bool isPaused = false;


    private void Start() 
    {

        // WebCamDevice[] devices = WebCamTexture.devices;
        // for (int i = 0; i < devices.Length; i++)
        // {
        //     Debug.Log(devices[i].name);
        // }

        statusFilePath = "/home/minseon/Desktop/nslt/status/status.txt";
        if (camTexture != null)
        {
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }
        WebCamDevice device = WebCamTexture.devices[currentIndex];
        camTexture = new WebCamTexture(device.name);
        display.texture = camTexture;
        camTexture.Play();
    }

    IEnumerator delayTime()
    {
        CamCapture();  
        yield return new WaitForSeconds(0.1f);
        if(!isPaused)
        {
            StartCoroutine(delayTime());
        }
        
    }

 
 
    private void LateUpdate()
    {
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     Debug.Log("저장 시도");

        //     CamCapture();  
        // }

        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("##########################################");
            isPaused = true;
            Debug.Log("file status updated");

            StreamWriter writer;
            writer = File.CreateText(statusFilePath);         //Text File이 저장될 위치(파일명)
            //파일 이름만 지정하면 컴파일된 폴더내 해당 파일 이름으로 저장됨
            writer.WriteLine("change");    //저장될 string
            writer.Close();
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S key down");    
            isPaused = false;
            StartCoroutine(delayTime());
        }
    }


 
    void CamCapture()
    {
        RenderTexture currentRT = RenderTexture.active;

        Texture2D Image = Convert_WebCamTexture_To_Texture2d(camTexture);
        Image.Apply();
        RenderTexture.active = currentRT;
 
        var Bytes = Image.EncodeToPNG();
        Destroy(Image);
 
        File.WriteAllBytes("/home/minseon/Desktop/nslt/Data/Images/" + FileCounter + ".png", Bytes);

        FileCounter++;
    }

    public Texture2D Convert_WebCamTexture_To_Texture2d(WebCamTexture _webCamTexture)
    {
    Texture2D _texture2D = new Texture2D(_webCamTexture.width, _webCamTexture.height);
    _texture2D.SetPixels32(_webCamTexture.GetPixels32());

    return _texture2D;
    }
 
}
