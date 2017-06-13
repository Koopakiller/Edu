# Author: Tom Lambert
# Content: Provides functionality to calculate sums using the decimal type.

from decimal import *


class Sum(object):
    """Provides functionality to calculate sums."""

    def __init__(self, delegate, start_value=1, end_value=10):
        """
        Initializes the Sum-class.
        :param delegate: A method which determines one addend.
        """
        self.start_value = start_value
        self.end_value = end_value
        self.__delegate = delegate
        self.cache = {}

    def get_from_cache(self):

        max_key = self.start_value
        try:
            dict_entry = self.cache[self.start_value]
            for key in dict_entry:
                if key <= self.end_value and key > max_key:
                    max_key = key
            res = Decimal('0') if max_key == self.start_value else dict_entry[max_key]
            max_key += 1
        except KeyError:
            res = Decimal('0')

        return max_key, res

    def add_to_cache(self, res):
        if self.start_value not in self.cache:
            self.cache.update({self.start_value: {}})
        if self.end_value not in self.cache[self.start_value]:
            self.cache[self.start_value].update({self.end_value: res})

    def calculate(self):
        """
        Calculates the sum.
        :return: The sum.
        """
        start, res = self.get_from_cache()
        for i in range(start, self.end_value + 1):
            res += self.__delegate(i)
        self.add_to_cache(res)
        return res
