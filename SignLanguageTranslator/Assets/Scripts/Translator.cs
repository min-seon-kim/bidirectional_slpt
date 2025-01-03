using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using TMPro;

public class Translator : MonoBehaviour
{
    private const string APIKey = "AIzaSyCmevB6KFhl0peZFY8HwT2-vqSlWgoy6_4";
    [SerializeField] private string path;
    [SerializeField] private string file;
    private FileSystemWatcher watcher;
    private bool changed = false;
    [SerializeField]
    private GameObject textUI;
    private TextMeshProUGUI text;
    private string result;
    private bool changeUI = false;
    [SerializeField]
    private string translatedFilePath;
    [SerializeField]
    private List<String> textList;
    private int currentIndex;
    private int iterTime;

    void Start()
    {
        text = textUI.GetComponent<TextMeshProUGUI>();

        currentIndex = 0;

        iterTime = 0;
    }
    public void Translate(string text)
    {
        iterTime += 1;
        
        // german -> english 
        // de
        TranslateText("de", "en", text, (success, translatedText) =>
        {
            if (success)
            {
                Debug.Log(translatedText);
                // result = translatedText;
                // result = textList[currentIndex];

            
                // System.IO.File.Delete(translatedFilePath);
                // System.IO.File.Create(translatedFilePath);

                StreamWriter writer;
                writer = File.AppendText(translatedFilePath);         //Text File이 저장될 위치(파일명)
                //파일 이름만 지정하면 컴파일된 폴더내 해당 파일 이름으로 저장됨
                writer.WriteLine(result);    //저장될 string
                writer.Close();
                changeUI = true;

                currentIndex = (currentIndex+1)%3;
                
                
            }
        });
        
    }

    private void OnEnable()
    {
        if (!File.Exists(Path.Combine(path, file)))
        {
            return;
        }

        watcher = new FileSystemWatcher();
        watcher.Path = path;
        watcher.Filter = file;

        // Watch for changes in LastAccess and LastWrite times, and
        // the renaming of files or directories.
        watcher.NotifyFilter = NotifyFilters.LastWrite;

        // Add event handlers
        watcher.Changed += OnChanged;

        // Begin watching
        watcher.EnableRaisingEvents = true;
    }

    private void OnChanged(object source, FileSystemEventArgs e)
    {
        string curText = "";
        // // on Changed 감지 되면 해당 skel 값을 bone animation에 적용할 수 있도록 짜기 
        StreamReader sr = new StreamReader(Path.Combine(path, file));
        curText = sr.ReadToEnd();
        Debug.Log(curText);
        result = curText;
        changed = true;
    }

    void Update()
    {
        if(changed)
        {
            Translate(result);
            changed = false;
            
        }
        if(changeUI)
        {
            text.text = result;
            changeUI = false;
        }
    }

    private void OnDisable()
    {
        if(watcher != null)
        {
            watcher.Changed -= OnChanged;
            watcher.Dispose();
        }
    }

    public void TranslateText(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        StartCoroutine(TranslateTextRoutine(sourceLanguage, targetLanguage, sourceText, callback));
    }

    private IEnumerator TranslateTextRoutine(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        var formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("Content-Type", "application/json; charset=utf-8"),
            new MultipartFormDataSection("source", sourceLanguage),
            new MultipartFormDataSection("target", targetLanguage),
            new MultipartFormDataSection("format", "text"),
            new MultipartFormDataSection("q", sourceText)
        };

        var uri = $"https://translation.googleapis.com/language/translate/v2?key={APIKey}";

        var webRequest = UnityWebRequest.Post(uri, formData);

        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.LogError(webRequest.error);
            callback.Invoke(false, string.Empty);

            yield break;
        }

        var parsedTexts = JSONNode.Parse(webRequest.downloadHandler.text);
        var translatedText = parsedTexts["data"]["translations"][0]["translatedText"];

        callback.Invoke(true, translatedText);
    }
}