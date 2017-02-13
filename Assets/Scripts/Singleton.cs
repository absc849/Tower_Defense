
using UnityEngine;

public class Singleton<T>: MonoBehaviour where T : MonoBehaviour {
	// if we type in GameManager as a type we are saying that game manager is a monobehavior
	private static T instance;

	public static T Instance{
	get{
//if we pass in game manager and theres no instance of it then we shall create one
		if(instance == null)
			instance = FindObjectOfType<T>();
		else if(instance != FindObjectOfType<T>())
			Destroy(FindObjectOfType<T>());
		
		DontDestroyOnLoad(FindObjectOfType<T>());

			return instance;
		}
	}


	
}
