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

    Decimal.getcontext().prec = 100

    # Calculated with Mathematica (see Matheamtica.nb) to a precision of 100 places behind decimal point
    angles = {
        0:   (Decimal("1.0"),
              Decimal("0.0")),
        10:  (Decimal("0.98480775301220805936674302458952301367064325171984"
                      "24187900257523558275999430362392746784100561198992"),
              Decimal("0.17364817766693034885171662676931479600037567718406"
                      "93872362413781320658221390147354215166131573995740")),
        22:  (Decimal("0.92718385456678740080647445113695694209762171984899"
                      "76151430209124095628534066347424128493580393263935"),
              Decimal("0.37460659341591203541496377450119513100015892225367"
                      "61741034403710333586143660852189586357362224852295")),
        23:  (Decimal("0.92050485345244032739689472330046142027950328097024"
                      "03012627017117829537900873103657545739776622661136"),
              Decimal("0.39073112848927375506208458888909426761801516757643"
                      "20757471065494645546820718925532166282293840540004")),
        45:  (Decimal("0.70710678118654752440084436210484903928483593768847"
                      "40365883398689953662392310535194251937671638207864"),
              Decimal("0.70710678118654752440084436210484903928483593768847"
                      "40365883398689953662392310535194251937671638207864")),
        90:  (Decimal("0.0"),
              Decimal("1.0")),
        280: (Decimal("0.17364817766693034885171662676931479600037567718406"
                      "93872362413781320658221390147354215166131573995740"),
              Decimal("-0.98480775301220805936674302458952301367064325171984"
                      "24187900257523558275999430362392746784100561198992")),
        359: (Decimal("0.99984769515639123915701155881391485169274031058318"
                      "59396583207145115391811033372153972993952881103455"),
              Decimal("-0.0174524064372835128194189785163161924722527203071"
                      "3964268361242764059738420392807004200192679102134691"))
    }

    k = Kreis()

    print("This program compares different ways to calculate the points at the unit circle.")
    print("For more information about the algorithms, see EWR/ab5 document.")
    print()

    print("The points for 360 angles will be calculated...")

    k.reset_function_call_counter()
    n = k.naiv()
    print("Number of calls of sin or cos in naiv(): {0}".format(k.function_call_counter))

    k.reset_function_call_counter()
    e = k.effizient()
    print("Number of calls of sin or cos in effizient(): {0}".format(k.function_call_counter))

    k.reset_function_call_counter()
    s = k.symmetrie()
    print("Number of calls of sin or cos in symmetrie(): {0}".format(k.function_call_counter))

    print("All points were calculated.")
    print()
    print("Some calculated points will be compared to more exact calculated values.")
    print()

    for angle in angles:
        print("Compare of exactness with an angle of {0}".format(angle))
        print("x/y calculated with Mathematica with 100 places behind decimal point:")
        print("m_x: {0}".format(angles[angle][0]))
        print("m_y: {0}".format(angles[angle][1]))
        print()

        print("Calculated with naiv():")
        print("n_x: {0}".format(n[angle][0]))
        print("n_y: {0}".format(n[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - n_x| = {0}".format(np.abs(n[angle][0] - angles[angle][0])))
        print("|m_x - n_x| = {0}".format(np.abs(n[angle][0] - angles[angle][0])))
        print("Relative approximation error from m_x/m_y:")
        print("|(m_x - n_x)/m_x| = {0}".format(np.abs((n[angle][0] - angles[angle][0]) / n[angle][0])))
        print("|(m_y - n_y)/m_y| = {0}".format(np.abs((n[angle][1] - angles[angle][1]) / n[angle][1])))
        print("|(m_x - n_x)/n_x| = {0}".format(np.abs((n[angle][0] - angles[angle][0]) / angles[angle][0])))
        print("|(m_y - n_y)/n_y| = {0}".format(np.abs((n[angle][1] - angles[angle][1]) / angles[angle][1])))
        print()

        print("Calculated with effizient():")
        print("e_x: {0}".format(e[angle][0]))
        print("e_y: {0}".format(e[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - e_x| = {0}".format(np.abs(e[angle][0] - angles[angle][0])))
        print("|m_x - e_x| = {0}".format(np.abs(e[angle][0] - angles[angle][0])))
        print("Relative approximation error from m_x/m_y:")
        print("|(m_x - e_x)/m_x| = {0}".format(np.abs((e[angle][0] - angles[angle][0]) / e[angle][0])))
        print("|(m_y - e_y)/m_y| = {0}".format(np.abs((e[angle][1] - angles[angle][1]) / e[angle][1])))
        print("|(m_x - e_x)/n_x| = {0}".format(np.abs((e[angle][0] - angles[angle][0]) / angles[angle][0])))
        print("|(m_y - e_y)/n_y| = {0}".format(np.abs((e[angle][1] - angles[angle][1]) / angles[angle][1])))
        print()

        print("Calculated with symmetrie():")
        print("s_x: {0}".format(s[angle][0]))
        print("s_y: {0}".format(s[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - s_x| = {0}".format(np.abs(s[angle][0] - angles[angle][0])))
        print("|m_x - s_x| = {0}".format(np.abs(s[angle][0] - angles[angle][0])))
        print("Relative approximation error from m_x/m_y:")
        print("|(m_x - s_x)/m_x| = {0}".format(np.abs((s[angle][0] - angles[angle][0]) / s[angle][0])))
        print("|(m_y - s_y)/m_y| = {0}".format(np.abs((s[angle][1] - angles[angle][1]) / s[angle][1])))
        print("|(m_x - s_x)/n_x| = {0}".format(np.abs((s[angle][0] - angles[angle][0]) / angles[angle][0])))
        print("|(m_y - s_y)/n_y| = {0}".format(np.abs((s[angle][1] - angles[angle][1]) / angles[angle][1])))
        print()
        print("---------------------------------------------------------------------------")
        print()

    print("All tests run")

# always execute main(), because no user input is required
main()
