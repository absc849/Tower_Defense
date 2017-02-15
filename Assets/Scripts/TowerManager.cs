using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

	private TowerBtn towerBtnPressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			// Transforms position from screen space into world space.
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// raycasts, pretty much shoot a line into the scene until it hits something 
			RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);
			if(hit.collider.tag == "BuildSite"){
				PlaceTower(hit);
			}


		}
	}
	public void PlaceTower(RaycastHit2D hit){
		//if pointer over game object is false and the tower button has been pressed this will work
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null){
			GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
		}
	}


	public void SelectedTower(TowerBtn towerSelected){
		towerBtnPressed = towerSelected;
	}
}
