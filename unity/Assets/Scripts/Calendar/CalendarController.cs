using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    public Text monthYearText;

    // Start is called before the first frame update
    void Start()
    {
        DateTime now = System.DateTime.Now;
        print(now);
        int year = now.Year;
        int month = now.Month;
        int days = DateTime.DaysInMonth(year, month);

        monthYearText.text = year.ToString() + "." + now.Month.ToString();

        for (int i = 0; i < days; i++)
        {
            DateTime date = new DateTime(year, month, i + 1);
            DayOfWeek day = date.DayOfWeek;

        }
    }
}
