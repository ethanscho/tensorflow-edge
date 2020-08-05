using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TensorFlowLite;

public class SquatController : MonoBehaviour
{
	public PoseNetSample poseNetSample;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var results = poseNetSample.results;

        foreach(PoseNet.Result result in results)
        {
            if (result.confidence > 0.5f)
            {
                Debug.Log(result.part);
                Debug.Log(result.x);
                Debug.Log(result.y);
            }
        }
	}
}
