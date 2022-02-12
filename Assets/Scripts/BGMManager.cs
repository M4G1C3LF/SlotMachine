using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BGMManager : AudioManager
{
    public Track[] tracks;
    int currentTrack;
    AudioSource playingTrack;
    bool isPlaying;
    public static BGMManager GetBGMManager() {
         return GameObject.FindWithTag(Tags.BGM_MANAGER).GetComponent<BGMManager>();
      }
    public void PlayTrack(int trackNumber){
        currentTrack = trackNumber;
        playingTrack = PlayClip(tracks[currentTrack].GetAudioClip());
        isPlaying = true;
    }
    public void StopMusic(){
        isPlaying = false;
        playingTrack = null;
        foreach(AudioSource aS in audioSources){
            if (aS.isPlaying){
                aS.Stop();
            }
        }
    }
    public void PlayNextTrack(){
        playingTrack.Stop();
        currentTrack++;
            
        if(currentTrack == tracks.Length)
            currentTrack = 0;
        PlayTrack(currentTrack);
    }
    
    void CheckTrackEnding(){
        if(playingTrack != null && !playingTrack.isPlaying) {
            PlayNextTrack();
        }
        
    }
    private void OnEnable() {
        audioSources = GetComponentsInChildren<AudioSource>();
        currentTrack = 0;
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
            CheckTrackEnding();
    }
   
}
