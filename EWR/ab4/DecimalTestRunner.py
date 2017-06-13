# Author: Tom Lambert
# Content: Provides functions to compare decimals with different exponents and mantissas


class DecimalTestRunner:
    def __init__(self):
        self._min_exponent = 1
        self._max_exponent = 10
        self._min_mantissa = 1
        self._max_mantissa = 10
        self._custom_exponents = []
        self._custom_mantissas = []
        self._mantissas = []
        self._exponents = []

# min_exponent
    def _get_min_exponent(self):
        return self._min_exponent

    def _set_min_exponent(self, value):
        if value <= 0:
            raise ValueError("Exponent cannot less or equal 0")
        self._min_exponent = value
        self._update_exponents()

    min_exponent = property(_get_min_exponent, _set_min_exponent)

# max_exponent
    def _get_max_exponent(self):
        return self._max_exponent

    def _set_max_exponent(self, value):
        if value <= 0:
            raise ValueError("Exponent cannot less or equal 0")
        self._max_exponent = value
        self._update_exponents()

    max_exponent = property(_get_min_exponent, _set_min_exponent)

# min_mantissa
    def _get_min_mantissa(self):
        return self._min_mantissa

    def _set_min_mantissa(self, value):
        if value <= 0:
            raise ValueError("Mantissa cannot less or equal 0")
        self._min_mantissa = value
        self._update_mantissas()

    min_mantissa = property(_get_min_mantissa, _set_min_mantissa)

# max_mantissa
    def _get_max_mantissa(self):
        return self._max_mantissa

    def _set_max_mantissa(self, value):
        if value <= 0:
            raise ValueError("Mantissa cannot less or equal 0")
        self._max_mantissa = value
        self._update_mantissas()

    max_mantissa = property(_get_min_mantissa, _set_min_mantissa)

# custom_exponents
    def _get_custom_exponents(self):
        return self._custom_exponents

    def _set_custom_exponents(self, value):
        self._custom_exponents = value
        self._update_exponents()

    custom_exponents = property(_get_custom_exponents, _set_custom_exponents)

# custom_exponents
    def _get_custom_mantissas(self):
        return self._custom_exponents

    def _set_custom_mantissas(self, value):
        self._custom_mantissas = value
        self._update_mantissas()

    custom_mantissas = property(_get_custom_mantissas, _set_custom_mantissas)

# update lists
    def _update_exponents(self):
        """Updates the _exponents list."""
        self._exponents = range(self._min_exponent, self._max_exponent + 1)
        self._exponents.extend(self._custom_exponents)
        self._exponents = list(set(self._exponents))  # distinct the list

    def _update_mantissas(self):
        """Updates the _mantissas list."""
        self._mantissas = range(self._min_mantissa, self._max_mantissa + 1)
        self._mantissas.extend(self._custom_mantissas)
        self._mantissas = list(set(self._mantissas))  # distinct the list
