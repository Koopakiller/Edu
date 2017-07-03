# Author: Tom Lambert
# Content: Functions to interact with the user (ui = User Interface)

import os.path


def input_file_name(msg, error_msg="The file does not exists. Try again."):
    user_input = raw_input(msg)
    if not os.path.isfile(user_input):
        print(error_msg)
        return input_file_name(msg, error_msg=error_msg)
    return user_input
