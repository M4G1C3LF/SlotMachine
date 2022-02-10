using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using UnityEngine.UI;

public static class Utils {
	/*
	 *	This method returns a GameObject.
	 *	It will iterate for each gameObject, if the tag matches, it will return that object.
	 */

	public static GameObject FindChildrenWithTag(GameObject gO, string tag){
		Transform[] gameObjects = gO.GetComponentsInChildren<Transform> (true);
		foreach (Transform child in gameObjects) {
			if (child.tag == tag) {
				return child.gameObject;
			}
		}
		return null;
	}

	public static List<GameObject> FindChildrensWithTag(GameObject gO, string tag){
		List<GameObject> foundList = new List<GameObject>();
		Transform[] gameObjects = gO.GetComponentsInChildren<Transform> (true);
		foreach (Transform child in gameObjects) {
			if (child.tag == tag) {
				foundList.Add(child.gameObject);
			}
		}
		return foundList;
	}
}