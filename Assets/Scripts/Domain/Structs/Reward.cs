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
    [SerializeField]
    private GameObject elementOnUI;

    public int GetOccurrences(){
        return occurrences;
    }
    public FigureType GetFigureType(){
        return figureType;
    }
    public int GetCredits(){
        return credits;
    }
    public GameObject GetElementOnUI(){
        return elementOnUI;
    }
    public Reward(int occurrences, FigureType figureType,int credits, GameObject elementOnUI) { 
        this.occurrences = occurrences;
        this.figureType = figureType; 
        this.credits = credits;
        this.elementOnUI = elementOnUI;
    }
}
