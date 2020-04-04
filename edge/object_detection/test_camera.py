import os
import sys
import cv2
import argparse
import numpy as np
from time import sleep
import tensorflow as tf
from picamera import PiCamera

camera = PiCamera()
camera.start_preview()
sleep(10)
camera.stop_preview()