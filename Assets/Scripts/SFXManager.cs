using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SFXManager : AudioManager
{
      public AudioClip startReelSpin;
      public AudioClip stopReelSpin;
      public AudioClip noReward;
      public AudioClip reward;
      public AudioClip addCredit;
      public static SFXManager GetSFXManager() {
         return GameObject.FindWithTag("SFXManager").GetComponent<SFXManager>();
      }
      public void PlayStartReelSpin(){
         PlayClip(startReelSpin);
      }
      public void PlayStopReelSpin(){
         PlayClip(stopReelSpin);
      }
      public void PlayNoReward(){
         PlayClip(noReward);
      }
      public void PlayReward(){
         PlayClip(reward);
      }
      public void PlayAddCredit(){
         PlayClip(addCredit);
      }
      public void PlayAudioClip(AudioClip audioClip){
         PlayClip(audioClip);
      }
      public void PlayRandomAudioClipFromList(AudioClip[] clipList){
            PlayClip(clipList[Random.Range(0,clipList.Length-1)]);
      }
      void Start(){
            audioSources = GetComponentsInChildren<AudioSource>();
      }
}
