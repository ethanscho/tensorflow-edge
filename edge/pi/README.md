## Overview
This guide provides step-by-step instructions for how to set up TensorFlow’s Object Detection API on the Raspberry Pi. 

## Installation
### 1. Update the Raspberry Pi
```
sudo apt-get update
sudo apt-get dist-upgrade
```
### 2. Install TensorFlow
```
pip3 install tensorflow
sudo apt-get install libatlas-base-dev
sudo pip3 install pillow lxml jupyter matplotlib cython
sudo apt-get install python-tk
```
### 3. Install OpenCV
```
sudo apt-get install libjpeg-dev libtiff5-dev libjasper-dev libpng12-dev
sudo apt-get install libavcodec-dev libavformat-dev libswscale-dev libv4l-dev
sudo apt-get install libxvidcore-dev libx264-dev
sudo apt-get install qt4-dev-tools libatlas-base-dev
sudo pip3 install opencv-python
```
### 4. Compile and Install Protobuf
```
sudo apt-get install protobuf-compiler
```
Run `protoc --version` once that's done to verify it is installed. You should get a response of `libprotoc 3.6.1` or similar.


## Reference
https://github.com/EdjeElectronics/TensorFlow-Object-Detection-on-the-Raspberry-Pi