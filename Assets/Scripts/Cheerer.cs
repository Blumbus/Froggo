using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheerer : MonoBehaviour
{

    public Image rend;


    public Sprite[] frames;
    float framesPerSecond = 20;

    void Update()
    {
        int index = (int) (Time.time * framesPerSecond) % frames.Length;
        rend.sprite = frames[index];
    }
}
