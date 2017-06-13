# Author: Tom Lambert
# Content: Functions to interact with the user


def read_integer(msg=None, error_msg=None):
    """
    Asks the user for an integer value (int or long)
    :param msg: The message, displayed to the user.
    :param error_msg: The message, displayed to the user, in case he did not entered a valid int or long.
    :return: An int or a long from the user.
    """
    res = raw_input(msg)
    if isinstance(res, int):
        return int(res)
    if isinstance(res, long):
        return long(res)

    if error_msg is not None:
        print(error_msg)
    return read_integer(msg)
