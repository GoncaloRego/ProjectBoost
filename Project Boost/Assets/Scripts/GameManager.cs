using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManager")]
public class GameManager : ScriptableObject
{
    public void ReloadLevel()
    {
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneID);
    }
    public void LoadNextLevel()
    {
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;
        int nextSceneID = currentSceneID + 1;

        if (nextSceneID == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneID = 0;
        }
        SceneManager.LoadScene(nextSceneID);
    }
}
