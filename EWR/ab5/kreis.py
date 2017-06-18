# Author: Tom Lambert
# Content: Logic for ab5.py

from numpy as np


class Kreis:

    def __init__(self):
        self._function_call_counter = 0


    @property
    def function_call_counter(self):
        return self._function_call_counter

    def reset_function_call_counter(self):
        self._function_call_counter = 0


    def _cos(self, angle):
        self._function_call_counter += 1
        return np.cos(angle)

    def _sin(self, angle):
        self._function_call_counter += 1
        return np.sin(angle)


    def _naiv(self, angles):
        result = {}
        for angle in angles:
            x = self._cos(2 * np.Pi * angle / 360)
            y = self._sin(2 * np.Pi * angle / 360)
            result.update( { angle: (x, y) } )
        return result

    def naiv(self):
        return self._naiv(range(0, 360))

    def effizient(self):

        result = []
        for angle in range(0, 360):
            x = self._cos(2 * np.Pi * angle / 360)
            y = self._sin(2 * np.Pi * angle / 360)
            result.append((x, y))
        return result

    def symmetrie(self):
        pairs = self._naiv(range(1, 46))
        result = {}
        for angle in pairs:
            pair = pairs[angle]
            result.append( { (  0 + angle) : ( pair[0],  pair[1]) } )
            result.append( { (360 - angle) : ( pair[0], -pair[1]) } )
            result.append( { (180 - angle) : (-pair[0],  pair[1]) } )
            result.append( { (180 + angle) : (-pair[0], -pair[1]) } )
            result.append( { ( 90 - angle) : ( pair[1],  pair[0]) } )
            result.append( { (270 + angle) : ( pair[1], -pair[0]) } )
            result.append( { ( 90 + angle) : (-pair[1],  pair[0]) } )
            result.append( { (270 - angle) : (-pair[1], -pair[0]) } )
        return result
