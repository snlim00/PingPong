using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = GameManager.S.score.ToString();
    }

    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            GameManager.S.ChangeScene(SCENE.MAIN_MENU);
    }
}
