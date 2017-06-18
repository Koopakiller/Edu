#!/usr/bin/env python

# Author: Tom Lambert
# Content: Contains the main program of ab4. It compares harmonic series calculated with different number of addends.
# Task from Uebungsblatt 4 EWR 

from __future__ import print_function
from decimal import *

from utils import *
from Sum import Sum
from DecimalComparer import DecimalComparer
from ui import *

__min_k = 1
__max_k = 4
__min_prec = 1
__max_prec = 4


def test_k(ks):
    """
    Runs the test to compare series which are aborted after different count of addends.
    :param ks: The k's to use for the maximum 10^k.
    :return: Nothing.
    """

    ks.sort()

    print("Test about different k's started...")
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
    print("The average difference between the results is {0}".format(avg))
    print()

    print("Test about different k's finished")


def test_prec(precisions, k):
    """
    Runs the test to compare decimals/series with different precision.
    :param precisions: The precisions to use for the maximum 10^k.
    :param k: 10^k is the end of the calculation of the series.
    :return: Nothing.
    """
    print("Test about different precisions started...")
    print()

    precisions.sort()

    print("The precisions are {0}".format(precisions))

    def test_fx():
        s = Sum(lambda x: Decimal(1) / Decimal(str(x)))
        s.start_value = 1
        s.end_value = 10 ** k
        return s.calculate()

    dc = DecimalComparer(test_fx)
    dc.precisions = precisions
    dct = dc.run()

    print("The following values were calculated:")
    lst = []
    for key in dct:
        print(" precision = {0:>3} | result = {1}".format(key, dct[key]))
        lst.append(dct[key])

    print("The average of these results is {0}.".format(average(lst)))
    print("The maximum difference of these results is {0}.".format(max(lst) - min(lst)))
    print()

    print("Test finished")


# Main Program
def main():
    """The main program"""
    ks = range(__min_k, __max_k + 1)
    precisions = range(__min_prec, __max_prec + 1)
    prec_test_k = 5

    print("This program tests the calculation of the harmonic sum with a certain number of addends. "
          "The Test is split in 2 parts:")

    print("The first one compares different amounts of addends (=10^k).")
    print("The predefined set of k's is {0}".format(ks))
    print()

    if __name__ == '__main__':
        if read_yesno("Would you like to extend the predefined set of k's? [y/N] ", default_input=False):
            k = read_integer("Custom k: ")
            ks.append(k)
            ks = list(set(ks))  # distinct the list
        print()

        __prec = 28
        if read_yesno("Would you like to provide a custom precision for the decimal type to use in the k-test? "
                      "The default precision is {0}. [y/N] "
                      .format(__prec), default_input=False):
            __prec = read_integer_interval("Custom precision: ", minimum=1)
            getcontext().prec = __prec
            print("Initialized the precision of decimal to {0} places".format(__prec))
        print()

    print("In the second test, different precisions of decimal type will be used to calculate the sums.")
    print("The predefined set of precisions is {0}".format(precisions))
    print()

    if __name__ == '__main__':
        if read_yesno("Would you like to extend the predefined set of precisions? [y/N] ", default_input=False):
            p = read_integer("Custom precision: ")
            precisions.append(p)
            precisions = list(set(precisions))  # distinct the list
        print()

        if read_yesno("Would you like to provide a custom amount of addends for the calculation of the sum?\n"
                      "(Amount of addends = 10^k, the default k is {0}) [y/N] "
                      .format(prec_test_k), default_input=False):
            prec_test_k = read_integer_interval("Custom k: ", minimum=1)
        print()

    test_k(ks)
    print()

    test_prec(precisions, prec_test_k)

main()
