using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	public int hours;
	public int minutes;
	public float seconds;
	public bool isIncrementingHours;
	public bool isIncrementingMinutes;

    [SerializeField]
	bool isRunning;
	//This parameter sets the behaviour of the Class.
	public bool isDecrementalTimer;

	public void SetTimer(int hours, int minutes, float seconds){
		this.hours = hours;
		this.minutes = minutes;
		this.seconds = seconds;
	}
	public void SetTimer(float seconds){
		this.hours = Mathf.FloorToInt(seconds / (60*2));
		this.minutes = Mathf.FloorToInt(seconds/60) - (this.hours*60);
		this.seconds = seconds - (this.hours *(60*2)) - (this.minutes * 60);


	}
    public bool IsRunning(){
        return isRunning;
    }

	public void SetHours(int value){
		hours = value;
	}
	public int GetHours(){
		return hours;
	}
	public void SetMinutes(int value){
		minutes = value;
	}
	public int GetMinutes(){
		return minutes;
	}
	public void SetSeconds(float value){
		seconds = value;
	}
	public float GetSeconds(){
		return seconds;
	}
	public string GetFormattedHours(){
		return hours.ToString();
	}
	public string GetFormattedMinutes(){
		return minutes < 10 ? "0"+minutes.ToString() : minutes.ToString();
	}
	public string GetFormattedSeconds(){
		return seconds < 10 ? "0"+Mathf.Floor(seconds).ToString() : Mathf.Floor(seconds).ToString();
	}
	public string GetFormattedMilliseconds(){
		try {
			return (seconds - Mathf.Floor(seconds)).ToString().Substring(1,4);
		} catch (System.ArgumentOutOfRangeException e){
			return (seconds - Mathf.Floor(seconds)).ToString().Substring(1)+",000";
		}
		
	}
	public float GetTimeInSeconds(){
		return seconds + minutes * 60 + hours * 60 * 60;
	}
	public void StopTimer(){
		isRunning = false;
	}
	public void StartTimer(){
		isRunning = true;
	}
	public string GetFormattedTime(){
		return	(hours > 0 ? GetFormattedHours()+":" : "") +
				GetFormattedMinutes()+":" +
				GetFormattedSeconds();
	}
	void IncrementTimer(){
		seconds += Time.deltaTime;
		if (isIncrementingMinutes && seconds >= 60f) {
			seconds -= 60f;
			minutes++;
		}
		if (isIncrementingHours && minutes >= 60f) {
			minutes -= 60;
			hours++;
		}
	}

	void DecrementTimer(){
		if (seconds - Time.deltaTime <= 0f) {
			seconds = 0f;
			return;
		}
			
		seconds -= Time.deltaTime;
		
		if (seconds <= 0f) {
			seconds += 60f;
			minutes--;
		}
		if (minutes < 0) {
			minutes += 60;
			hours--;
		}
	}
	public void InitializeTimer(){
		hours = 0;
		minutes= 0;
		seconds= 0f;
	}
	// Use this for initialization
	void Start () {
		isRunning = false;
		//InitializeTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning){
			if (isDecrementalTimer) {
				DecrementTimer ();
			} else {
				IncrementTimer ();
			}
		}
		
	}
}
