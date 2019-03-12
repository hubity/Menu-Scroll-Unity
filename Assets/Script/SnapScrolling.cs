using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;

    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    public int selectedPanID;
    private bool isScrolling;

    public int j;

    public Image image;
    public Sprite[] sprite;

    public string[] nomeGame;

    private void Start()
    {
        
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {
            image.sprite = sprite[i];
            instPans[i] = Instantiate(panPrefab, transform, false);
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                instPans[i].transform.localPosition.y);
            pansPos[i] = -instPans[i].transform.localPosition;
        }
      
    }

    public int local;
    
    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            local = local + 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            local = local - 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(nomeGame[local]);
            Show();
        }

        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = local;
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
            
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    public void Show()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com." + nomeGame[local]);
    }

}