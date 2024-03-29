using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    private List<GameObject> activeAudioGameObjects;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            activeAudioGameObjects = new List<GameObject>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public AudioSource PlayAudio(AudioClip clip, float volume = 1)
    {
        GameObject sourceObj = new GameObject(clip.name); // Crear nuevo objeto
        activeAudioGameObjects.Add(sourceObj); // Lo a�ade al objeto
        sourceObj.transform.SetParent(this.transform); //Lo convierte en hijo del AudioManager
        AudioSource source = sourceObj.AddComponent<AudioSource>(); // A�ade el componente del audio
        source.clip = clip;
        source.volume = volume;
        source.Play();
        StartCoroutine(PlayAudio(source));
        return source;
    }
    public AudioSource PlayAudioOnLoop(AudioClip clip, float volume = 1)
    {
        GameObject sourceObj = new GameObject(clip.name);
        activeAudioGameObjects.Add(sourceObj);
        sourceObj.transform.SetParent(this.transform);
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.Play();
        return source;
    }
    public void ClearAudioList()
    {
        foreach (GameObject go in activeAudioGameObjects)
        {
            Destroy(go);
        }
        activeAudioGameObjects.Clear();
    }
    IEnumerator PlayAudio(AudioSource source)
    {
        while (source && source.isPlaying)
        {
            yield return null;
        }
        if (source)
        {
            activeAudioGameObjects.Remove(source.gameObject);
            Destroy(source.gameObject);
        }
    }
}
