using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FigureType {
    BELL,
    CHERRY,
    GRAPES,
    LEMON,
    ORANGE,
    PLUM,
    WATERMELON
}
public class Figure : MonoBehaviour
{
    [SerializeField]
    private FigureType figureType;

    public FigureType GetFigureType(){
        return figureType;
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
