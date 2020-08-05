using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;

public class SquatController : MonoBehaviour
{
	public PoseNetSample poseNetSample;
    public Text counterText;
    bool counterable = true;
    int count = 0;
    public List<ProgressBarItem> progressBarItems = new List<ProgressBarItem>();

    float hipPosY;
    float kneePosY;

    void Update()
    {
        var results = poseNetSample.results;

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

        //Debug.Log("knee: " + kneePosY);
        //Debug.Log("hip: " + hipPosY);

        if (counterable)
        {
            if (hipPosY > kneePosY)
            {
                count += 1;
                counterText.text = count.ToString();

                counterable = false;

                UpdateProgressBar();
            }
        }
        else
        {
            //Debug.Log(hipPosY - kneePosY);
            if (kneePosY - hipPosY > 0.1f)
            {
                counterable = true;
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
}
