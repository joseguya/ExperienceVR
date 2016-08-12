#if UNITY_EDITOR || UNITY_EDITOR_64 || UNITY_EDITOR_OSX //only work in the Editor

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


public class ScreenShotMaker : MonoBehaviour {

    #region Variables
    //public
    public bool DontDestroy = true;
    [Tooltip("the key that will trigger a screenshot")]
    public KeyCode ScreenShotKey = KeyCode.F1;
    [Tooltip("a List of ScreenShotsSizes")]
    public List<ScreenShotSize> ScreenShotSizes = new List<ScreenShotSize>();

    //private
	private string _ScreenShotPath;
    private EditorWindow GameView;
    private Rect DefaultRec;
    #endregion

    #region Methods 

	// Use this for initialization
	void Start () 
	{
        if (DontDestroy)
        {
		    DontDestroyOnLoad(gameObject); //no not destory this GameObject
        }

		CreateScreenShotFolder(); //creates a folder to store the screenshots

		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetMainGameView.Invoke(null,null);

		GameView = (EditorWindow)Res;
        DefaultRec = GameView.position;
	}

    //creates a folder to store the screenshots
	private void CreateScreenShotFolder() 
	{
        // /Users/JustinGarza/UnityProjects/ScreenShotMaker/Assets/~/ScreenShots
		_ScreenShotPath =  Application.dataPath + "/~/ScreenShots";

        print(_ScreenShotPath);

		if (!Directory.Exists(_ScreenShotPath))
		{
			Directory.CreateDirectory(_ScreenShotPath);
		}
	}


	void Update () 
	{
        
		if (Input.GetKeyDown(ScreenShotKey))
		{
            StartCoroutine("TakeScreenShots");
		}
			
	}
    
    //take screenshots
    private int i = 0;
    IEnumerator TakeScreenShots()
    {
        i = 0;

        while(i < ScreenShotSizes.Count)
        {

            if (ScreenShotSizes[i].Enable)
            {
                string TimeTag = System.DateTime.Now.ToString().Replace("/","").Replace(" ","").Replace(":",""); //get DatetimeTag
                string NewFileName = _ScreenShotPath + "/" + ScreenShotSizes[i].Name + "_" + TimeTag + ".png";

                Vector2 V2 = new Vector2(ScreenShotSizes[i].Size.x,ScreenShotSizes[i].Size.y);

                GameView.position = new Rect(0f,0f,(int)V2.x,(int)V2.y);

                Application.CaptureScreenshot(NewFileName); //Save the Image
            }

            i++;
            yield return null;
        }


        GameView.position = DefaultRec;
    }

    #endregion

}


#region ScreenShotSize

[System.Serializable]
public class ScreenShotSize
{
    public bool Enable = true;
	public string Name;
	public Vector2 Size;

}

#endregion

#endif