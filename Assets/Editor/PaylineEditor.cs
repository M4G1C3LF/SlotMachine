using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(Payline))]
public class PaylineEditor : Editor
{
    public override void OnInspectorGUI(){
        //base.OnInspectorGUI();
        GUILayout.Label("Payline Pattern");
        Payline payline = (Payline) target;
        if(payline.row1.Length > 0){
            GUILayout.BeginHorizontal();
            for (int i = 0; i<payline.row1.Length; i++){
                payline.row1[i] = GUILayout.Toggle(payline.row1[i], string.Empty);
                if (payline.row1[i]){
                    payline.row2[i] = false;
                    payline.row3[i] = false;
                }
            }
            GUILayout.EndHorizontal();
        }
        if(payline.row2.Length > 0){
            GUILayout.BeginHorizontal();
            for (int i = 0; i<payline.row2.Length; i++){
                payline.row2[i] = GUILayout.Toggle(payline.row2[i], string.Empty);
                if (payline.row2[i]){
                    payline.row1[i] = false;
                    payline.row3[i] = false;
                }
            }
            GUILayout.EndHorizontal();
        }
        if(payline.row3.Length > 0){
            GUILayout.BeginHorizontal();
            for (int i = 0; i<payline.row3.Length; i++){
                payline.row3[i] = GUILayout.Toggle(payline.row3[i], string.Empty);
                if (payline.row3[i]){
                    payline.row1[i] = false;
                    payline.row2[i] = false;
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
    