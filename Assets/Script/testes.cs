using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testes : MonoBehaviour
{
    public Sprite[] image;
    public Image x;
    // Start is called before the first frame update
    void Start()
    {
        x.sprite = image[1]; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
