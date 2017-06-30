# coding=utf-8
# Author: Tom Lambert, Phillip Kries
# Inhalt: Gibt aus, wie viele Vokale das Wort enthält, welches der Benutzer eingegeben hat.


def main1():
    s = raw_input("Bitte geben Sie ein Wort ein: ")

    print("Länge der Zeichenfolge: " + str(len(s)))
    print("Anzahl der Vorkommen von Vokalen: ")
    print("  a oder A = " + str(s.count("a") + s.count("A")))
    print("  e oder E = " + str(s.count("e") + s.count("E")))
    print("  i oder I = " + str(s.count("i") + s.count("I")))
    print("  o oder O = " + str(s.count("o") + s.count("O")))
    print("  u oder U = " + str(s.count("u") + s.count("U")))


def main2():
    s = raw_input("Bitte geben Sie ein Wort ein: ")

    print("Länge der Zeichenfolge: " + str(len(s)))
    print("Anzahl der Vorkommen von Vokalen: ")

    vowels = list("aeiou")
    for vowel in vowels:
        lower = str(vowel).lower()[0]
        upper = str(vowel).upper()[0]
        print("  " + str(lower) + "        = " + str(s.count(lower)))
        print("  " + str(upper) + "        = " + str(s.count(upper)))
        print("  " + str(lower) + " oder " + str(upper) + " = " + str(s.count(lower) + s.count(upper)))

if __name__ == "__main__":
    main2()
