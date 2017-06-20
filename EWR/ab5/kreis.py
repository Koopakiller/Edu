# Author: Tom Lambert
# Content: Logic for ab5.py

from decimal import *
import sympy as sp


class Kreis:
    """Contains logic to calculate points at the unit circle with different algorithms."""

    def __init__(self):
        """Initializes the class object."""
        self._function_call_counter = 0
        self._pi = Decimal("3.14159265358979323846")

    @property
    def function_call_counter(self):
        """Returns the number of called sin and cos calculations since the last reset."""
        return self._function_call_counter

    def reset_function_call_counter(self):
        """Resets the counter of sin- and cos calls."""
        self._function_call_counter = 0


    def _cos(self, angle):
        """Calculates the cosine of an angle and increases the counter for sin- and cos calls."""
        self._function_call_counter += 1
        return sp.cos(angle)

    def _sin(self, angle):
        """Calculates the sine of an angle and increases the counter for sin- and cos calls."""
        self._function_call_counter += 1
        return sp.sin(angle)


    def _naiv(self, angles):
        """
        calculates the points at the unit circle with the 'naiv' algorithm for the given angles.
        :param angles: The points will be calculated these angles.
        :rtype: Dictionary { angle (integer) : (x (integer), y (integer)) }
        :return: A dictionary with the angles as keys and the points ( (x,y)-tuples ) as values.
        """
        result = {}
        for angle in angles:
            x = self._cos(Decimal("2") * self._pi * Decimal(str(angle)) / Decimal("360"))
            y = self._sin(Decimal("2") * self._pi * Decimal(str(angle)) / Decimal("360"))
            result.update( { angle: (x, y)})
        return result

    def naiv(self):
        """
        calculates the points at the unit circle with the 'naiv' algorithm for the angles 0...359.
        :rtype: Dictionary { angle (integer) : (x (integer), y (integer)) }
        :return: A dictionary with the angles as keys and the points ( (x,y)-tuples ) as values.
        """
        return self._naiv(range(0, 360))

    def effizient(self):
        """
        calculates the points at the unit circle with the 'effizient' algorithm for the angles 0...359.
        :rtype: Dictionary { angle (integer) : (x (integer), y (integer)) }
        :return: A dictionary with the angles as keys and the points ( (x,y)-tuples ) as values.
        """
        beta = Decimal("2") * self._pi / Decimal("360")
        cos_beta = self._cos(beta)
        sin_beta = self._sin(beta)
        alpha = 0

        result = {}

        last_x = Decimal("1")
        last_y = Decimal("0")

        result.update({0: (last_x, last_y)})

        while alpha < 360:
            alpha += 1
            x = last_x * cos_beta - last_y * sin_beta
            y = last_y * cos_beta + last_x * sin_beta
            result.update({alpha: (x, y)})
            last_x = x
            last_y = y
        return result

    def symmetrie(self):
        """
        calculates the points at the unit circle with the 'symmetrie' algorithm for the angles 0...359.
        :rtype: Dictionary { angle (integer) : (x (integer), y (integer)) }
        :return: A dictionary with the angles as keys and the points ( (x,y)-tuples ) as values.
        """
        pairs = self._naiv(range(0, 46))
        result = {}
        for angle in pairs:
            pair = pairs[angle]
            result.update( { (  0 + angle): ( pair[0],  pair[1])})
            result.update( { (360 - angle): ( pair[0], -pair[1])})
            result.update( { (180 - angle): (-pair[0],  pair[1])})
            result.update( { (180 + angle): (-pair[0], -pair[1])})
            result.update( { ( 90 - angle): ( pair[1],  pair[0])})
            result.update( { (270 + angle): ( pair[1], -pair[0])})
            result.update( { ( 90 + angle): (-pair[1],  pair[0])})
            result.update( { (270 - angle): (-pair[1], -pair[0])})
        return result
