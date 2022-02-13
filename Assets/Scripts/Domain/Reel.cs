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
    private List<Figure> figures;
    private bool isSpinning;
    
    private void OnEnable() {
        GetAllFiguresOnReel();
        SetDistanceBetweenFigures();
    }
    private void GetAllFiguresOnReel(){
        figures = new List<Figure>();
        Utils.FindChildrensWithTag(gameObject,Tags.FIGURE).ForEach( figure => {
            figures.Add(figure.GetComponent<Figure>());
        });
        
    }
    public List<Figure> GetFigures(){
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
            if (dir == Vector3.up && HasToTranslateToBottom(figure.gameObject)){
                TranslateFigureToBottom(figure.gameObject);
                return;    
            }
            if (dir == Vector3.down && HasToTranslateToTop(figure.gameObject)){
                TranslateFigureToTop(figure.gameObject);
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
        SFXManager.GetSFXManager().PlayStartReelSpin();
    }
    public void StopSpinning(){
        isSpinning = false;
        SFXManager.GetSFXManager().PlayStopReelSpin();
        figures = SortFigureByDescendingPosition(figures);
        figures = SetFiguresToCorrectPosition(figures);
        
    }
    public List<Figure> SortFigureByDescendingPosition(List<Figure> list){
        list.Sort(delegate(Figure a, Figure b)
        {
            if (a == null && b == null) return 0;
            else if (a == null) return -1;
            else if (b == null) return 1;
            else return b.transform.localPosition.y.CompareTo(a.transform.localPosition.y);
        });
        return list;
    }
    public List<Figure> SetFiguresToCorrectPosition(List<Figure> list){
        List<float> yPositions = GetFiguresYPositions();
        int i = 0;
        list.ForEach(element =>{
            element.transform.localPosition = new Vector3(0f,yPositions[i]);
            i++;
        });
        return list;
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
    public Reel(float spinningSpeed, ReelDirection direction, float distanceBetweenFigures, List<Figure> figures){
        this.spinningSpeed = spinningSpeed;
        this.direction = direction ;
        this.distanceBetweenFigures = distanceBetweenFigures;
        this.figures = figures;

    }
}
