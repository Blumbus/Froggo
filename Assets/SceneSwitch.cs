using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
	[SerializeField] private string nextScene;

    public void StartGame()
    {
        SceneManager.LoadScene(nextScene);
    }
}