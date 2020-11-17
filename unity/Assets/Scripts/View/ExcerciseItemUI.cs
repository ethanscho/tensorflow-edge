using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcerciseItemUI : MonoBehaviour
{
    public ExcerciseId excerciseId;

    public void OnClickExcerciseItem ()
    {
        MenuSceneController.Instance.OnClickExerciseItem(excerciseId);
    }
}
