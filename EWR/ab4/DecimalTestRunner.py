# Author: Tom Lambert
# Content: Provides functions to compare decimals with different precisions

from decimal import *


class DecimalTestRunner(object):
    """
        Provides functions to compare decimals with different exponents and precisions
    """

    def __init__(self, delegate=None):
        """
        :param delegate: A delegate which returns a decimal for comparison.
        """
        self._min_precision = 1
        self._max_precision = 10
        self._custom_precisions = []
        self._precisions = []
        self.delegate = delegate

# min_precision
    def _get_min_precision(self):
        return self._min_precision

    def _set_min_precision(self, value):
        if value <= 0:
            raise ValueError("precision cannot less or equal 0")
        self._min_precision = value

    min_precision = property(_get_min_precision, _set_min_precision)

# max_precision
    def _get_max_precision(self):
        return self._max_precision

    def _set_max_precision(self, value):
        if value <= 0:
            raise ValueError("precision cannot less or equal 0")
        self._max_precision = value

    max_precision = property(_get_min_precision, _set_min_precision)

# custom_exponents
    def _get_custom_precisions(self):
        return self._custom_precisions

    def _set_custom_precisions(self, value):
        self._custom_precisions = value

    custom_precisions = property(_get_custom_precisions, _set_custom_precisions)

# update lists

    def _update_precisions(self):
        """Updates the _precisions list."""
        self._precisions = range(self._min_precision, self._max_precision + 1)
        self._precisions.extend(self._custom_precisions)
        self._precisions = list(set(self._precisions))  # distinct the list

# class logic
    def run(self):
        if self.delegate is None:
            ValueError("delegate cannot be None")

        self._update_precisions()
        lst = []
        for p in self._precisions:
            getcontext().prec = p
            res = self.delegate()
            lst.extend([res])
            print("  precision = {0:<4} | result = {1}".format(p, res))
        return max(lst) - min(lst)
