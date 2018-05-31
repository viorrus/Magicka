using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBar : MonoBehaviour {

    public Image image;

    public void Init(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetValue(float value)
    {
        image.fillAmount = value;
    }

}
