using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controller")]
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Header("Other Objects")]
    public GameObject panPrefab;

    private GameObject[] insPans; 
    private Vector2[] pansPos;
     
    private RectTransform contentRect;

    public int selectedPanID;
    public bool isScrolling;
    // Start is called before the first frame update
    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        insPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        for(int i = 0; i < panCount; i++)
        {
            insPans[i] = Instantiate(panPrefab, transform, false);
            if (i == 0) continue;
            {
                insPans[i].transform.localPosition = new Vector2(insPans[i-1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                    insPans[i].transform.localPosition.y);
                pansPos[i] = insPans[i].transform.localPosition;
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x + pansPos[i].x);
            if(distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
        }
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
    }
}
