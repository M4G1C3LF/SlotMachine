using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Track : MonoBehaviour {
        public AudioClip audioClip;
        public string songName;
        public string artist;
        Track (AudioClip audioClip, string songName, string artist){
            this.audioClip = audioClip;
            this.songName = songName;
            this.artist = artist;
        }
        public AudioClip GetAudioClip(){
            return audioClip;
        }
        public string GetName(){
            return songName;
        }
        public string GetArtist(){
            return artist;
        }
        
    }