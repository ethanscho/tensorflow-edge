using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarDay : MonoBehaviour
{
    public Text day;
    DateTime date;

    public void Display (DateTime date)
    {
        this.date = date;

        day.text = date.Day.ToString();
    }
}
