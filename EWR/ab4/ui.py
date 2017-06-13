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

    try:
        return int(res)
    except TypeError:
        pass

    try:
        return long(res)
    except TypeError:
        pass

    if error_msg is not None:
        print(error_msg)
    return read_integer(msg=msg, error_msg=error_msg)


def read_integer_interval(msg=None, error_msg=None, minimum=None, maximum=None):
    """
    Asks the user for an integer value (int or long)
    :param msg: The message, displayed to the user.
    :param error_msg: The message, displayed to the user, in case he did not entered a valid int or long.
    :param minimum: The minimum for the returned value. This border is ignored if this parameter is None.
    :param maximum: The maximum for the returned value. This border is ignored if this parameter is None.
    :return: An int or a long from the user.
    """
    res = read_integer(msg=msg, error_msg=error_msg)
    if minimum is not None and res < minimum:
        print("The value cannot be less then {0}!".format(minimum))
        return read_integer_interval(msg=msg, error_msg=error_msg, minimum=minimum, maximum=maximum)
    if maximum is not None and res > maximum:
        print("The value cannot be greater then {0}!".format(maximum))
        return read_integer_interval(msg=msg, error_msg=error_msg, minimum=minimum, maximum=maximum)
    return res


def read_yesno(msg="[Y/n]", error_msg="Unrecognized input!", default_input=None):
    """
    Asks the user to input yes or no.
    :param msg: The message, displayed to the user.
    :param error_msg: The message, displayed to the user, in case he did not entered "y", "yes", "n" or "no".
    :param default_input: The value to return, in case the user entered "". Pass None to ask the user again.
    :return: True if he entered "y" or "yes"; otherwise False.
    """
    res = raw_input(msg)
    if res.lower() in ["y", "yes"]:
        return True
    if res.lower() in ["n", "no"]:
        return False
    if default_input is not None and res == "":
        return default_input

    if error_msg is not None:
        print(error_msg)
    return read_yesno(msg, error_msg)
