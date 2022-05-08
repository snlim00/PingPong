using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private int direction;
    private float speed = 8;

    public bool canClear = false;

    private bool doChangeDir = true;

    private SpriteRenderer[] spr;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color activeColor;


    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    #region 초기화 관련 함수
    //해당 초기화는 생성하는 Line이 호출함.
    public void Init(int dir = 0)
    {
        InitVariable();

        SetDirection(dir);

        GameInfo.S.ballList.Add(this);
    }

    private void InitVariable()
    {
        spr = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            spr[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        audioSource = GetComponent<AudioSource>();
    }
    
    private void SetDirection(int dir)
    {
        while(dir == 0)
        {
            dir = Random.Range(-1, 2);
        }

        this.direction = dir;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * direction, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TAG.WALL) 
        {
            if (doChangeDir == true)
                SetCanClear(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TAG.WALL)
        {
            if(doChangeDir == true)
            {
                doChangeDir = false;

                ToggleDirction();

                audioSource.Play();
            }
            else
            {
                doChangeDir = !doChangeDir;

                if (canClear == true) //노트가 튕겨나간 후에도 처리 판정이 남아있다면 Miss처리
                {
                    Miss();
                }

                SetCanClear(false);
            }
        }
    }

    public void SetCanClear(bool value)
    {
        if(value == true)
        {
            canClear = true;
            SetColor(activeColor);
            transform.localScale = Vector2.one;
            BallAnimation.speedMultiplier = 40;
        }
        else
        {
            canClear = false;
            SetColor(defaultColor);
            transform.localScale = new Vector2(0.9f, 0.9f);
            BallAnimation.speedMultiplier = 9;
        }
    }

    private void SetColor(Color color)
    {
        for(int i = 0; i < spr.Length; ++i)
        {
            spr[i].color = color;
        }
    }

    private void ToggleDirction()
    {
        direction *= -1;
    }


    public void Clear()
    {
        canClear = false;

        GameInfo.S.AddScore();
    }

    public void Miss()
    {
        GameInfo.S.Miss();
    }
}
