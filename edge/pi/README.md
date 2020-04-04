## Overview
This guide provides step-by-step instructions for how to set up TensorFlow’s Object Detection API on the Raspberry Pi. 

## Installation
### 1. Update the Raspberry Pi
```
sudo apt-get update
sudo apt-get dist-upgrade
```
### 2. Install TensorFlow 1.14
```
wget https://www.piwheels.org/simple/tensorflow/tensorflow-1.14.0-cp37-none-linux_armv7l.whl
sudo pip3 install tensorflow-1.14.0-cp37-none-linux_armv7l.whl
```
### 3. Install OpenCV
```
sudo apt install cmake build-essential pkg-config git

sudo apt install libjpeg-dev libtiff-dev libjasper-dev libpng-dev libwebp-dev libopenexr-dev
sudo apt install libavcodec-dev libavformat-dev libswscale-dev libv4l-dev libxvidcore-dev libx264-dev libdc1394-22-dev libgstreamer-plugins-base1.0-dev libgstreamer1.0-dev

sudo apt install libgtk-3-dev libqtgui4 libqtwebkit4 libqt4-test python3-pyqt5
sudo apt install libatlas-base-dev liblapacke-dev gfortran
sudo apt install libhdf5-dev libhdf5-103

sudo apt install python3-dev python3-pip python3-numpy

git clone https://github.com/opencv/opencv.git
git clone https://github.com/opencv/opencv_contrib.git

mkdir ~/opencv/build
cd ~/opencv/build

cmake -D CMAKE_BUILD_TYPE=RELEASE \
    -D CMAKE_INSTALL_PREFIX=/usr/local \
    -D OPENCV_EXTRA_MODULES_PATH=~/opencv_contrib/modules \
    -D ENABLE_NEON=ON \
    -D ENABLE_VFPV3=ON \
    -D BUILD_TESTS=OFF \
    -D INSTALL_PYTHON_EXAMPLES=OFF \
    -D OPENCV_ENABLE_NONFREE=ON \
    -D CMAKE_SHARED_LINKER_FLAGS=-latomic \
    -D BUILD_EXAMPLES=OFF ..

make -j$(nproc)

sudo make install
```
### 4. Compile and Install Protobuf
```
sudo apt-get install protobuf-compiler
```
Run `protoc --version` once that's done to verify it is installed. You should get a response of `libprotoc 3.6.1` or similar.


## Reference
https://github.com/EdjeElectronics/TensorFlow-Object-Detection-on-the-Raspberry-Pi