using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    protected AudioSource[] audioSources;
    
    AudioSource GetFreeAudioSource(){
        foreach(AudioSource aS in audioSources){
            if (!aS.isPlaying){
                return aS;
            }
        }
        return null;
    }
    protected AudioSource PlayClip(AudioClip clip){
        AudioSource aS = GetFreeAudioSource();
        if (aS){
            aS.clip = clip;
            aS.Play();
            return aS;
        } 
        Debug.Log("Can't reproduce audioClip because all audioSources are busy");
        return null;
        
    }  
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
