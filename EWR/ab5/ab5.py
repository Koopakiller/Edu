#!/usr/bin/env python

# Author: Tom Lambert
# Content: Main program for tasks in ab5.py

import kreis
from decimal import Decimal

# Calculated with Mathematica (see Matheamtica.nb) to a precision of 100 places behind decimal point
angles = {
    0: (Decimal("1.0"), Decimal("0.0")),
    10: (Decimal("0.98480775301220805936674302458952301367064325171984241879002575235582\
75999430362392746784100561198992"), Decimal("0.17364817766693034885171662676931479600037567718406938723624137813206\
58221390147354215166131573995740")),
    22: (Decimal("0.92718385456678740080647445113695694209762171984899761514302091240956\
28534066347424128493580393263935"), Decimal("0.37460659341591203541496377450119513100015892225367617410344037103335\
86143660852189586357362224852295")),
    23: (Decimal("0.92050485345244032739689472330046142027950328097024030126270171178295\
37900873103657545739776622661136"), Decimal("0.39073112848927375506208458888909426761801516757643207574710654946455\
46820718925532166282293840540004")),
    45: (Decimal("0.70710678118654752440084436210484903928483593768847403658833986899536\
62392310535194251937671638207864"), Decimal("0.70710678118654752440084436210484903928483593768847403658833986899536\
62392310535194251937671638207864")),
    90: (Decimal("0.0"), Decimal("1.0")),
    280: (Decimal("0.17364817766693034885171662676931479600037567718406938723624137813206\
58221390147354215166131573995740"), Decimal("-0.9848077530122080593667430245895230136706432517198424187900257523558\
275999430362392746784100561198992")),
    359: (Decimal("0.99984769515639123915701155881391485169274031058318593965832071451153\
91811033372153972993952881103455"), Decimal("-0.0174524064372835128194189785163161924722527203071396426836124276405\
9738420392807004200192679102134691"))
}

k = Kreis()

k.reset_function_call_counter()
n = k.naiv()
print("Anzahl der Aufrufe von sin oder cos in naiv(): {0}".format(k.function_call_counter))

k.reset_function_call_counter()
e = k.effizient()
print("Anzahl der Aufrufe von sin oder cos in effizient(): {0}".format(k.function_call_counter))

k.reset_function_call_counter()
s = k.symmetrie()
print("Anzahl der Aufrufe von sin oder cos in symmetrie(): {0}".format(k.function_call_counter))

for angle in angles:
    print("Vergleich")