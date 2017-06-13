#!/usr/bin/env python

from __future__ import print_function
from decimal import *
from DecimalTestRunner import DecimalTestRunner
from Sum import Sum
from ui import *


__min_prec = 3
__max_prec = 10
__min_k = 1
__max_k = 5


def test_runner_function(k):
    s = Sum(lambda x: Decimal(1) / Decimal(str(x)))
    s.start_value = 1
    s.end_value = 10**k
    return s.calculate()


# Main Program
def main():
    dtr = DecimalTestRunner()
    dtr.min_precision = __min_prec
    dtr.max_precision = __max_prec

    if __name__ == '__main__':
        if read_yesno("Would you like to extend the predefined set of mantissas? (starting with {0}, ending with {1}) "
                      "[Y/n] "
                      .format(__min_prec, __max_prec)):
            prec = read_integer("Custom Precision: ")
            dtr.custom_precisions.extend([prec])

    print()
    print("Test started")

    for k in range(__min_k, __max_k + 1):
        print("Test started for k={0}".format(k))
        dtr.delegate = lambda: test_runner_function(k)
        diff = dtr.run()
        print("Maximum difference with the given mantissas is {0}".format(diff))
        print()

    print("Test finished")

main()
