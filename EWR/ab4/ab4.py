#!/usr/bin/env python

from __future__ import print_function
from DecimalTestRunner import DecimalTestRunner
from SumTestRunner import SumTestRunner
from ui import *


def test_runner_function():
    t = SumTestRunner()
    t.run()

# Main Program
def main():

    dtr = DecimalTestRunner(test_runner_function)

    if __name__ == '__main__':
        print("Would you like to use a predefined set of mantissas?")



    dtr.min_precision = 1
    dtr.max_precision = 10



    # x = Sum(lambda i: Decimal(1)/i)
    # x.start_value = 1
    # x.end_value = 100
    # result = x.calculate()
    # print(result)

main()
