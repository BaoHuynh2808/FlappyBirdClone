using UnityEngine;
using System.Collections;

public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object ();

	public static T Instance {
		get {
			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = (T)FindObjectOfType (typeof(T));

					if (FindObjectsOfType (typeof(T)).Length > 1)
					{
						Debug.LogError (typeof(T) + " was instantiated more than once.");
						return _instance;
					}

					if (_instance == null)
					{
						GameObject singleton = new GameObject ();
						_instance = singleton.AddComponent<T> ();
						singleton.name = "[SceneSingleton] " + typeof(T).ToString ();

//						Debug.Log ("[Singleton] An instance of " + typeof(T) + " was created.");
					}
				}

				return _instance;
			}
		}
	}
}

