using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip quackClip;

    [SerializeField]
    private List<AudioClip> forQuit;

    int i = 0;

    public void Title()
    {
        source.PlayOneShot(quackClip);  
    }
    public void StartGame()
    {
        EventsManager_PersistenceScene.LoadNextSceneEvent();
    }
    public void QuitGame()
    {

        source.PlayOneShot(forQuit[i]);

        i++;
        if(i >= forQuit.Count) 
        {
            i = 0;
        }
    }
}
