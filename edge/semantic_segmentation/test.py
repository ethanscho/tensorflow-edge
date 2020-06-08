import os
import cv2
import numpy as np
import matplotlib.pyplot as plt
import tflite_runtime.interpreter as tflite

model_path = '../../research/deeplab-v3-plus/weights/model.tflite'
interpreter = tflite.Interpreter(model_path)

interpreter.allocate_tensors()

input_details = interpreter.get_input_details()
output_details = interpreter.get_output_details()

floating_model = input_details[0]['dtype'] == np.float32
print(floating_model)

##
TEST_DIR = 'images/'
RESULT_DIR = 'results/'
INPUT_SIZE = 513
test_imgs = os.listdir(TEST_DIR)

for i, img_path in enumerate(test_imgs):
    img_name = img_path
    img_name = img_name.split('.')[0]
    img_path = os.path.join(TEST_DIR, img_path)

    test_img = cv2.imread(img_path)
    test_img = test_img[:,:,::-1]
    test_img = cv2.resize(test_img, (INPUT_SIZE, INPUT_SIZE))
    test_img = test_img[np.newaxis, ...]

    test_img = (2.0 / 255.0) * test_img - 1.0

    # output = model.predict(test_img)[0]
    # output = np.squeeze(output, axis=-1)
    print(test_img.shape)
    test_img = np.float32(test_img)

    interpreter.set_tensor(input_details[0]['index'], test_img)
    interpreter.invoke()

    output_data = interpreter.get_tensor(output_details[0]['index'])
    output = np.squeeze(output_data)

    output[output > 0.5] = 255

    plt.imshow(test_img[0])
    plt.imshow(output, alpha=0.4)
    plt.show()

    plt.savefig(RESULT_DIR + img_name + '.png')

    if i > 9:
        break