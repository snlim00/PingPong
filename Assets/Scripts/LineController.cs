using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private GameObject ballPref;
    private GameObject ball;

    private GameObject line;
    private GameObject[] wallArr = new GameObject[2];
    private GameObject progress;

    private float lineLength;
    private float minLineLength = 9;
    private float maxLineLength = 17;

    private void Start()
    {
        
    }

    #region 초기화 관련 함수
    //Init함수는 LineGenerator에 의해 호출됨
    public void Init(Vector2 pos, float duration)
    {
        transform.position = pos;

        GetObjects();

        SetLineLength(minLineLength, maxLineLength);

        SetWallsPosition(lineLength);
        

        BallGeneration();

        StartCoroutine(DurationTimer(duration));
    }

    private void GetObjects()
    {
        line = transform.GetChild(0).gameObject;

        wallArr[0] = transform.GetChild(1).gameObject;
        wallArr[1] = transform.GetChild(2).gameObject;
        progress = transform.GetChild(3).gameObject;
    }

    private void SetLineLength(float min, float max)
    {
        lineLength = Random.Range(min, max);

        line.transform.localScale = new Vector2(lineLength, line.transform.localScale.y);
        progress.transform.localScale = new Vector2(lineLength, progress.transform.localScale.y);
    }

    private void SetWallsPosition(float lineLength)
    {
        wallArr[0].transform.localPosition = new Vector2(-lineLength * 0.5f, 0);
        wallArr[1].transform.localPosition = new Vector2(lineLength * 0.5f, 0);
    }

    private void BallGeneration()
    {
        ball = Instantiate(ballPref) as GameObject;

        BallController ballCtrl = ball.GetComponent<BallController>();

        ballCtrl.transform.position = transform.position;

        ballCtrl.Init();
    }
    #endregion

    private IEnumerator DurationTimer(float duration)
    {
        float t = 0;

        Vector2 startScale = progress.transform.localScale;

        while(t < 1)
        {
            t += Time.deltaTime / duration;

            progress.transform.localScale = Vector2.Lerp(startScale, new Vector2(0, startScale.y), t);

            yield return null;
        }

        Delete();
    }

    private void Delete()
    {
        GameInfo.S.ballList.Remove(ball.GetComponent<BallController>());

        Destroy(ball);

        Destroy(this.gameObject);
    }
}
