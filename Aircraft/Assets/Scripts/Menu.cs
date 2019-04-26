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
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.SimpleGames.Aircraft");
    }
}
