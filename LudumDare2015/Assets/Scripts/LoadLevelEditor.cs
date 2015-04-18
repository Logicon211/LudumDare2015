using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LoadLevel))]
public class LoadLevelEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		LoadLevel loadLevelScript = (LoadLevel)target;
	
		if(GUILayout.Button ("Load Level")) {
			loadLevelScript.loadAtStartUp = false;
			loadLevelScript.ClearLevelFromEditor();
			loadLevelScript.InstantiateLevel();
		}

		if(GUILayout.Button ("Clear Level")) {
			loadLevelScript.ClearLevelFromEditor();
		}
	}

	// Use this for initialization
	//void Start () {
	
	//}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
#endif
