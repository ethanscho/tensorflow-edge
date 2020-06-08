import os
import cv2
import time
import numpy as np
from picamera import PiCamera
import matplotlib.pyplot as plt
from picamera.array import PiRGBArray

from pose_engine import PoseEngine

IM_WIDTH = 640
IM_HEIGHT = 480

# load deeplab-v3-plus model
engine = PoseEngine('models/mobilenet/posenet_mobilenet_v1_075_481_641_quant_decoder_edgetpu.tflite')

# Initialize frame rate calculation
frame_rate_calc = 1
freq = cv2.getTickFrequency()
font = cv2.FONT_HERSHEY_SIMPLEX

camera = PiCamera()
camera.resolution = (IM_WIDTH,IM_HEIGHT)
camera.framerate = 10
rawCapture = PiRGBArray(camera, size=(IM_WIDTH,IM_HEIGHT))
rawCapture.truncate(0)

for frame1 in camera.capture_continuous(rawCapture, format="bgr",use_video_port=True):
    t1 = cv2.getTickCount()
    
    frame = np.copy(frame1.array)
    frame.setflags(write=1)
    frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    frame_rgb = cv2.resize(frame, (IM_WIDTH, IM_HEIGHT))
    poses, inference_time = engine.DetectPosesInImage(np.uint8(frame_rgb))
    print('Inference time: %.fms' % inference_time)

    for pose in poses:
        if pose.score < 0.4: continue
        print('\nPose Score: ', pose.score)
        for label, keypoint in pose.keypoints.items():
            print(' %-20s x=%-4d y=%-4d score=%.1f' %
                (label, keypoint.yx[1], keypoint.yx[0], keypoint.score))
            cv2.circle(frame, (keypoint.yx[1], keypoint.yx[0]), 10, (255, 0, 0), 4) 

    cv2.putText(frame,"FPS: {0:.2f}".format(frame_rate_calc),(30,50),font,1,(255,255,0),2,cv2.LINE_AA)
    cv2.imshow('semantic segmentation', frame)

    t2 = cv2.getTickCount()
    time1 = (t2-t1)/freq
    frame_rate_calc = 1/time1

    # Press 'q' to quit
    if cv2.waitKey(1) == ord('q'):
        break

    rawCapture.truncate(0)

camera.close()