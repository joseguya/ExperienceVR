using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class OverlayObjectsController : MonoBehaviour {

	[SerializeField] private GameObject[] objects;
	[SerializeField] private MediaPlayerCtrl mediaPlayer; 
	[SerializeField] private GameObject gvrCamera;
	private int duration;
	private float percentage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		duration = mediaPlayer.GetDuration ();
	}

	void FixedUpdate(){
		
		if (mediaPlayer.GetSeekPosition() >= 5800){
			for (int i = 0;i<objects.Length;i++){
				objects[i].SetActive (true);
				//objects[i].transform.rotation = objects[i].transform.rotation * Quaternion.Euler(0,-10,0); 
			}
		}

		if (mediaPlayer.GetSeekPosition() >= 53000){
			for (int i = 0;i<objects.Length;i++){
				objects[i].SetActive (false);
			}
		}
		percentage = ((float)mediaPlayer.GetSeekPosition () / (float)duration) *100.0f;
		if((percentage) >= 99){

			SceneManager.LoadScene("menu", LoadSceneMode.Single);
		}
	}
}
