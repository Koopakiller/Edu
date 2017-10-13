# Author: Tom Lambert
# Content: UnitTests for Sum-class

import unittest

from prime import Prime


class PrimeTests(unittest.TestCase):

    def test_get_prime(self):
        self.assertEqual(Prime.get_prime(0), 2)
        self.assertEqual(Prime.get_prime(1), 3)
        self.assertEqual(Prime.get_prime(2), 5)
        self.assertEqual(Prime.get_prime(3), 7)
        self.assertEqual(Prime.get_prime(4), 11)
        self.assertEqual(Prime.get_prime(5), 13)
        self.assertEqual(Prime.get_prime(6), 17)
        self.assertEqual(Prime.get_prime(7), 19)

    def test_get_prime_factors(self):
        self.assertRaises(ValueError, lambda: Prime.get_prime_factors(1))
        self.assertRaises(ValueError, lambda: Prime.get_prime_factors(0))
        self.assertRaises(ValueError, lambda: Prime.get_prime_factors(-1))
        self.assertRaises(ValueError, lambda: Prime.get_prime_factors(-3))
        self.assertListEqual(Prime.get_prime_factors(2), [2])
        self.assertListEqual(Prime.get_prime_factors(2 * 3 * 5 * 7 * 7 * 11), [2, 3, 5, 7, 7, 11])

    def test_get_greatest_common_divisor(self):
        self.assertEqual(Prime.get_greatest_common_divisor(3 * 5 * 7 * 7 * 11, 5 * 7 * 11 * 11 * 13), 5 * 7 * 11)
        self.assertEqual(Prime.get_greatest_common_divisor(1, 2), 1)
        self.assertEqual(Prime.get_greatest_common_divisor(-1, 2), 1)
        self.assertEqual(Prime.get_greatest_common_divisor(-1, -2), 1)
        self.assertEqual(Prime.get_greatest_common_divisor(2, -1), 1)
        self.assertEqual(Prime.get_greatest_common_divisor(-2, 1), 1)
        self.assertEqual(Prime.get_greatest_common_divisor(-2, -1), 1)

if __name__ == '__main__':
    unittest.main()
