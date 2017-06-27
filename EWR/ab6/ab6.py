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


def compare_results(results, should_result):
    """
    Compares results of sort algorithms with the python standard and prints them.
    :param results: A dictionary of results.
    The keys are the name of the sort algorithms and the values the sorted list.
    :param should_result: The result, sorted by the python default.
    :return: Nothing.
    """
    for result_key in results:
        print("Testing the result of {0}".format(result_key))
        if len(results[result_key]) != len(should_result):
            print("The length of the list is correct.")
            counter = 0
            for index in range(0, len(should_result)):
                if results[result_key][index] != should_result[index]:
                    counter += 0
            if counter == 0:
                print("The result of {0} matches the result of the standard python sort method.".format(result_key))
            else:
                print("The result of {0} does not match the result of the standard python sort method."
                      .format(result_key))
                print("{0} out of {1} words are different".format(counter, len(should_result)))
        else:
            print("The length of the List is incorrect. It should be {0} but it is {1}."
                  .format(len(should_result), len(results[result_key])))
        print()


def print_function_calls(calls):
    """
    Prints the dictionary with the number of function calls.
    :param calls: A dictionary with function-names as keys and call-counts as values.
    :return: Nothing,
    """
    for key in calls:
        if calls[key] == 1:
            print("Function '{0}' was called once.".format(key))
        elif calls[key] == 2:
            print("Function '{0}' was called twice.".format(key))
        elif calls[key] > 2:
            print("Function '{0}' was called {1} times".format(key, calls[key]))


def main():
    """
    The main program and logic of the program ab6.
    :return: Nothing.
    """

    print("This program executes some sort algorithms and compares them to the standard python implementation.")
    print("The program reads words (delimited by space) from a file.")
    path = get_file_name()
    if not os.path.isfile(path):
        print("File '{0}' does not exists.".format(path))
        return

    print("The file '{0}' will be used.".format(path))

    # noinspection PyBroadException
    try:
        words = read_words_from_file(path)
    except:
        print("An unknown error occurred.")
        return

    print()
    output_list("The following words wre found:", words)

    sort_algorithms = [
        MergeSort(),
        QuickSort(),
        GnomeSort()
    ]
    results = {}

    print()
    for sort_algorithm in sort_algorithms:
        result = sort_algorithm.sort(words)
        output_list("The sort-method from {0} returned this list:".format(sort_algorithm.name), result)
        results.update({sort_algorithm.name: result})
        print()

    standard_sort_algorithm = PythonSort()
    should_result = standard_sort_algorithm.sort(words)
    output_list("The sort-method from {0} returned this list:".format(standard_sort_algorithm.name), should_result)

    compare_results(results, should_result)

main()  # always execute main(), __name__ is checked in other places
