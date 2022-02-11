using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReelDirection {
    UPWARDS,
    DOWNWARDS
}
public class Reel : MonoBehaviour
{
    [SerializeField]
    private float spinningSpeed;
    [SerializeField]
    private ReelDirection direction;
    public float distanceBetweenFigures;
    private List<GameObject> figures;
    public bool isSpinning;
    
    private void OnEnable() {
        GetAllFiguresOnReel();
        SetDistanceBetweenFigures();
    }
    private void GetAllFiguresOnReel(){
        figures = Utils.FindChildrensWithTag(gameObject,Tags.FIGURE);
    }
    public List<GameObject> GetFigures(){
        return figures;
    }
    public Figure GetFigureAtPosition(int position){
        return figures[position].GetComponent<Figure>();
    }
    public void SetDistanceBetweenFigures(){
        int i = 0;
        figures.ForEach(figure => {
            figure.transform.localPosition = new Vector2(0f,-distanceBetweenFigures*i);
            i++;
        });
    }
    public float GetDistanceBetweenFigures(){
        return distanceBetweenFigures;
    }
    private void DoSpinMotion(){
        Vector3 dir = direction == ReelDirection.UPWARDS ? Vector3.up : Vector3.down;

        figures.ForEach(figure => {
            if (dir == Vector3.up && HasToTranslateToBottom(figure)){
                TranslateFigureToBottom(figure);
                return;    
            }
            if (dir == Vector3.down && HasToTranslateToTop(figure)){
                TranslateFigureToTop(figure);
                return;    
            }
            figure.transform.localPosition += dir * spinningSpeed * Time.deltaTime;
        });
    }
    private bool HasToTranslateToTop(GameObject figure){
        return (figure.transform.localPosition.y <= (figures.ToArray().Length) * -distanceBetweenFigures);
    }
    private bool HasToTranslateToBottom(GameObject figure){
        return (figure.transform.localPosition.y >= 0f);
    }
    private void TranslateFigureToTop(GameObject figure){
        float higherPositionFound = float.PositiveInfinity;
        figures.ForEach(fig => {
            if (higherPositionFound == float.PositiveInfinity || fig.transform.localPosition.y > higherPositionFound)
                higherPositionFound = fig.transform.localPosition.y;
        });
        figure.transform.localPosition = new Vector2(0f, higherPositionFound + distanceBetweenFigures);
    }
    private void TranslateFigureToBottom(GameObject figure){
        float lowerPositionFound = float.NegativeInfinity;
        figures.ForEach(fig => {
            if (lowerPositionFound == float.NegativeInfinity || fig.transform.localPosition.y < lowerPositionFound)
                lowerPositionFound = fig.transform.localPosition.y;
        });
        figure.transform.localPosition = new Vector2(0f, lowerPositionFound - distanceBetweenFigures);
    }

    public void StartSpinning(){
        isSpinning = true;
    }
    public void StopSpinning(){
        isSpinning = false;
        
        SortFiguresByDescendingPosition();
        SetFiguresToCorrectPosition();
        
    }
    private void SortFiguresByDescendingPosition(){
        figures.Sort(delegate(GameObject a, GameObject b)
        {
            if (a == null && b == null) return 0;
            else if (a == null) return -1;
            else if (b == null) return 1;
            else return b.transform.localPosition.y.CompareTo(a.transform.localPosition.y);
        });
    }
    private void SetFiguresToCorrectPosition(){
        List<float> yPositions = GetFiguresYPositions();
        int j = 0;
        figures.ForEach(figure =>{
            figure.transform.localPosition = new Vector3(0f,yPositions[j]);
            j++;
        });
    }
    private List<float> GetFiguresYPositions(){
        List<float> yPositions = new List<float>();
        for (int i = 0; i<figures.ToArray().Length;i++){
            yPositions.Add(-distanceBetweenFigures*i);
        }
        return yPositions;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinning){
            DoSpinMotion();
        }
        
    }
}
