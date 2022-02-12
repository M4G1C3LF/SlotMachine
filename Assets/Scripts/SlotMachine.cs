using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [SerializeField]
    private int credits;
    [SerializeField]
    private int creditsPerSpin;
    [SerializeField]
    private int centralPosition;
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
    [SerializeField]
    private List<Payline> paylines;
    private List<Reel> reels;
    [SerializeField]
    private List<Reward> rewards;
    [SerializeField]
    private GameObject spinButton;
    [SerializeField]
    private GameObject addCreditsButton;
    private bool isSpinning;

    private void OnEnable() {
        reels = GetReelsInChildren();
        paylines = GetPaylinesInChildren();
        addCreditsButton.GetComponent<Animator>().SetBool(AnimatorParameters.BUTTON_IS_ENABLED, true);
    }

    private void CheckSpinButton(){
        if (credits >= creditsPerSpin && !isSpinning)
            spinButton.GetComponent<Animator>().SetBool(AnimatorParameters.BUTTON_IS_ENABLED, true);
    }
    public List<Reel> GetReelsInChildren(){
        List<Reel> reels = new List<Reel>();
        Reel[] reelArray = GetComponentsInChildren<Reel>();
        foreach (Reel reel in reelArray){
            reels.Add(reel);
        }
        return reels;
    }
    public List<Payline> GetPaylinesInChildren(){
        List<Payline> paylines = new List<Payline>();
        Payline[] paylinesArray = GetComponentsInChildren<Payline>();
        foreach (Payline payline in paylinesArray){
            paylines.Add(payline);
        }
        return paylines;
    }
    public void SpinReels(){
        if (credits >= creditsPerSpin){
            credits -= creditsPerSpin;
            StartCoroutine(SpinMotion());
        }
        else
            Debug.Log("Not enought credits to play...");
    }

    private void DisableLightsOnElements(){
        Utils.FindChildrensWithTag(gameObject,Tags.FIGURE).ForEach( figure => {
            Animator animator = figure.GetComponent<Animator>();
            animator.SetBool(AnimatorParameters.FIGURE_IS_ENABLED,false);
            
        });
        Utils.FindChildrensWithTag(gameObject,Tags.UI_REWARD).ForEach( reward => {
            Animator animator = reward.GetComponent<Animator>();
            animator.SetBool(AnimatorParameters.LOW_REWARD_IS_ENABLED,false);
            animator.SetBool(AnimatorParameters.MID_REWARD_IS_ENABLED,false);
            animator.SetBool(AnimatorParameters.HIGH_REWARD_IS_ENABLED,false);
            
        });
    }
    public void Add50Credits(){
        credits += 50;
        CheckSpinButton();
    }
    private IEnumerator SpinMotion(){
        isSpinning = true;
        DisableLightsOnElements();
        spinButton.GetComponent<Animator>().SetBool(AnimatorParameters.BUTTON_IS_ENABLED, false);
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
        isSpinning = false;

        CheckPaylines();
        CheckSpinButton();
    }

    private void CheckPaylines(){

        List<Combination> totalCombinations = new List<Combination>();
        paylines.ForEach(payline => {
            List<int> paylineRows = new List<int>();
            for (int i = 0; i<reels.ToArray().Length;i++){
                paylineRows.Add(payline.GetActiveRowForPosition(i));
            }
            
            int j = 0;
            List<Figure> paylineFigures = new List<Figure>();
            reels.ForEach(reel => {
                paylineFigures.Add(reel.GetFigureAtPosition(centralPosition+paylineRows[j]));
                j++;
            });
           
            List<Combination> combinationsFoundInPayline = new List<Combination>();
            Figure lastFigureFound = null;
            paylineFigures.ForEach(figure => {
                
                foreach (Combination combination in combinationsFoundInPayline){    
                    if (lastFigureFound != null && lastFigureFound.GetFigureType().Equals(figure.GetFigureType()) && combination.GetFigureType().Equals(figure.GetFigureType())){
                        combination.AddFigure(figure);
                        lastFigureFound = figure;
                        return;
                    }
                }
                if (lastFigureFound != null && lastFigureFound.GetFigureType().Equals(figure.GetFigureType())){
                    List<Figure> figureList = new List<Figure>();
                    figureList.Add(lastFigureFound);
                    figureList.Add(figure);
                    combinationsFoundInPayline.Add(new Combination(figureList));
                }
                    

                lastFigureFound = figure;   
            });
            totalCombinations.AddRange(combinationsFoundInPayline);
            
        });

        Debug.Log("Combinations Found");
        rewards.ForEach(reward => {
            foreach (Combination combination in totalCombinations){
                
                if (reward.GetFigureType().Equals(combination.GetFigureType()) && reward.GetOccurrences().Equals(combination.GetOccurrences())){
                    combination.GetFigures().ForEach( figure => {
                        Animator figureAnimator = figure.GetComponent<Animator>();
                        figureAnimator.SetTrigger(AnimatorParameters.FIGURE_BLINK);
                        figureAnimator.SetBool(AnimatorParameters.FIGURE_IS_ENABLED,true);
                    });
                    GameObject rewardUIElement = reward.GetElementOnUI();
                    Animator rewardAnimator = rewardUIElement.GetComponentInParent<Animator>();
                    switch (rewardUIElement.name){
                        case "LowReward":
                            rewardAnimator.SetTrigger(AnimatorParameters.LOW_REWARD_BLINK);
                            rewardAnimator.SetBool(AnimatorParameters.LOW_REWARD_IS_ENABLED,true);
                            break;
                        case "MidReward":
                            rewardAnimator.SetTrigger(AnimatorParameters.MID_REWARD_BLINK);
                            rewardAnimator.SetBool(AnimatorParameters.MID_REWARD_IS_ENABLED,true);
                            break;
                        case "HighReward":
                            rewardAnimator.SetTrigger(AnimatorParameters.HIGH_REWARD_BLINK);
                            rewardAnimator.SetBool(AnimatorParameters.HIGH_REWARD_IS_ENABLED,true);
                            break;
                    }
                    Animator figureRewardAnimator = Utils.FindChildrenWithTag(rewardAnimator.gameObject,Tags.FIGURE).GetComponent<Animator>();

                    figureRewardAnimator.SetTrigger(AnimatorParameters.FIGURE_BLINK);
                    figureRewardAnimator.SetBool(AnimatorParameters.FIGURE_IS_ENABLED,true);

                    Debug.Log(combination.GetOccurrences()+" "+combination.GetFigureType()+" - Credits earned: "+reward.GetCredits());
                    credits += reward.GetCredits();
                }
                    
            }
        });
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
