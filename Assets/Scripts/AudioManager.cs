using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Array of sound clips
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds){
            //apply each sound to an audio source game component with sounds.
            s.source= gameObject.AddComponent<AudioSource>();

            //Other Components
            s.source.clip=s.clip;
            s.source.volume=s.volume;
            s.source.pitch=s.pitch;

        }
    }
    //Finds and plays sound of name upon request
    public void Play(string name){
        //Extract sound from list
        Sound s = Array.Find(sounds,sound => sound.name == name);

        //Play sound
        s.source.Play();
    }
}
