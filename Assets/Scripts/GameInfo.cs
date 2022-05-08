using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public static GameInfo S;

    public List<BallController> ballList = new List<BallController>();

    public Camera mainCam;

    [SerializeField] private SpriteRenderer bg;

    private int maxHp = 5;
    public int curHp { get; private set; } = 5;
    [SerializeField] private Color[] hpColor;
    private AudioSource audioSource;

    public int score = 0;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InitVariable();
    }

    private void InitVariable()
    {
        S = this;

        mainCam = Camera.main;

        SetBGColor();

        audioSource = GetComponent<AudioSource>();

        GameManager.S.score = 0;
    }

    public void Miss()
    {
        curHp -= 1;

        audioSource.Play();

        if(curHp <= 0)
        {
            GameOver();
            return;
        }

        SetBGColor();
    }

    private void SetBGColor()
    {
        bg.color = hpColor[curHp - 1];
    }

    private void GameOver()
    {
        GameManager.S.score = this.score;

        GameManager.S.ChangeScene(SCENE.RESULT_SCENE);
    }

    public void AddScore(int add = 1)
    {
        score += add;

        scoreText.text = score.ToString();
    }
}
