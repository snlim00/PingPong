using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown == true || Input.GetMouseButtonDown(0))
        {
            Touch();
        }
    }

    private void Touch()
    {
        int clearedNoteCount = 0;

        for (int i = 0; i < GameInfo.S.ballList.Count; ++i)
        {
            if (GameInfo.S.ballList[i].canClear == true)
            {
                clearedNoteCount += 1;
                GameInfo.S.ballList[i].Clear();
                break;
            }
        }

        if (clearedNoteCount <= 0)
        {
            Miss();
        }
    }

    private void Miss()
    {
        GameInfo.S.Miss();
    }
}
