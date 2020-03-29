## Overview
This guide provides step-by-step instructions for how to set up TensorFlow’s Object Detection API on the Raspberry Pi. 

## Installation
### 1. Update the Raspberry Pi
```
sudo apt-get update
sudo apt-get dist-upgrade
```
### 2. Install Python 3.6
```
sudo apt-get update
sudo apt-get install -y build-essential tk-dev libncurses5-dev libncursesw5-dev libreadline6-dev libdb5.3-dev libgdbm-dev libsqlite3-dev libssl-dev libbz2-dev libexpat1-dev liblzma-dev zlib1g-dev libffi-dev libhdf5-dev

wget https://www.python.org/ftp/python/3.6.8/Python-3.6.8.tgz
sudo tar zxf Python-3.6.8.tgz
cd Python-3.6.8
sudo ./configure
sudo make -j 4
sudo make altinstall
```
### 3. Install TensorFlow
```
wget https://www.piwheels.org/simple/tensorflow/tensorflow-1.11.0-cp35-none-linux_armv7l.whl
mv tensorflow-1.11.0-cp35-none-linux_armv7l.whl tensorflow-1.11.0-cp36-none-linux_armv7l.whl
sudo pip3.6 install tensorflow-1.11.0-cp36-none-linux_armv7l.whl
```
### 4. Install OpenCV
```
sudo apt-get install libjpeg-dev libtiff5-dev libjasper-dev libpng12-dev
sudo apt-get install libavcodec-dev libavformat-dev libswscale-dev libv4l-dev
sudo apt-get install libxvidcore-dev libx264-dev
sudo apt-get install qt4-dev-tools libatlas-base-dev
sudo pip3 install opencv-python
```
### 5. Compile and Install Protobuf
```
sudo apt-get install protobuf-compiler
```
Run `protoc --version` once that's done to verify it is installed. You should get a response of `libprotoc 3.6.1` or similar.


## Reference
https://github.com/EdjeElectronics/TensorFlow-Object-Detection-on-the-Raspberry-Pi