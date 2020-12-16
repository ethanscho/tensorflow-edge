using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    public Text monthYearText;
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

        for (int i = 0; i < days; i++)
        {
            DateTime date = new DateTime(year, month, i + 1);
            DayOfWeek day = date.DayOfWeek; // 0: sun 6: sat

            if ((int)day == 0 && i > 0) // sunday
                week += 1;

            int dayIndex = (int)day + week * 7;
            calendarDays[dayIndex].Display(date);

            if (DatabaseController.Instance.IsDateSaved(date))
                calendarDays[dayIndex].ShowCircle();

            //if (UnityEngine.Random.Range(0, 4) == 0)
            //    calendarDays[dayIndex].ShowCircle();
        }
    }

    void InstantiateDays ()
    {
        GameObject calendarDayPrefab = Resources.Load("Prefabs/CalendarDay") as GameObject;
        for (int i=0; i < 40; i++)
        {
            GameObject calendarDay = Instantiate(calendarDayPrefab, calendarDayParent);
            CalendarDay calendarDayScript = calendarDay.GetComponent<CalendarDay>();
            calendarDays.Add(calendarDayScript);

            //calendarDayScript.ShowCircle();
        }
    }
}
