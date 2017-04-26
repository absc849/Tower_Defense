using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

	public TowerBtn towerBtnPressed{get; set;}
	private SpriteRenderer spriteRenderer;



	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			// Transforms position from screen space into world space.
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// raycasts, pretty much shoot a line into the scene until it hits something 
			RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);
			if(hit.collider.tag == "BuildSite" && towerBtnPressed != null){
				PlaceTower(hit);
			}
		}

		if(spriteRenderer.enabled){
				FollowMouse();
				transform.position = new Vector2(transform.position.x, transform.position.y);
			}
	}
	public void PlaceTower(RaycastHit2D hit){
		//if pointer over game object is false and the tower button has been pressed this will work
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null){
			hit.collider.tag = "BuildSiteFull";
			GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
			DisableDragSprite();
		}
	}


	public void SelectedTower(TowerBtn towerSelected){
		towerBtnPressed = towerSelected;
		EnableDragSprite(towerBtnPressed.DragSprite);
	}

	public void FollowMouse(){
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//transform.position = new Vector2(transform.position.x, transform.position.y);
	}

	public void EnableDragSprite(Sprite sprite){
		spriteRenderer.enabled = true;
		spriteRenderer.sprite = sprite;

	}

	public void DisableDragSprite(){
		spriteRenderer.enabled = false;

	}
}
