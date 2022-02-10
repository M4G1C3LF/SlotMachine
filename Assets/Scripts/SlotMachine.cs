using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField]
    private MinMaxFloat rangeToStartStoppingRollers;
    [SerializeField]
    private float rollerSpinningFrequency;
    [SerializeField]
    private float rollerStoppingFrequency;
    [SerializeField]
    private Timer rollerSpinningFrequencyTimer;
    [SerializeField]
    private Timer startStoppingRollersTimer;
    [SerializeField]
    private Timer rollerStoppingFrequencyTimer;
    
    private List<Reel> reels;

    // DEVELOPMENT VARS

    public bool spinButtonPressed;

    //

    private void OnEnable() {
        reels = GetReels();
    }
    private List<Reel> GetReels(){
        List<Reel> reels = new List<Reel>();
        Reel[] reelArray = GetComponentsInChildren<Reel>();
        foreach (Reel reel in reelArray){
            reels.Add(reel);
        }
        return reels;
    }
    private void SpinReels(){
        spinButtonPressed = false;
        StartCoroutine(SpinMotion());
    }
    private IEnumerator SpinMotion(){
        rollerSpinningFrequencyTimer.InitializeTimer();
        rollerSpinningFrequencyTimer.SetSeconds(0);

        foreach (Reel reel in reels){
            rollerSpinningFrequencyTimer.StartTimer();
            while (rollerSpinningFrequencyTimer.GetTimeInSeconds() > 0f){
                yield return null;
            }
            reel.StartSpinning();
            rollerSpinningFrequencyTimer.StopTimer();
            rollerSpinningFrequencyTimer.InitializeTimer();
            rollerSpinningFrequencyTimer.SetSeconds(rollerSpinningFrequency);
        }
        rollerSpinningFrequencyTimer.InitializeTimer();
        

        startStoppingRollersTimer.InitializeTimer();
        startStoppingRollersTimer.SetSeconds(Random.Range(rangeToStartStoppingRollers.GetMin(),rangeToStartStoppingRollers.GetMax()));
        startStoppingRollersTimer.StartTimer();
        while (startStoppingRollersTimer.GetTimeInSeconds() > 0f){
            yield return null;
        }
        startStoppingRollersTimer.StopTimer();

        rollerStoppingFrequencyTimer.InitializeTimer();
        rollerStoppingFrequencyTimer.SetSeconds(0);
        foreach (Reel reel in reels){
            rollerStoppingFrequencyTimer.StartTimer();
            while (rollerStoppingFrequencyTimer.GetTimeInSeconds() > 0f){
                yield return null;
            }
            reel.StopSpinning();
            rollerStoppingFrequencyTimer.StopTimer();
            rollerStoppingFrequencyTimer.InitializeTimer();
            rollerStoppingFrequencyTimer.SetSeconds(rollerStoppingFrequency);
        }
        rollerStoppingFrequencyTimer.InitializeTimer();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spinButtonPressed)
            SpinReels();
    }
}
