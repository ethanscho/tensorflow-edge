using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ExcerciseId
{
    Squat,
    JumpingSquat,
}

public class MenuSceneController : Singleton<MenuSceneController>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickExerciseItem (ExcerciseId excerciseId)
    {
        SceneManager.LoadScene(excerciseId.ToString());
    }
}
