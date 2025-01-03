using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public const string audioName = "voice.mp3"; // audio file name
    public bool fileChange = false;

    public AudioSource audioSource;
    public AudioClip audioClip;

    public string soundPath;

    private void Awake() {
        audioSource = gameObject.AddComponent<AudioSource>();
        // soundPath = "/home/minseon/Desktop/nslt/Data"; // file path for audio
        
    }

    void Update()
    {
        if(fileChange)
        {
            StartCoroutine(LoadAudio());
        }
    }

    private IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile(soundPath, audioName);
        yield return request;

        audioClip = request.GetAudioClip();
        audioClip.name = audioName;

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = false;
        audioSource.mute = false;

        fileChange = false;
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path+ "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }
}
