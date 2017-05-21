using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

	public TowerBtn towerBtnPressed{get; set;}
	private SpriteRenderer spriteRenderer;

	private List <Tower> TowerList = new List<Tower>();
	private List <Collider2D> BuildList = new List<Collider2D>();

	private Collider2D buildTile;

	//private GameObject otherObject;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		buildTile = GetComponent<Collider2D>();

	}
	
	public void RegisterBuildSite(Collider2D buildTag){
		BuildList.Add(buildTag);
	}

	public void DeregisterBuildSite(Collider2D buildTag){
		BuildList.Remove(buildTag);
	}

	public void RegisterTower(Tower tower){
		TowerList.Add(tower);
	}
	public void DeregisterTower(Tower tower){
		TowerList.Remove(tower);
	}

	public void RenameTagsBuildSites(){
		foreach(Collider2D buildTag in BuildList){
			buildTag.tag = "BuildSite";
		}
		BuildList.Clear();
	}



	public void DestroyAllTowers(){
		foreach(Tower tower in TowerList){
			Destroy(tower.gameObject);
		}
		TowerList.Clear();
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			// Transforms position from screen space into world space.
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// raycasts, pretty much shoot a line into the scene until it hits something 
			RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);
			if(hit.collider.tag == "BuildSite" && towerBtnPressed != null){
				buildTile = hit.collider;
				hit.collider.tag = "BuildSiteFull";
				RegisterBuildSite(buildTile);
				PlaceTower(hit);
			 }
			//else if(hit.collider.tag == "BuildSiteFull"  && towerBtnPressed != null && GameManager.Instance.TotalMoney >= 20){
			 	
			// 	Destroy(buildTile.gameObject);
			// 	hit.collider.tag = "BuildSite";
			// 	DeregisterBuildSite(buildTile);
			// 	DeregisterTower(towerBtnPressed.TowerObject);
			//}
		}

		if(spriteRenderer.enabled){
				FollowMouse();
				transform.position = new Vector2(transform.position.x, transform.position.y);
			}
	}
	public void PlaceTower(RaycastHit2D hit){
		//if pointer over game object is false and the tower button has been pressed this will work
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null){
		//	hit.collider.tag = "BuildSiteFull";
			Tower newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
			BuyTower(towerBtnPressed.TowerPrice);
			GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuilt);			
			RegisterTower(newTower);
			DisableDragSprite();
		}
	}

	public void BuyTower(int price){
		GameManager.Instance.SubtractMoney(price);

	}



	public void SelectedTower(TowerBtn towerSelected){
		if(towerSelected.TowerPrice <= GameManager.Instance.TotalMoney){
		towerBtnPressed = towerSelected;
		EnableDragSprite(towerBtnPressed.DragSprite);
		}
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
