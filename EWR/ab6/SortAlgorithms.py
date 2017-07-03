# Author: Tom Lambert
# Content: Implementation of sorting algorithms for EWR/ab6.

from abc import ABCMeta
from abc import abstractmethod


class SortAlgorithm(object):
    """A base-class for testing sorting algorithms."""
    __metaclass__ = ABCMeta

    def __init__(self):
        self.calls = {}
        self.name = None

    @abstractmethod
    def sort(self, lst):
        """
        If implemented, sorts the list.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        NotImplementedError()

    def increase_call_counter(self, name, count=1):
        """
        Increases the counter for the given name to generate statistics about sorting algorithms.
        :param name: The name of the used method.
        :param count: the number to increase the counter.
        :return: Nothing
        """
        if name in self.calls:
            self.calls[name] += count
        else:
            self.calls.update({ name: count})

    def chunk(self, lst, n):
        """
        Splits a list in n chunks.
        :param lst: The list to split.
        :param n: The number of entry in 1 chunk.
        :return: A list of chunks with items from lst.
        """
        self.increase_call_counter("chunk(n={0})".format(n))
        k, m = divmod(len(lst), n)
        return (lst[i * k + min(i, m):(i + 1) * k + min(i + 1, m)] for i in range(n))

    def swap(self, lst, i, j):
        """
        Swaps two items in the list.
        :param lst: The list.
        :param i: The index of the first item.
        :param j: The index of the second item.
        :return: Nothing.
        """
        # a swap equals 2 list assignments when there are no tuples and we need a help-variable to swap
        self.increase_call_counter("swap (~2 List item assignments)")
        lst[i], lst[j] = lst[j], lst[i]


class MergeSort(SortAlgorithm):
    """Provides an implementation for the Mergesort algorithm"""

    def __init__(self):
        super(MergeSort, self).__init__()
        self.name = "Mergesort"

    def merge(self, l_lst, r_lst):
        """
        Merges wo lists together into a sorted result list.
        :param l_lst: List 1 (left) to merge.
        :param r_lst: List 2 (right) to merge.
        :return: A sorted merged list from l_lst and r_lst.
        """
        result = []
        while len(l_lst) > 0 and len(r_lst) > 0:
            self.increase_call_counter("Element Compare")
            self.increase_call_counter("Get item from list", 2)
            if l_lst[0] <= r_lst[0]:
                self.increase_call_counter("Get item from list")
                item = l_lst.pop(0)
                self.increase_call_counter("Add item to result-list")
                result.append(item)
            else:
                self.increase_call_counter("Get item from list")
                item = r_lst.pop(0)
                self.increase_call_counter("Add item to result-list")
                result.append(item)
        self.increase_call_counter("Split list into list1 < PivotItem(s) < list2")
        self.increase_call_counter("Add multiple items to result-list")
        result.extend(l_lst)
        result.extend(r_lst)
        return result

    def sort(self, lst):
        """
        Sorts the list using the Mergesort algorithm.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        if len(lst) <= 1:
            return lst
        chunks = list(self.chunk(lst, 2))

        self.increase_call_counter("Recursive Sort-Call")
        l_lst = self.sort(chunks[0])
        self.increase_call_counter("Recursive Sort-Call")
        r_lst = self.sort(chunks[1])

        return self.merge(l_lst, r_lst)


class QuickSort(SortAlgorithm):
    """Provides an implementation for the Quicksort algorithm"""

    def __init__(self):
        super(QuickSort, self).__init__()
        self.name = "Quicksort"

    def sort(self, lst):
        """
        Sorts the list using the Quicksort algorithm.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """

        if len(lst) > 1:
            self.increase_call_counter("Get item from list")
            pivot = lst[0]
            ltp = []  # less than pivot item
            gtp = []  # greater than pivot item
            ep = []  # equals pivot item
            for item in lst:
                self.increase_call_counter("Get item from list")
                self.increase_call_counter("Element Compare")
                if item < pivot:
                    self.increase_call_counter("Add item to result-list")
                    ltp.append(item)
                elif item > pivot:
                    self.increase_call_counter("Add item to result-list")
                    gtp.append(item)
                else:
                    self.increase_call_counter("Add item to result-list")
                    ep.append(item)
            self.increase_call_counter("Split list into list1 < PivotItem(s) < list2")

            self.increase_call_counter("Recursive Sort-Call")
            ltp = self.sort(ltp)
            self.increase_call_counter("Recursive Sort-Call")
            gtp = self.sort(gtp)

            result = ltp
            self.increase_call_counter("Add multiple items to result-list")
            result.extend(ep)
            self.increase_call_counter("Add multiple items to result-list")
            result.extend(gtp)
            return result
        else:
            return lst


class GnomeSort(SortAlgorithm):
    """Provides an implementation for the Gnome sort algorithm"""

    def __init__(self):
        super(GnomeSort, self).__init__()
        self.name = "Gnome sort"

    def sort(self, lst):
        """
        Sorts the list using the Gnome sort algorithm.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        self.increase_call_counter("Copy the list")
        lst = list(lst)  # copy the list, because lists are mutable and passed by reference
        pos = 0
        while pos < len(lst):
            self.increase_call_counter("Get item from list", 2)
            self.increase_call_counter("Element Compare")
            if pos == 0 or lst[pos] >= lst[pos - 1]:
                pos += 1
            else:
                self.swap(lst, pos, pos - 1)
                pos -= 1
        return lst


class InsertionSort(SortAlgorithm):
    """Provides an implementation for the Insertion sort algorithm"""

    def __init__(self):
        super(InsertionSort, self).__init__()
        self.name = "Insertion sort"

    def sort(self, lst):
        """
        Sorts the list using the Insertion sort algorithm.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        self.increase_call_counter("Copy the list")
        lst = list(lst)  # copy the list, because lists are mutable and passed by reference
        for i in range(1, len(lst)):
            self.increase_call_counter("Get item from list")
            val = lst[i]
            j = i
            while j > 1 and lst[j - 1] > val:
                self.increase_call_counter("Get item from list")
                self.increase_call_counter("Element Compare")
                self.increase_call_counter("List item assignment")
                lst[j] = lst[j - 1]
                j = j - 1  # breaks the while loop
            lst[j] = val
            self.increase_call_counter("List item assignment")
        return lst


class PythonSort(SortAlgorithm):
    """Provides an implementation for a sort algorithm, using the python standard function."""

    def __init__(self):
        super(PythonSort, self).__init__()
        self.name = "Pythons standard list sort"

    def sort(self, lst):
        """
        Sorts the list using the standard python function.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        result = list(lst)  # copy the list, because lists are mutable and passed by reference
        result.sort()
        self.increase_call_counter("python_list.sort")
        return result
