using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    public Text monthYearText;
    public CalendarDay calendarDayPrefab;
    public Transform calendarDayParent;
    List<CalendarDay> calendarDays = new List<CalendarDay>();
    int week = 0;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateDays();

        DateTime now = System.DateTime.Now;
        int year = now.Year;
        int month = now.Month;
        int days = DateTime.DaysInMonth(year, month);

        monthYearText.text = year.ToString() + "." + now.Month.ToString();

        print(days);

        for (int i = 0; i < days; i++)
        {
            DateTime date = new DateTime(year, month, i + 1);
            DayOfWeek day = date.DayOfWeek; // 0: sun 6: sat

            if ((int)day == 0)
                week += 1;

            int dayIndex = (int)day + week * 7;
            //print(dayIndex);
            print(dayIndex);
            calendarDays[dayIndex].Display(date);
        }
    }

    void InstantiateDays ()
    {
        for (int i=0; i < 40; i++)
        {
            CalendarDay calendarDay = Instantiate(calendarDayPrefab, calendarDayParent) as CalendarDay;
            calendarDays.Add(calendarDay);
        }
    }
}
