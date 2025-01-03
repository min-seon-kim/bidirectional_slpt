import os.path
import cv2
    

def resize():
    import os.path
    import cv2

    original_path = "../Data/Images"
    destination_path = "../Data/ConvertedImages"
    data_partitions = os.listdir(original_path)

    if not os.path.exists(destination_path):
        os.mkdir(destination_path)

    for data in data_partitions:
        filepath = original_path+'/'+data
        # curr_sequences = os.listdir(filepath)
        destination_file_path = destination_path + '/' + data
        # destination_file_path = destination_path
        # if not os.path.exists(destination_file_path):
        #     os.mkdir(destination_file_path)
                                                        
        # for sequence in curr_sequences:
        #     curr_sequence_path = filepath + '/' + sequence
            # curr_frames = os.listdir(curr_sequence_path)
                                    
            # destination_sequence_path = destination_file_path + '/' + sequence
        #     if not os.path.exists(destination_sequence_path):
        #         print("sequence_path", curr_sequence_path)   
        #         os.mkdir(destination_sequence_path)                                                                                                                
        #         for frame in curr_frames:
        #             curr_frames_path = curr_sequence_path + '/' + frame
        #             destination_frame_path = destination_sequence_path + '/' + frame
        try:
            image = cv2.imread(filepath)
            resized_image = cv2.resize(image, (227, 227))
            cv2.imwrite(destination_file_path, resized_image)
        except Exception as e:
            print(str(e))
            print(filepath)
