using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CustomButton : Button, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData){
        GetComponent<Animator>().SetBool(AnimatorParameters.BUTTON_IS_CLICKED, true);
        
    }
    public void OnPointerUp(PointerEventData eventData){
        GetComponent<Animator>().SetBool(AnimatorParameters.BUTTON_IS_CLICKED, false);
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }
}
