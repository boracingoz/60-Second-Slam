using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaınMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       if (PlayerPrefs.HasKey("Level1"))
       {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
       } 
       else
       {
            PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
       }
    }
}
