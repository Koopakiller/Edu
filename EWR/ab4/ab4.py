#

from __future__ import print_function
from decimal import *
from ui import *

class Sum:
    """Provides mechanisms to calculate sums."""

    def __init__(self, delegate, start_value=1, end_value=10):
        """
        Initializes the Sum-class.
        :param delegate: A method which determines one addend.
        """
        self.start_value = start_value
        self.end_value = end_value
        self.__delegate = delegate

    def calculate(self):
        """
        Calculates the sum.
        :return: The sum.
        """
        res = Decimal('0')
        for i in range(self.start_value, self.end_value + 1):
            res += self.__delegate(i)
        return res


class SumTest:
    """Test class for Sum-class."""

    def __init__(self):
        """Initializes the necessary parameters for the tests."""
        self.min_k = 1
        self.max_k = 10

    def run(self):
        """Runs the test."""
        for k in range(self.min_k, self.max_k):
            s = Sum(lambda x: Decimal(1) / Decimal(str(x)))
            s.start_value = 1
            s.end_value = 10**k
            res = s.calculate()
            print("result = {0:>20} | k = {1:>3} | 10^k = {2:>20}".format(res, k, 10**k))


# Main Program

def main():
    if __name__ == '__main__':

        getcontext().prec = 28

        t = SumTest()
        t.run()

    # x = Sum(lambda i: Decimal(1)/i)
    # x.start_value = 1
    # x.end_value = 100
    # result = x.calculate()
    # print(result)

main()
