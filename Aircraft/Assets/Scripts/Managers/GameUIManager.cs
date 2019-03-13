using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

    void Start ()
    {
	
	}
	

	void Update ()
    {
	
	}

    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }



}
