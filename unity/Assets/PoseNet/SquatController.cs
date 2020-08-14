using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;

public enum GameMode
{
    Ready,
    Playing,
    Finished,
}

public class SquatController : MonoBehaviour
{
	public PoseNetSample poseNetSample;
    public Text counterText;
    bool counterable = false;
    int count = 0;
    public List<ProgressBarItem> progressBarItems = new List<ProgressBarItem>();

    float hipPosY;
    float kneePosY;

    GameMode gameMode = GameMode.Ready;
    public Text startButtonText;

    void Update()
    {
        if (gameMode != GameMode.Playing)
            return;

        var results = poseNetSample.results;

        hipPosY = -1f;
        kneePosY = -1f;

        foreach (PoseNet.Result result in results)
        {
            if (result.confidence > 0.5f)
            {
                if (result.part == PoseNet.Part.LEFT_HIP)
                {
                    hipPosY = result.y;
                }
                else if (result.part == PoseNet.Part.RIGHT_HIP)
                {
                    hipPosY = result.y;
                }
                else if (result.part == PoseNet.Part.LEFT_KNEE)
                {
                    kneePosY = result.y;
                }
                else if (result.part == PoseNet.Part.RIGHT_KNEE)
                {
                    kneePosY = result.y;
                }
            }
        }

        // 엉덩이 혹은 무릎 위치가 확인되지 않으면 통과
        if (hipPosY < 0f || kneePosY < 0f)
            return;

        if (counterable == false)
        {
            // 엉덩이가 무릎보다 낮아졌을 때            
            if (hipPosY > kneePosY)
            {
                counterable = true;
            }
        }
        else
        {
            //Debug.Log(hipPosY - kneePosY);
            if (kneePosY - hipPosY > 0.1f)
            {
                count += 1;
                counterText.text = count.ToString();

                counterable = false;

                UpdateProgressBar();

                if (count == 10)
                {
                    gameMode = GameMode.Finished;
                    startButtonText.text = "Finish!";

                    foreach (ProgressBarItem item in progressBarItems)
                    {
                        item.ChangeColor(new Color(49/255f, 102/255f, 1f, 1f));
                    }
                }
            }
        }
    }

    void UpdateProgressBar()
    {
        foreach(ProgressBarItem item in progressBarItems)
        {
            item.ChangeColor(Color.white);
        }

        for (int i=0; i < count; i++)
        {
            progressBarItems[i].ChangeColor(Color.green);
        }
    }

    public void OnClickStartButton()
    {
        if (gameMode == GameMode.Ready)
        {
            gameMode = GameMode.Playing;
            startButtonText.text = "Stop";
        }
        else if (gameMode == GameMode.Playing)
        {
            gameMode = GameMode.Ready;
            startButtonText.text = "Start";

            Reset();
        }
        else if (gameMode == GameMode.Finished)
        {
            gameMode = GameMode.Ready;
            startButtonText.text = "Start";

            Reset();
        }
    }

    private void Reset()
    {
        count = 0;
        counterText.text = count.ToString();
        UpdateProgressBar();

        foreach (ProgressBarItem item in progressBarItems)
        {
            item.ChangeColor(Color.white);
        }
    }
}
