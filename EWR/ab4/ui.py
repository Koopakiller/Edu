# Author: Tom Lambert
# Content: Functions to interact with the user (ui = User Interface)


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


def read_yesno(msg="[Y/n]", error_msg="Unrecognized input!", default_input=None):
    """
    Asks the user to input yes or no.
    :param msg: The message, displayed to the user.
    :param error_msg: The message, displayed to the user, in case he did not entered "y", "yes", "n" or "no".
    :param default_input: The value to return, in case the user entered "". Pass None to ask the user again.
    :return: True if he entered "y" or "yes"; otherwise False.
    """
    res = input(msg)
    if res.lower() in ["y", "yes"]:
        return True
    if res.lower() in ["n", "no"]:
        return False
    if default_input is not None and res == "":
        return default_input

    if error_msg is not None:
        print(error_msg)
    return read_yesno(msg, error_msg)
