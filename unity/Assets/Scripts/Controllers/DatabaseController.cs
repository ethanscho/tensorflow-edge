using System;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseController : MonoBehaviour
{
    IDbConnection dbcon;

    void Start()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/db";
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        print(Application.persistentDataPath);

        //CreateTable();
        InsertExcerciseHistory();

        ReadTable();
    }

    void CreateTable ()
    {
        IDbCommand dbcmd;
        IDataReader reader;

        dbcmd = dbcon.CreateCommand();
        string q_createTable =
          "CREATE TABLE IF NOT EXISTS history (excercise_id INTEGER, date TEXT )";

        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();
    }

    void InsertExcerciseHistory ()
    {
        DateTime now = System.DateTime.Now;

        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO history (excercise_id, date) VALUES (0, '" + now.ToString("dd/MM/yyyy") + "')";
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
            Debug.Log("excercise_id: " + reader[0].ToString());
            Debug.Log("date: " + reader[1].ToString());
        }

        // Close connection
        dbcon.Close();
    }
}
