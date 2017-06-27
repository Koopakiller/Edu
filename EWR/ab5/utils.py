# Author: Tom Lambert
# Content: Utils for common use.


def average(lst):
    length = len(lst)
    if length == 0:
        return None
    else:
        return sum(lst) / length
