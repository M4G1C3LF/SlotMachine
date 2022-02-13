using UnityEngine;

[System.Serializable]
struct MinMaxFloat {
    [SerializeField]
    float min, max;
    public float GetMin(){
        return min;
    }
    public float GetMax(){
        return max;
    }
    public MinMaxFloat(int min, int max) { this.min = min; this.max = max; }
}