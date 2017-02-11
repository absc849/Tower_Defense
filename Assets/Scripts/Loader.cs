using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	// Use this for initialization

	public GameObject gameManager;

	 /// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if(GameManager.instance == null){
			Instantiate(gameManager);
		}

	}
		

	
	// Update is called once per frame
	void Update () {
		
	}
}
