using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string nameSound;
    public AudioClip clip;
    public float volume = 1;
    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : MonoBehaviour {
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    public List<Sound> sounds;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        sounds.ForEach(x =>
        {
            x.source = gameObject.AddComponent<AudioSource>();
            x.source.volume = 0;
            x.source.playOnAwake = false;
            x.source.clip = x.clip;

        });
    }

    public void PlaySound(string nameSound)
    {
        Sound snd = sounds.Where(x => x.nameSound == nameSound).FirstOrDefault();
     
        if (snd!=null)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            temp.clip = snd.clip;
            temp.volume = snd.volume;
            temp.Play();
            DOVirtual.DelayedCall(snd.clip.length - 0.1f, () => { temp.DOFade(0, 0.1f); Destroy(temp); });
        }
    }
   
}
