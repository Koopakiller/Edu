# Author: Tom Lambert
# Content: Main program for EWR/ab6 (sorting algorithms).

from SortAlgorithms import *
from ui import *
import os.path


def get_file_name():
    """
    Gets a file name from user in __name__ == "__main__" mode, otherwise returns a default file name.
    :return: A default file name respectively the file specified from the user.
    """
    if __name__ == "__main__":
        return input_file_name("Specify a file with words to sort:")
    else:
        return "test.txt"  # return a default file name for tests in all other cases to execute the program


def read_words_from_file(path):
    """
    Reads the content of a file and returns the content, split by space-character.
    :param path: The path of the file.
    :return: A list of words in this file.
    """
    file_obj = open(path, "r")
    content = file_obj.read()
    return content.split(" ")


def output_list(msg, lst):
    """
    Prints a message and a list of words to the user.
    :param msg: The message to print.
    :param lst: The list to print.
    :return: Nothing.
    """
    print(msg)
    print(lst)


def main():
    """
    The main program and logic of the program ab6.
    :return: Nothing.
    """
    path = get_file_name()
    if not os.path.isfile(path):
        print("File '{0}' does not exists.".format(path))
        return

    words = read_words_from_file(path)
    output_list("The following words wre found:", words)

    sort_algorithms = [
        MergeSort(),
        QuickSort(),
        GnomeSort(),
        PythonSort()
    ]

    for sort_algorithm in sort_algorithms:
        result = sort_algorithm.sort(words)
        output_list("The sort-method from {0} returned this list:".format(sort_algorithm.name), result)

main()  # always execute main(), __name__ is checked in other places
