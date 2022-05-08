using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager S = null;

    public int difficulty;

    public int score;

    void Awake()
    {
        if(S != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        S = this;
    }

    public void SetDifficulty()
    {
        difficulty = Convert.ToInt32(EventSystem.current.currentSelectedGameObject.name);
        Debug.Log(difficulty);

        SceneManager.LoadScene(SCENE.GAME_SCENE);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
