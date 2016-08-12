using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class RotationController : MonoBehaviour {
	public Camera cam;
	void Awake(){
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){


	}

	void OnDestroy(){
		cam.gameObject.transform.rotation = new Quaternion (0,0,0,0);
	}
}
