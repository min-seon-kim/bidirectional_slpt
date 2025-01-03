using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FileWatcher : MonoBehaviour
{
    [SerializeField] private GameObject audioController;
    [SerializeField] private string path;
    [SerializeField] private string file;
    private string curText = "";
    private bool changed;
    private FileSystemWatcher watcher;
    // private AudioControl audioControl;

    [SerializeField]
    private GameObject textUI;
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject translator;
    private Translator translation;


    void Start()
    {
        text = textUI.GetComponent<TextMeshProUGUI>();
        // audioControl = audioController.GetComponent<AudioControl>();
        translation = translator.GetComponent<Translator>();
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


    private void OnDisable()
    {
        if(watcher != null)
        {
            watcher.Changed -= OnChanged;
            watcher.Dispose();
        }
    }


    // private void Update()
    // {
    //     if (changed)
    //     {
    //         text.text = curText;
    //     }
    // }


    private void OnChanged(object source, FileSystemEventArgs e)
    {
        curText = "";
        Debug.Log("파일 변화 감지");
        // changed = true;
        // // // on Changed 감지 되면 해당 skel 값을 bone animation에 적용할 수 있도록 짜기 
        // StreamReader sr = new StreamReader(Path.Combine(path, file));
        // curText = sr.ReadToEnd();
        
    }

}