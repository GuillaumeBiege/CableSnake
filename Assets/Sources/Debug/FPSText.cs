using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSText : MonoBehaviour
{
    //References
    Text text = default;

    //Variables 
    public int avgFPS;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFPS = (int)current;

        text.text = "FPS : " + avgFPS.ToString();
    }
}
