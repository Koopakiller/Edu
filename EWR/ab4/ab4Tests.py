import unittest

from ab4 import Sum


class SumTest(unittest.TestCase):

    def test_start_value(self):
        obj = Sum(lambda x: x**2)
        obj.end_value = 1000

        with self.assertRaises(ValueError):
            obj.start_value = 12.345

        with self.assertRaises(ValueError):
            obj.start_value = 200

        obj.start_value = 123
        self.assertEqual(obj.start_value, 123)

    def test_end_value(self):
        obj = Sum(lambda x: x**2)
        obj.start_value = 0

        with self.assertRaises(ValueError):
            obj.end_value = 12.345

        with self.assertRaises(ValueError):
            obj.end_value = -12

        obj.end_value = 123
        self.assertEqual(obj.start_value, 123)

    def test_calculate(self):
        obj = Sum(lambda x: x**2)
        obj.start_value = 2
        obj.end_value = 5

        self.assertEqual(obj.calculate(), 2*2 + 3*3 + 4*4 + 5*5)


if __name__ == '__main__':
    unittest.main()
