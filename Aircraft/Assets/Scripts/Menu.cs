using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    public void Play()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void Creators()
    {
        SceneManager.LoadScene("Creators");
    }
    public void RateGame()
    {
        Application.OpenURL("http://unity3d.com/");
    }
}
