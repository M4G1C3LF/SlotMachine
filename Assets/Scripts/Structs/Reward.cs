using UnityEngine;
[System.Serializable]
public struct Reward
{
    [SerializeField]
    private int occurrences;
    [SerializeField]
    private FigureType figureType;
    [SerializeField]
    private int credits;
    public int GetOccurrences(){
        return occurrences;
    }
    public FigureType GetFigureType(){
        return figureType;
    }
    public int GetCredits(){
        return credits;
    }
    public Reward(int occurrences, FigureType figureType,int credits) { 
        this.occurrences = occurrences;
        this.figureType = figureType; 
        this.credits = credits;
    }
}
