using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payline : MonoBehaviour 
{
    public bool[] row1 = new bool[Constants.REEL_SIZE];
    public bool[] row2 = new bool[Constants.REEL_SIZE];
    public bool[] row3 = new bool[Constants.REEL_SIZE];

    public int GetActiveRowForPosition(int position){
        try {
            if (row1[position]) 
                return -1;
            if (row2[position]) 
                return 0;
            if (row3[position]) 
                return 1;
        } catch(System.IndexOutOfRangeException e){
            return int.MinValue;
        }
        return int.MinValue;
    }

}