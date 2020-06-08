tflite_convert \
    --output_file=model.tflite \
    --graph_def_file='datasets/people_segmentation/exp/deeplab-v3-plus/export/saved_model.pb' \
    --input_arrays=Input \
    --output_arrays=Output

edgetpu_compiler '../../research/deeplab-v3-plus/weights/model.tflite' '../../research/deeplab-v3-plus/weights/tpu_model.tflite'