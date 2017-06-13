#!/usr/bin/env python

from __future__ import print_function
from decimal import *
from Sum import Sum
from ui import *

__min_k = 1
__max_k = 4


# Main Program
def main():
    """The main program"""
    ks = range(__min_k, __max_k + 1)

    if __name__ == '__main__':
        if read_yesno("Would you like to extend the predefined set of k's? (starting with {0}, ending with {1}) "
                      "[Y/n] "
                      .format(__min_k, __max_k)):
            prec = read_integer("Custom k: ")
            ks.append(prec)
            ks = list(set(ks))  # distinct the list
    ks.sort()

    print()
    print("Test started...")

    getcontext().prec = 28
    print("Initialized the precision of decimal to 28 places")

    print()
    print("The sums will be calculated for i=1 to i=10^k")
    print("k = {0}".format(ks))

    lst = []
    last_sum = None
    diffs = []

    for k in ks:
        s = Sum(lambda x: Decimal(1) / Decimal(str(x)))
        s.start_value = 1
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
    avg = sum(diffs) / len(diffs)
    print("Maximum difference between the sums is {0}".format(diff))
    print("The average difference between the k's is {0}".format(avg))
    print("Test finished")

main()
