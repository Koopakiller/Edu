# coding=utf-8
# Author: Tom Lambert, Phillip Kries
# Inhalt: Zeigt dem Benutzer das kleine Einmaleins für eine Zahl zwischen 0 und 20 an


def read_integer():
    """Liest eine Zahl vom Benutzer zwischen 0 und inkl. 20 ein"""

    print("Bitte geben Sie eine ganze Zahl zwischen 0 und 20 ein")

    res = raw_input()

    try:
        res = int(res)
    except ValueError:
        print("Die Eingabe war keine Zahl!")
        return read_integer()

    if 0 < res <= 20:
        return res

    print("Die Zahl lag nicht zwischen 0 und 20!")
    return read_integer()


if __name__ == "__main__":
    x = read_integer()

    for i in range(0, 10 + 1):
        print(str(i) + " * " + str(x) + " = " + str(i*x))