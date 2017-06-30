# coding=utf-8
# Author: Tom Lambert, Phillip Kries
# Inhalt: Gibt aus, wie viele Vokale das Wort enthält, welches der Benutzer eingegeben hat.

if __name__ == "__main__":
    s = raw_input("Bitte geben Sie ein Wort ein: ")

    print("Länge der Zeichenfolge: " + str(len(s)))
    print("Anzahl der Vorkommen von Vokalen: ")
    print("  a oder A = " + str(s.count("a") + s.count("A")))
    print("  e oder E = " + str(s.count("e") + s.count("E")))
    print("  i oder I = " + str(s.count("i") + s.count("I")))
    print("  o oder O = " + str(s.count("o") + s.count("O")))
    print("  u oder U = " + str(s.count("u") + s.count("U")))
