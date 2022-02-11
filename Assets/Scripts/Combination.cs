using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Combination {

    [SerializeField]
    private List<Figure> figures;
    public int GetOccurrences(){
        //return occurrences;
        return figures.ToArray().Length;
    }
    public FigureType GetFigureType(){
        return figures[0].GetFigureType();
    }
    public void AddFigure(Figure figure){
        figures.Add(figure);
    }
    public List<Figure> GetFigures(){
        return figures;
    }
    public Combination(List<Figure> figures) { 
        this.figures = figures; 
    }
}
