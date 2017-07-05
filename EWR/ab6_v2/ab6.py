# coding=utf-8
# Author: Tom Lambert
# Content: Hauptprogramm für EWR/ab6 (Sortieralgorithmen)


from __future__ import print_function
from Sort import *
import os.path
import time
import os.path


def print_line():
    """Gibt eine Trennlinie in der Konsole aus."""
    print("-------------------------------------------------------------------------------------")


def print_list(lst):
    """Gibt eine Liste aus."""
    # Entspricht der Standard-Python-Ausgabe, jedoch mit lesbarer Darstellung von Umlauten
    print("[ '{0}' ]".format("', '".join(lst)))


def input_file_name(msg):
    """Fragt einen Dateipfad vom Benutzer ab. Bei Falscheingabe wird er erneut gefragt."""
    while True:
        user_input = raw_input(msg)
        if os.path.isfile(user_input):
            return user_input
        print("Die Datei existiert nicht!")


def compare_lists(a, b):
    """
      Vergleicht 2 Listen mit einander und bestimmt ob diese gleich sind; falls nicht, wie viele Elemente verschieden
      sind. Sollten die Längen unterschiedlich sein, werden nur diese zurück gegeben.
    """
    if len(a) != len(b):
        return "length", len(a), len(b)
    else:
        counter = 0
        for i in range(0, len(a)):
            if a[i] != b[i]:
                counter += 1
        if counter == 0:
            return "ok"
        else:
            return "different", counter, len(a) - counter


def main():
    """Führt die Logik des Programms aus."""
    print("Dieses Programm sortiert eine Liste mit Wörtern mit verschiedenen Algorithmen und "
          "wertet die unterschiedlichen Vorgehensweißen statistisch aus.")
    print("Die zu sortierenden Wörter werden von einer Datei eingelesen.")

    if __name__ == "__main__":
        path = input_file_name("Geben Sie eine Datei mit zu sortierenden Wörtern an: ")
    else:
        path = "test.txt"
        print("Das Programm wird nicht im Nutzer-Kontext ausgeführt, daher wird 'test.txt' als Datei genutzt.")

    if not os.path.isfile(path):
        print("Die Datei '{0}' existiert nicht.".format(path))
        return
    print()

    # noinspection PyBroadException
    try:
        file_obj = open(path, "r")
        file_content = file_obj.read()
        words = file_content.split(" ")
    except:
        print("Ein unbekannter Fehler ist aufgetreten. Das Programm wird beendet.")
        return

    print_line()
    print()

    words_distinct = list(set(words))
    words_sorted = list(words)
    time_start = time.time()
    words_sorted.sort()
    time_end = time.time()

    print("Die folgenden {0} Wörter wurden gefunden:".format(len(words)))
    print_list(words)
    print()

    if len(words) == len(words_distinct):
        print("Die Liste der Wörter enthält keine doppelten Einträge.")
    else:
        print("Die Liste der Wörter enthält doppelte Einträge.")
        print("Dies sind die {0} eindeutigen Wörter:".format(len(words_distinct)))
        print_list(words_distinct)
    print()

    print("Mit Pythons Standard-sort-Methode sortiert, ergibt sich folgende Liste:")
    print_list(words_sorted)
    print("Diese Sortierung hat {0}ms gedauert".format((time_end - time_start) * 1000))
    print()

    print_line()
    print()

    sort_algorithms = {
        "Gnome Sort": lambda sort: sort.gnome_sort(words),
        "Quick Sort": lambda sort: sort.quick_sort(words),
        "Insertion Sort": lambda sort: sort.insertion_sort(words)
    }

    succeeded = 0
    for key in sort_algorithms:
        print_line()

        # https://stackoverflow.com/a/7370824/1623754
        sort = Sort()
        time_start = time.time()
        result = sort_algorithms[key](sort)
        time_end = time.time()

        print("'{0}' hat folgende sortierte Liste zurück gegeben:")
        print_list(result)
        print("  Die Sortierung hat {0}ms gedauert".format((time_end - time_start) * 1000))
        print("  Statistik über die ausgeführten Operationen:")
        print("   - swap (elemente tauschen): . . . . . . . . . . . {0}".format(sort.counter_swap))
        print("   - Element zu einer Liste hinzufügen:  . . . . . . {0}".format(sort.counter_add_item_to_result_list))
        print("   - Liste kopieren (für gleiche Start-Bedingugnen): {0}".format(sort.counter_copy_list))
        print("   - Element aus Liste abrufen:  . . . . . . . . . . {0}".format(sort.counter_get_item_from_list))
        print("   - 2 Elemente vergleichen: . . . . . . . . . . . . {0}".format(sort.counter_item_compare))
        print("   - Element in Liste zuweisen:  . . . . . . . . . . {0}".format(sort.counter_list_item_assignment))
        print("   - Rekursiver Funktionsaufruf: . . . . . . . . . . {0}".format(sort.counter_recursive_call))
        print("   - Aufrufe der Sortier-Funktion: . . . . . . . . . {0}".format(sort.counter_sort_call))
        print("   - Aufteilen der Liste:  . . . . . . . . . . . . . {0}".format(sort.counter_split_list))
        print()

        print("Die von '{0}' sortierte Liste wird mit der von Python sortierten Liste verglichen.".format(key))
        compare = compare_lists(words_sorted, result)
        print("Der Vergleich wurde beendet, das Ergebnis lautet:")
        if compare[0] == "ok":
            print("Die Listen stimmen überein.")
            succeeded += 1
        elif compare[0] == "length":
            print("Die Längen ({0} und {1}) stimmen nicht überein.".format(compare[1], compare[2]))
        elif compare[0] == "different":
            print("Die Listen stimmen nicht überein. {0} Elemente sind unterschiedlich, {1} sind gleich."
                  .format(compare[1], compare[2]))
        else:
            print("Unbekanntes Ergebnis. Die Listen stimmen vermutlich nicht überein.")

    print()
    print("{0} Sortieralgorithmen arbeiten korrekt, {0} nicht.".format(succeeded, len(sort_algorithms) - succeeded))

main()  # immer main() ausführen, __name__ wird (wenn notwendig) im inneren überprüft.
