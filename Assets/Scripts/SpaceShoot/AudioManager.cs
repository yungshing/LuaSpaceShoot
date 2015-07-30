using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[System.Serializable]
//public class AudioItem
//{
//    public string name;
//    public AudioClip audio;
//}
public class AudioManager : MonoBehaviour {

    public Dictionary<string ,AudioClip> audios;

    [HideInInspector]
    public AudioManager instance;
    void Start()
    {
        instance = this;
        InitAudios();
        DontDestroyOnLoad(gameObject);
    }

    private void InitAudios()
    {
        this.audios = new Dictionary<string, AudioClip>();
        AudioClip audioShoot = Resources.Load("Audio/Shoot") as AudioClip;
        AudioClip audioExplode = Resources.Load("Audio/Explode") as AudioClip;
        audios.Add("Shoot", audioShoot);
        audios.Add("Explode", audioExplode);
    }


    public void playAudio(string name)
    {
        if (audios.ContainsKey(name))
        {
            if (name == "Shoot")
            {
                AudioSource.PlayClipAtPoint(audios[name], transform.position, 0.2f);

            }
            else
            {
                AudioSource.PlayClipAtPoint(audios[name], transform.position, 1f);
            }
        }
    }
}
