#!/usr/bin/env python

# Author: Tom Lambert
# Content: Main program for tasks in ab5.py

from kreis import Kreis
from decimal import Decimal
from numpy as np

def main():
    """
    The main function of this program.
    :return: Nothing.
    """

    Decimal.getcontext().prec = 20

    # Calculated with Mathematica (see Mathematica.nb) to a precision of 20 places behind decimal point
    angles = {
        0:   (Decimal( "1.00000000000000000000"),
              Decimal( "0.00000000000000000000")),
        10:  (Decimal( "0.98480775301220805936"),
              Decimal( "0.17364817766693034885")),
        22:  (Decimal( "0.92718385456678740080"),
              Decimal( "0.37460659341591203541")),
        23:  (Decimal( "0.92050485345244032739"),
              Decimal( "0.39073112848927375506")),
        45:  (Decimal( "0.70710678118654752440"),
              Decimal( "0.70710678118654752440")),
        90:  (Decimal( "0.00000000000000000000"),
              Decimal( "1.00000000000000000000")),
        280: (Decimal( "0.17364817766693034885"),
              Decimal("-0.98480775301220805936")),
        359: (Decimal( "0.99984769515639123915"),
              Decimal("-0.01745240643728351281"))
    }

    k = Kreis()

    print("This program compares different ways to calculate the points at the unit circle.")
    print("For more information about the algorithms, see EWR/ab5 document.")
    print()

    print("The points for 360 angles will be calculated...")

    k.reset_function_call_counter()
    nai = k.naiv()
    print("Number of calls of sin or cos in naiv(): {0}".format(k.function_call_counter))

    k.reset_function_call_counter()
    eff = k.effizient()
    print("Number of calls of sin or cos in effizient(): {0}".format(k.function_call_counter))

    k.reset_function_call_counter()
    sym = k.symmetrie()
    print("Number of calls of sin or cos in symmetrie(): {0}".format(k.function_call_counter))

    print("All points were calculated.")
    print()
    print("---------------------------------------------------------------------------")
    print()
    print("Some calculated points will be compared to more exact calculated values.")
    print()

    for angle in angles:
        print("Compare of exactness with an angle of {0}".format(angle))
        print("x/y calculated with Mathematica with 20 places behind decimal point:")
        print("m_x: {0}".format(angles[angle][0]))
        print("m_y: {0}".format(angles[angle][1]))
        print()

        print("Calculated with naiv():")
        print("n_x: {0}".format(nai[angle][0]))
        print("n_y: {0}".format(nai[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - n_x| = {0}".format(np.abs(nai[angle][0] - angles[angle][0])))
        print("|m_x - n_x| = {0}".format(np.abs(nai[angle][0] - angles[angle][0])))
        print("Relative approximation error from m_x/m_y:")
        print("|(m_x - n_x)/m_x| = {0}".format(np.abs((nai[angle][0] - angles[angle][0]) / nai[angle][0])))
        print("|(m_y - n_y)/m_y| = {0}".format(np.abs((nai[angle][1] - angles[angle][1]) / nai[angle][1])))
        print("|(m_x - n_x)/n_x| = {0}".format(np.abs((nai[angle][0] - angles[angle][0]) / angles[angle][0])))
        print("|(m_y - n_y)/n_y| = {0}".format(np.abs((nai[angle][1] - angles[angle][1]) / angles[angle][1])))
        print()

        print("Calculated with effizient():")
        print("e_x: {0}".format(eff[angle][0]))
        print("e_y: {0}".format(eff[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - e_x| = {0}".format(np.abs(eff[angle][0] - angles[angle][0])))
        print("|m_x - e_x| = {0}".format(np.abs(eff[angle][0] - angles[angle][0])))
        print("Relative approximation error from m_x/m_y:")
        print("|(m_x - e_x)/m_x| = {0}".format(np.abs((eff[angle][0] - angles[angle][0]) / eff[angle][0])))
        print("|(m_y - e_y)/m_y| = {0}".format(np.abs((eff[angle][1] - angles[angle][1]) / eff[angle][1])))
        print("|(m_x - e_x)/n_x| = {0}".format(np.abs((eff[angle][0] - angles[angle][0]) / angles[angle][0])))
        print("|(m_y - e_y)/n_y| = {0}".format(np.abs((eff[angle][1] - angles[angle][1]) / angles[angle][1])))
        print()

        print("Calculated with symmetrie():")
        print("s_x: {0}".format(sym[angle][0]))
        print("s_y: {0}".format(sym[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - s_x| = {0}".format(np.abs(sym[angle][0] - angles[angle][0])))
        print("|m_x - s_x| = {0}".format(np.abs(sym[angle][0] - angles[angle][0])))
        print("Relative approximation error from m_x/m_y:")
        print("|(m_x - s_x)/m_x| = {0}".format(np.abs((sym[angle][0] - angles[angle][0]) / sym[angle][0])))
        print("|(m_y - s_y)/m_y| = {0}".format(np.abs((sym[angle][1] - angles[angle][1]) / sym[angle][1])))
        print("|(m_x - s_x)/n_x| = {0}".format(np.abs((sym[angle][0] - angles[angle][0]) / angles[angle][0])))
        print("|(m_y - s_y)/n_y| = {0}".format(np.abs((sym[angle][1] - angles[angle][1]) / angles[angle][1])))
        print()
        print("---------------------------------------------------------------------------")
        print()

    print("All tests run")

# always execute main(), because no user input is required
main()
