import os
import cv2
import time
import model
import numpy as np
import matplotlib.pyplot as plt
import tflite_runtime.interpreter as tflite

from picamera import PiCamera
from picamera.array import PiRGBArray

IM_WIDTH = 640
IM_HEIGHT = 480
INPUT_SIZE = 513

# load deeplab-v3-plus tflite model
model_path = '../../research/deeplab-v3-plus/weights/model.tflite'
#interpreter = tflite.Interpreter(model_path)
interpreter = tflite.Interpreter(model_path,
  experimental_delegates=[tflite.load_delegate('libedgetpu.so.1')])
interpreter.allocate_tensors()

input_details = interpreter.get_input_details()
output_details = interpreter.get_output_details()

floating_model = input_details[0]['dtype'] == np.float32
print(floating_model)

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

    frame_rgb = cv2.resize(frame, (INPUT_SIZE, INPUT_SIZE))
    frame_rgb = frame_rgb[np.newaxis, ...] # expand dim for batch
    frame_rgb = (2.0 / 255.0) * frame_rgb - 1.0 # normalize image

    frame_rgb = np.float32(frame_rgb)

    # infer 
    start_timestamp = time.time()
    interpreter.set_tensor(input_details[0]['index'], frame_rgb)
    interpreter.invoke()

    output_data = interpreter.get_tensor(output_details[0]['index'])
    output = np.squeeze(output_data)
    print(time.time() - start_timestamp)

    filters = output[output > 0]
    print(filters)
    #output = (output - 0.99) / 0.01

    # draw mask
    mask_image = np.zeros((INPUT_SIZE, INPUT_SIZE, 1))
    mask_image[:,:,0] = output
    mask_image = cv2.resize(mask_image, (IM_WIDTH, IM_HEIGHT))
    
    for c in range(3):
        frame[:,:,c] = frame[:,:,c] * mask_image

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