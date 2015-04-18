using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

public class LoadLevel : MonoBehaviour {

	public TextAsset levelXml;
	public bool loadAtStartUp;

	// Use this for initialization
	void Start () {
		if(loadAtStartUp) {
			//Destroy all existing Children and reload
			ClearLevel();
			InstantiateLevel();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearLevel() {
		foreach(Transform child in transform) {
			Destroy(child.gameObject);
		}
	}

	public void ClearLevelFromEditor() {
		List<Transform> tempList = transform.Cast<Transform>().ToList();
		foreach(Transform child in tempList) {
			DestroyImmediate(child.gameObject);
		}
	}

	public void InstantiateLevel() {
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(levelXml.text);
		XmlNodeList levelsList = xmlDoc.GetElementsByTagName("level"); // array of the level nodes.
		
		foreach(XmlNode levelInfo in levelsList) {
			XmlNodeList levelContent = levelInfo.ChildNodes;
			
			foreach(XmlNode levelItems in levelContent) {
				if(levelItems.Name == "Tiles") {
					//get Attribuites for level
					string tilesetName = levelItems.Attributes["tileset"].Value;
					
					foreach (XmlNode levelTile in levelItems.ChildNodes) {
						if(levelTile.Name == "tile") {
							int tileX = int.Parse(levelTile.Attributes["x"].Value);
							//-y values because OGMO's axis starts in the upper left and not lower left.
							int tileY = -int.Parse(levelTile.Attributes["y"].Value);
							int id = int.Parse(levelTile.Attributes["id"].Value);
							
							//convert these to cases?
							//More possible tiles
							//Note, in order to use Resources.load, the prefab needs to be in the Resources folder
							if(id == 7) {
								GameObject tile = Instantiate(Resources.Load("BasicTile"), new Vector3(transform.position.x +(tileX), transform.position.y +(tileY), 0), transform.rotation) as GameObject;
								tile.transform.parent = transform;
							} else if(id == 20) {
								GameObject tile = Instantiate(Resources.Load("GrassTile"), new Vector3(transform.position.x +(tileX), transform.position.y +(tileY), 0), transform.rotation) as GameObject;
								tile.transform.parent = transform;
							} else if(id == 30) {
								GameObject tile = Instantiate(Resources.Load("RoundedTile"), new Vector3(transform.position.x +(tileX), transform.position.y +(tileY), 0), transform.rotation) as GameObject;
								tile.transform.parent = transform;
							}
						}
					}
				}
				
				if(levelItems.Name == "Entities") {
					foreach(XmlNode levelEntities in levelItems) {
						//Do something with entities
						//obj.Add ("entities", levelEntities.InnerXml);
					}
				}
			}
		}
	}
}
