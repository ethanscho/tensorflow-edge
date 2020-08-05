using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarItem : MonoBehaviour
{
    public Image image;

    public void ChangeColor(Color color)
    {
        image.color = color;
    }
}
