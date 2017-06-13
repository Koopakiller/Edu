# Author: Tom Lambert
# Content: Provides functionality to calculate sums using the decimal type.

from decimal import *


class DecimalComparer(object):

    def __init__(self, delegate):
        self.precisions = []
        self._delegate = delegate

    def run(self):
        dct = {}
        for p in self.precisions:
            getcontext().prec = p
            res = self._delegate()
            dct.update({p: res})
        return dct
