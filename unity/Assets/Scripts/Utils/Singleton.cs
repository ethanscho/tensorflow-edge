using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance {
        get {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)FindObjectOfType(typeof(T));
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }
}