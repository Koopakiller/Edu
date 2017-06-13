# Author: Tom Lambert
# Content: UnitTests for Sum-class

import unittest

from Sum import Sum


class SumTests(unittest.TestCase):

    def test(self):
        s = Sum(lambda x: x*x)
        s.start_value = 2
        s.end_value = 4

        self.assertEqual(s.calculate(), 2*2 + 3*3 + 4*4)
        self.assertTrue(2 in s._cache)
        self.assertEqual(len(s._cache), 1)
        self.assertTrue(4 in s._cache[2])
        self.assertEqual(len(s._cache[2]), 1)
        self.assertEqual(s._cache[2][4], 2*2 + 3*3 + 4*4)

        s.end_value = 6
        self.assertEqual(s.calculate(), 2*2 + 3*3 + 4*4 + 5*5 + 6*6)
        self.assertTrue(2 in s._cache)
        self.assertEqual(len(s._cache), 1)
        self.assertTrue(4 in s._cache[2])
        self.assertTrue(6 in s._cache[2])
        self.assertEqual(len(s._cache[2]), 2)
        self.assertEqual(s._cache[2][6], 2*2 + 3*3 + 4*4 + 5*5 + 6*6)

        s.end_value = 5
        self.assertEqual(s.calculate(), 2*2 + 3*3 + 4*4 + 5*5)
        self.assertTrue(2 in s._cache)
        self.assertEqual(len(s._cache), 1)
        self.assertTrue(4 in s._cache[2])
        self.assertTrue(5 in s._cache[2])
        self.assertTrue(6 in s._cache[2])
        self.assertEqual(len(s._cache[2]), 3)
        self.assertEqual(s._cache[2][5], 2*2 + 3*3 + 4*4 + 5*5)

        s.start_value = 3
        s.end_value = 4
        self.assertEqual(s.calculate(), 3*3 + 4*4)
        self.assertTrue(2 in s._cache)
        self.assertTrue(3 in s._cache)
        self.assertEqual(len(s._cache), 2)
        self.assertTrue(4 in s._cache[3])
        self.assertEqual(len(s._cache[3]), 1)
        self.assertEqual(s._cache[3][4], 3*3 + 4*4)

if __name__ == '__main__':
    unittest.main()
