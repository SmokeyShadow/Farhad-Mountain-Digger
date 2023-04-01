using UnityEngine;
using System.Collections;

public class LoadSceneOnClick : MonoBehaviour {
    #region PUBLIC METHODS
    public void playGame()
    {
		Application.LoadLevel ("FarhadKohkan");
	}
	public void exitGame (){
		Application.Quit ();
	}
    #endregion
}