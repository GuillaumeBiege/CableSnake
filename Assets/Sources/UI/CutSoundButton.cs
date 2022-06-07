using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSoundButton : MonoBehaviour
{
    //References
    [SerializeField] Button button = default;
    [SerializeField] Image iconProhibied = default;
    [SerializeField] Text textSound = default;

    //Variables
    [SerializeField] string textSoundON = "Sound ON";
    [SerializeField] string textSoundOFF = "Sound OFF";


    private void Start()
    {
        SetVisual();

        button = GetComponent<Button>();

        button.onClick.AddListener(Click);
    }

    void SetVisual()
    {
        if (SoundManager.Instance.isSoundON)
        {
            iconProhibied.gameObject.SetActive(false);

            textSound.text = textSoundON;
        }
        else
        {
            iconProhibied.gameObject.SetActive(true);

            textSound.text = textSoundOFF;
        }
    }

    public void Click()
    {
        if (SoundManager.Instance.isSoundON)
        {
            SoundManager.Instance.PutSoundOFF();
        }
        else
        {
            SoundManager.Instance.PutSoundON();
        }

        SetVisual();
    }
}
