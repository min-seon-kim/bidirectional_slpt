using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class audioWatcher : MonoBehaviour
{
    private FileSystemWatcher watcher;
    private AudioSource audioSource;
    private bool play = false;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (File.Exists(Path.Combine("./Assets/Resources/", "voice.mp3")))
        {
            Debug.Log("file exists###################3");
        }
        watcher = new FileSystemWatcher();
        watcher.Path = "./Assets/Resources/";
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
        
        play = true;
    }

    void Update()
    {
        if(play)
        {
            int i = 0;
            
            AudioClip audioAsset = (AudioClip)Resources.Load("voice"); //myfile.mp3
            if(audioAsset != null)
            {
                audioSource.clip = (AudioClip)audioAsset;
                audioSource.Play();
                play = false;
            }
            
        }
    }
}
