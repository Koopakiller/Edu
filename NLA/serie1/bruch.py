from prime import Prime


class Bruch:
    def __init__(self, zaehler, nenner):
        self.numerator = zaehler
        self.denominator = nenner
        self.reduce()

    # Reduce and Clone this object

    def reduce(self):
        q = Prime.get_greatest_common_divisor(self.numerator, self.denominator)
        self.numerator = self.numerator / q
        self.denominator = self.denominator / q

    def clone(self):
        return Bruch(self.numerator, self.denominator)

    # Calculation Operators

    def __add__(self, other):
        return Bruch(self.numerator * other.denominator + other.numerator * self.denominator,
                     self.denominator * other.denominator)

    def __sub__(self, other):
        return Bruch(self.numerator * other.denominator - other.numerator * self.denominator,
                     self.denominator * other.denominator)

    def __mul__(self, other):
        return Bruch(self.numerator * other.numerator, self.denominator * other.denominator)

    def __div__(self, other):
        return Bruch(self.numerator * other.denominator, self.denominator * other.numerator)

    def __neg__(self):
        return Bruch(-self.numerator, self.denominator)

    # Equality Operators

    def __eq__(self, other):
        a = self - other
        return a.numerator == 0 and a.denominator != 0

    def __cmp__(self, other):
        return self.__eq__(other)

    def __ne__(self, other):
        return not self.__eq__(other)

    # String

    def __str__(self):
        if self.numerator == 0:
            return "0"
        if self.denominator == 0:
            return "NaN"
        if self.denominator == 1:
            return str(self.numerator)
        return str(self.numerator) + " / " + str(self.denominator)
