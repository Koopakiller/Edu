# Authors: Tom Lambert (lambertt) and Yuuma Odaka-Falush (odafaluy)


class Prime:
    """Provides methods to obtain prime numbers and use them."""

    def __init__(self):
        pass

    # Cache for already calculated prime numbers
    cache = [2, 3]

    @staticmethod
    def get_prime(index):
        """
        Returns the prime number at the given index. The index starts with 0.
        :param index: The index of the requested prime number.
        :return: The prime number at position index.
        """
        while len(Prime.cache) < index + 1:
            Prime.append_next_to_cache()

        return Prime.cache[index]

    @staticmethod
    def append_next_to_cache():
        """
        Calculates the next prime number which is not in the cache.
        :return: The added prime number.
        """
        num = Prime.cache[len(Prime.cache) - 1] + 2
        while True:
            flag = True
            for p in Prime.cache:
                if num % p == 0:
                    num += 1
                    flag = False
                    break
            if flag:
                Prime.cache.append(num)
                return num

    @staticmethod
    def get_prime_factors(num):
        """
        Returns the prime factors of the given number.
        :param num: The number to split in prime factors.
        :return: An array of prime factors of num.
        """
        index = 0
        result = []
        while num > 1:
            prime = Prime.get_prime(index)
            if num % prime:
                result.append(num)
                num /= prime
            else:
                index += 1

        return result

    @staticmethod
    def get_greatest_common_divisor(a, b):
        """
        Calculates the greatest common divisor
        :param a: The first number.
        :param b: The second number.
        :return: The greatest common divisor of a and b.
        """
        n_factors = Prime.get_prime_factors(a)
        d_factors = Prime.get_prime_factors(b)
        q = 1
        for f in n_factors:
            if f in d_factors:
                q *= f
        return q
