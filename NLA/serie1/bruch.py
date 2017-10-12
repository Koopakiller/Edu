from prime import Prime


class Bruch:
    def __init__(self, zaehler, nenner):
        self._numerator = zaehler
        self._denominator = nenner

    def reduce(self):
        q = Prime.get_greatest_common_divisor(self._numerator, self._denominator)
        self._numerator = self._numerator / q
        self._denominator = self._denominator / q

    def __add__(self, a, b):
        

