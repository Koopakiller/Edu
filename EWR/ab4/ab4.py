#!/usr/bin/env python

# Author: Tom Lambert
# Content: Contains the main program of ab4. It compares harmonic series calculated with different number of addends.

from __future__ import print_function
from decimal import *

from utils import *
from Sum import Sum
from ui import *

__min_k = 1
__max_k = 4


# Main Program
def main():
    """The main program"""
    ks = range(__min_k, __max_k + 1)

    print("This program compares harmonic series calculated with different number of addends.")
    print("The maximum of the sum is a power to 10 (=10^k)")
    print("The predefined set of k's is {0}".format(ks))
    print()

    if __name__ == '__main__':
        if read_yesno("Would you like to extend the predefined set of k's? [y/N] ", default_input=False):
            k = read_integer("Custom k: ")
            ks.append(k)
            ks = list(set(ks))  # distinct the list
        print()

        __prec = 28
        if read_yesno("Would you like to provide a custom precision? The default precision is {0}. [y/N] "
                      .format(__prec), default_input=False):
            __prec = read_integer_interval("Custom precision: ", minimum=1)
            getcontext().prec = __prec
            print("Initialized the precision of decimal to {0} places".format(__prec))
        print()
    ks.sort()

    print("Test started...")
    print()

    print("The sums will be calculated for i=1 to i=10^k")
    print("k = {0}".format(ks))
    print()

    lst = []
    last_sum = None
    diffs = []

    s = Sum(lambda x: Decimal(1) / Decimal(str(x)))
    s.start_value = 1
    for k in ks:
        s.end_value = 10 ** k
        res = s.calculate()
        if last_sum is not None:
            diff = res - last_sum
            print("                 + {0} = ...".format(diff))
            diffs.append(diff)
        last_sum = res
        print(" k = {0:<2} | result = {1}".format(k, res))
        lst.append(res)

    diff = max(lst) - min(lst)
    avg = average(diffs)
    print("Maximum difference between the sums is {0}".format(diff))
    print("The average difference between the k's is {0}".format(avg))
    print()

    print("Test finished")

main()
