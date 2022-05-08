using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private GameObject linePref;

    [SerializeField] private int difficulty = DIF.E;
    private float[] linePosition = new float[] { 0, 1.7f, -1.7f, 3.4f, -3.4f };

    private float minDuration = 8;
    private float maxDuration = 12;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameManager.S.difficulty;
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LineGeneration(int num = 1)
    {
        LineController line = Instantiate(linePref).GetComponent<LineController>();

        float duration = Random.Range(minDuration, maxDuration);
        line.Init(new Vector2(0, linePosition[num]), 10);
    }

    private IEnumerator Timer()
    {
        int count = 0;

        while(true)
        {
            float t = 0;

            while(t < 1)
            {
                t += Time.deltaTime / (minDuration - difficulty);

                yield return null;
            }

            LineGeneration(count);
            count += 1;
            if(count >= difficulty)
            {
                count = 0;
            }
        }
    }
}
