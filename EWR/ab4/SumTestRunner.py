# Author: Tom Lambert
# Content: Provides functions to compare decimals with different precisions

from Sum import Sum
from decimal import *


class SumTestRunner:
    """Test class for Sum-class."""

    def __init__(self):
        """Initializes the necessary parameters for the tests."""
        self.min_k = 1
        self.max_k = 10

    def run(self):
        """Runs the test."""
        if self.min_k > self.max_k:
            ValueError("min_k cannot be greater then max_k")

        for k in range(self.min_k, self.max_k):
            s = Sum(lambda x: Decimal(1) / Decimal(str(x)))
            s.start_value = 1
            s.end_value = 10**k
            res = s.calculate()
            print("result = {0:>20} | k = {1:>3} | 10^k = {2:>20}".format(res, k, 10**k))
