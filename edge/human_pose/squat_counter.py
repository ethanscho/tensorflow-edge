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

# initialize frame rate calculation
frame_rate_calc = 1
freq = cv2.getTickFrequency()
font = cv2.FONT_HERSHEY_SIMPLEX

camera = PiCamera()
camera.resolution = (IM_WIDTH,IM_HEIGHT)
camera.framerate = 10
rawCapture = PiRGBArray(camera, size=(IM_WIDTH,IM_HEIGHT))
rawCapture.truncate(0)

# squat counter
counter = 0
counterable = True
#hip_pos_y = 0.0

for frame1 in camera.capture_continuous(rawCapture, format="bgr",use_video_port=True):
    t1 = cv2.getTickCount()
    
    frame = np.copy(frame1.array)
    frame.setflags(write=1)
    frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    frame_rgb = cv2.resize(frame, (IM_WIDTH, IM_HEIGHT))
    poses, inference_time = engine.DetectPosesInImage(np.uint8(frame_rgb))
    #print('inference time: %.fms' % inference_time)

    for pose in poses:
        if pose.score < 0.4: 
            continue
        
        print('\npose score: ', pose.score)
        for label, keypoint in pose.keypoints.items():
            
            #if label == 'left shoulder' or label == 'right shoulder':
            #print(' %-20s x=%-4d y=%-4d score=%.1f' % (label, keypoint.yx[1], keypoint.yx[0], keypoint.score))
            cv2.circle(frame, (keypoint.yx[1], keypoint.yx[0]), 4, (255, 0, 0), 4) 

        left_hip_y = pose.keypoints['left hip'].yx[0]
        right_hip_y = pose.keypoints['right hip'].yx[0]
        hip_pos_y = (left_hip_y + right_hip_y) * 0.5

        left_knee_y = pose.keypoints['left knee'].yx[0]
        right_knee_y = pose.keypoints['right knee'].yx[0]
        knee_pos_y = (left_knee_y + right_knee_y) * 0.5

        if hip_pos_y < knee_pos_y and counterable:
            counter += 1
            counterable = False
        elif hip_pos_y > knee_pos_y and counterable == False:
            counterable = True

    cv2.putText(frame,"Count: {:03d}".format(counter),(20,60),font,1,(0,0,255),1,cv2.LINE_AA)
    cv2.putText(frame,"FPS: {0:.2f}".format(frame_rate_calc),(20,20),font,0.5,(255,255,0),1,cv2.LINE_AA)
    cv2.imshow('semantic segmentation', frame)

    t2 = cv2.getTickCount()
    time1 = (t2-t1)/freq
    frame_rate_calc = 1/time1

    # Press 'q' to quit
    if cv2.waitKey(1) == ord('q'):
        break

    rawCapture.truncate(0)

camera.close()