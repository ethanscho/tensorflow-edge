﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseController : Singleton<DatabaseController>
{
    IDbConnection dbcon;
    List<DateTime> dateTimes = new List<DateTime>();

    void Start()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/db";
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        print(Application.persistentDataPath);

        CreateTable();
        InsertExcerciseHistory();
        ReadTable();
    }

    void CreateTable ()
    {
        IDbCommand dbcmd;
        IDataReader reader;

        dbcmd = dbcon.CreateCommand();
        string q_createTable =
          "CREATE TABLE IF NOT EXISTS history (exercise_id INTEGER, date TEXT )";

        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();
    }

    void InsertExcerciseHistory ()
    {
        DateTime now = System.DateTime.Now;

        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO history (exercise_id, date) VALUES (0, '" + now.ToString("yyyy-MM-dd") + "')";
        cmnd.ExecuteNonQuery();
    }

    void ReadTable ()
    {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM history";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("exercise_id: " + reader[0].ToString());
            Debug.Log("date: " + reader[1].ToString());

            DateTime dateTime = System.DateTime.Parse(reader[1].ToString());
            print(dateTime.ToString("yyyy-MM-dd"));

            dateTimes.Add(dateTime);
        }

        // Close connection
        dbcon.Close();
    }

    public bool IsDateSaved (DateTime dateTime)
    {
        return dateTimes.Contains(dateTime);
    }
}
