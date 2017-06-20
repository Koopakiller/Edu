#!/usr/bin/env python

# Author: Tom Lambert
# Content: Main program for tasks in ab5.py

from __future__ import print_function
from kreis import Kreis
from decimal import *
import numpy as np
from utils import *


def main():
    """
    The main function of this program.
    :return: Nothing.
    """

    getcontext().prec = 20

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

    lst = {
        "absolute":  {"n": {"x": [], "y": []}, "e": {"x": [], "y": []}, "s": {"x": [], "y": []}},
        "relative1": {"n": {"x": [], "y": []}, "e": {"x": [], "y": []}, "s": {"x": [], "y": []}},
        "relative2": {"n": {"x": [], "y": []}, "e": {"x": [], "y": []}, "s": {"x": [], "y": []}}
    }

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
        print("|m_x - n_x| = {0}".format(np.abs(nai[angle][1] - angles[angle][1])))
        lst["absolute"]["n"]["x"].append(np.abs(nai[angle][0] - angles[angle][0]))
        lst["absolute"]["n"]["y"].append(np.abs(nai[angle][1] - angles[angle][1]))

        print("Relative approximation error from m_x/m_y:")
        if nai[angle][0] != 0:
            lst["relative1"]["n"]["x"].append(np.abs((nai[angle][0] - angles[angle][0]) / nai[angle][0]))
            print("|(m_x - n_x)/n_x| = {0}".format(np.abs((nai[angle][0] - angles[angle][0]) / nai[angle][0])))
        else:
            print("|(m_x - n_x)/n_x| = not defined (because n_x = 0)")
        if nai[angle][1] != 0:
            lst["relative1"]["n"]["y"].append(np.abs((nai[angle][1] - angles[angle][1]) / nai[angle][1]))
            print("|(m_y - n_y)/n_y| = {0}".format(np.abs((nai[angle][1] - angles[angle][1]) / nai[angle][1])))
        else:
            print("|(m_y - n_y)/n_y| = not defined (because n_y = 0)")

        if angles[angle][0] != 0:
            lst["relative2"]["n"]["x"].append(np.abs((nai[angle][0] - angles[angle][0]) / angles[angle][0]))
            print("|(m_x - n_x)/m_x| = {0}".format(np.abs((nai[angle][0] - angles[angle][0]) / angles[angle][0])))
        else:
            print("|(m_x - n_x)/m_x| = not defined (because m_x = 0)")
        if angles[angle][1] != 0:
            lst["relative2"]["n"]["y"].append(np.abs((nai[angle][1] - angles[angle][1]) / angles[angle][1]))
            print("|(m_y - n_y)/m_y| = {0}".format(np.abs((nai[angle][1] - angles[angle][1]) / angles[angle][1])))
        else:
            print("|(m_y - n_y)/m_y| = not defined (because m_y = 0)")
        print()

        print("Calculated with effizient():")
        print("e_x: {0}".format(eff[angle][0]))
        print("e_y: {0}".format(eff[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - e_x| = {0}".format(np.abs(eff[angle][0] - angles[angle][0])))
        print("|m_x - e_x| = {0}".format(np.abs(eff[angle][1] - angles[angle][1])))
        lst["absolute"]["e"]["x"].append(np.abs(eff[angle][0] - angles[angle][0]))
        lst["absolute"]["e"]["y"].append(np.abs(eff[angle][1] - angles[angle][1]))

        print("Relative approximation error from m_x/m_y:")
        if eff[angle][0] != 0:
            lst["relative1"]["e"]["x"].append(np.abs((eff[angle][0] - angles[angle][0]) / eff[angle][0]))
            print("|(m_x - e_x)/e_x| = {0}".format(np.abs((eff[angle][0] - angles[angle][0]) / eff[angle][0])))
        else:
            print("|(m_x - e_x)/e_x| = not defined (because e_x = 0)")
        if eff[angle][1] != 0:
            lst["relative1"]["e"]["y"].append(np.abs((eff[angle][1] - angles[angle][1]) / eff[angle][1]))
            print("|(m_y - e_y)/e_y| = {0}".format(np.abs((eff[angle][1] - angles[angle][1]) / eff[angle][1])))
        else:
            print("|(m_y - e_y)/e_y| = not defined (because e_y = 0)")

        if angles[angle][0] != 0:
            lst["relative2"]["e"]["x"].append(np.abs((eff[angle][0] - angles[angle][0]) / angles[angle][0]))
            print("|(m_x - e_x)/n_x| = {0}".format(np.abs((eff[angle][0] - angles[angle][0]) / angles[angle][0])))
        else:
            print("|(m_x - e_x)/m_x| = not defined (because m_x = 0)")
        if angles[angle][1] != 0:
            lst["relative2"]["e"]["y"].append(np.abs((eff[angle][1] - angles[angle][1]) / angles[angle][1]))
            print("|(m_y - e_y)/m_y| = {0}".format(np.abs((eff[angle][1] - angles[angle][1]) / angles[angle][1])))
        else:
            print("|(m_y - e_y)/m_y| = not defined (because m_y = 0)")
        print()

        print("Calculated with symmetrie():")
        print("s_x: {0}".format(sym[angle][0]))
        print("s_y: {0}".format(sym[angle][1]))
        print("Absolute approximation error from m_x/m_y:")
        print("|m_x - s_x| = {0}".format(np.abs(sym[angle][0] - angles[angle][0])))
        print("|m_x - s_x| = {0}".format(np.abs(sym[angle][1] - angles[angle][1])))
        lst["absolute"]["s"]["x"].append(np.abs(sym[angle][0] - angles[angle][0]))
        lst["absolute"]["s"]["y"].append(np.abs(sym[angle][1] - angles[angle][1]))

        print("Relative approximation error from m_x/m_y:")
        if sym[angle][0] != 0:
            lst["relative1"]["s"]["x"].append(np.abs((sym[angle][0] - angles[angle][0]) / sym[angle][0]))
            print("|(m_x - s_x)/s_x| = {0}".format(np.abs((sym[angle][0] - angles[angle][0]) / sym[angle][0])))
        else:
            print("|(m_x - s_x)/s_x| = not defined (because s_x = 0)")
        if sym[angle][1] != 0:
            lst["relative1"]["s"]["y"].append(np.abs((sym[angle][1] - angles[angle][1]) / sym[angle][1]))
            print("|(m_y - s_y)/s_y| = {0}".format(np.abs((sym[angle][1] - angles[angle][1]) / sym[angle][1])))
        else:
            print("|(m_y - s_y)/s_y| = not defined (because s_y = 0)")

        if angles[angle][0] != 0:
            lst["relative2"]["s"]["x"].append(np.abs((sym[angle][0] - angles[angle][0]) / angles[angle][0]))
            print("|(m_x - s_x)/m_x| = {0}".format(np.abs((sym[angle][0] - angles[angle][0]) / angles[angle][0])))
        else:
            print("|(m_x - s_x)/m_x| = not defined (because m_x = 0)")
        if angles[angle][1] != 0:
            lst["relative2"]["s"]["y"].append(np.abs((sym[angle][1] - angles[angle][1]) / angles[angle][1]))
            print("|(m_y - s_y)/m_y| = {0}".format(np.abs((sym[angle][1] - angles[angle][1]) / angles[angle][1])))
        else:
            print("|(m_y - s_y)/m_y| = not defined (because m_y = 0)")
        print()
        print("---------------------------------------------------------------------------")
        print()

    print("The average absolute errors are:")
    print("naiv x:      {0}".format(average(lst["absolute"]["n"]["x"])))
    print("effizient x: {0}".format(average(lst["absolute"]["e"]["x"])))
    print("symmetrie x: {0}".format(average(lst["absolute"]["s"]["x"])))
    print("naiv y:      {0}".format(average(lst["absolute"]["n"]["y"])))
    print("effizient y: {0}".format(average(lst["absolute"]["e"]["y"])))
    print("symmetrie y: {0}".format(average(lst["absolute"]["s"]["y"])))
    print()

    print("The average relative errors (with m_x/m_y in the denominator) are:")
    print("naiv x:      {0}".format(average(lst["relative1"]["n"]["x"])))
    print("effizient x: {0}".format(average(lst["relative1"]["e"]["x"])))
    print("symmetrie x: {0}".format(average(lst["relative1"]["s"]["x"])))
    print("naiv y:      {0}".format(average(lst["relative1"]["n"]["y"])))
    print("effizient y: {0}".format(average(lst["relative1"]["e"]["y"])))
    print("symmetrie y: {0}".format(average(lst["relative1"]["s"]["y"])))
    print()

    print("The average relative errors (with n/e/s _x/_y in the denominator) are:")
    print("naiv x:      {0}".format(average(lst["relative2"]["n"]["x"])))
    print("effizient x: {0}".format(average(lst["relative2"]["e"]["x"])))
    print("symmetrie x: {0}".format(average(lst["relative2"]["s"]["x"])))
    print("naiv y:      {0}".format(average(lst["relative2"]["n"]["y"])))
    print("effizient y: {0}".format(average(lst["relative2"]["e"]["y"])))
    print("symmetrie y: {0}".format(average(lst["relative2"]["s"]["y"])))
    print()

    print("All tests run")

# always execute main(), because no user input is required
main()
