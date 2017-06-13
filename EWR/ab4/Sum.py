# Author: Tom Lambert
# Content: Provides functionality to calculate sums using the decimal type.

from decimal import *


class Sum:
    """Provides functionality to calculate sums."""

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
