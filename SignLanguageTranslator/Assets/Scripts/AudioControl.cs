using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class AudioControl : MonoBehaviour
{
    public bool fileChange = false;
    private AudioSource audioSource;
    [SerializeField]
    private string src;
    [SerializeField]
    private string dest;
    private FileSystemWatcher watcher;
    // Start is called before the first frame update
    private bool play = false;
    private int voiceIndex;

    // delete future
    [SerializeField]
    private GameObject textUI;
    private TextMeshProUGUI text;
    [SerializeField]
    private List<string> textList;
    private int textIndex;
    

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        voiceIndex = 0;
        textIndex = 0;
        text = textUI.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("get key down");
            PlayAudio();
            
            text.text = textList[textIndex];
            textIndex += 1;

            // AudioClip audioAsset = (AudioClip)Resources.Load("voice"); //myfile.mp3
            // audioSource.clip = (AudioClip)audioAsset;
            // audioSource.Play();
        }

        // if(fileChange)
        // {
        //     PlayAudio();
        // }

        // if(play)
        // {
        //     // AudioClip audioAsset = (AudioClip)Resources.Load("voice"); //myfile.mp3
        //     // audioSource.clip = (AudioClip)audioAsset;
        //     // audioSource.Play();
        //     play = false;
        // }

    }

    private void PlayAudio()
    {
        voiceIndex += 1;
        Debug.Log("play audio");

        string filePath = src;

        // if(File.Exists(filePath))
        // {
        //     Debug.Log(filePath);
        //     File.Delete(dest);
        //     File.Copy(filePath, dest);

        //     play = true;
        // }
        string fileName = "voice" + voiceIndex.ToString();
        AudioClip audioAsset = (AudioClip)Resources.Load(fileName); //myfile.mp3
        if(audioAsset != null)
        {
            audioSource.clip = (AudioClip)audioAsset;
            audioSource.Play();
            // play = false;
        }
       
        fileChange = false;

        // 
    }

    private void OnEnable()
    {
        if (File.Exists(Path.Combine("/home/minseon/Desktop/nslt/Data/Video/", "voice.mp3")))
        {
            Debug.Log("file exists###################3");
        }
        watcher = new FileSystemWatcher();
        watcher.Path = "/home/minseon/Desktop/nslt/Data/Video/";
        watcher.Filter = "voice.mp3";

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


    private void OnChanged(object source, FileSystemEventArgs e)
    {
        Debug.Log("change");

        fileChange = true;
    }

}
