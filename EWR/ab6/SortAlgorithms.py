# Author: Tom Lambert
# Content: Implementation of sorting algorithms for EWR/ab6.

from abc import ABCMeta
from abc import abstractmethod

class SortAlgorithm(object):
    __metaclass__ = ABCMeta

    @abstractmethod
    def sort(self, lst):
        """
        If implemented, sorts the list.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        NotImplementedError()

    def chunk(self, lst, n):
        """
        Splits a list in n-sized chunks.
        :param lst: The list to split.
        :param n: The number of entry in 1 chunk.
        :return: A list of chunks with items from lst.
        """
        for i in range(0, len(lst), n):
            yield lst[i:i + n]


class MergeSort(SortAlgorithm):
    """Provides an implementation for the merge sort algorithm"""

    @staticmethod
    def merge(l_lst, r_lst):
        """
        Merges wo lists together into a sorted result list.
        :param l_lst: List 1 (left) to merge.
        :param r_lst: List 2 (right) to merge.
        :return: A sorted merged list from l_lst and r_lst.
        """
        result = []
        while len(l_lst) > 0 and len(r_lst) > 0:
            if l_lst[0] <= r_lst[0]:
                item = l_lst.pop(0)
                result.append(item)
            else:
                item = r_lst.pop(0)
                result.append(item)
        result.extend(l_lst)
        result.extend(r_lst)
        return result

    def sort(self, lst):
        """
        Sorts the list using the merge sort algorithm.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        if len(lst) <= 1:
            return lst
        chunks = self.chunk(lst, 2)
        l_lst = self.sort(chunks[0])
        r_lst = self.sort(chunks[1])
        return self.merge(l_lst, r_lst)



# class QuickSort(SortAlgorithm):

# class GnomeSort(SortAlgorithm):


class PythonSort(SortAlgorithm):
    """Provides an implementation for a sort algorithm, using the python standard function."""

    def sort(self, lst):
        """
        Sorts the list using the standard python function.
        :param lst: The list to sort.
        :return: A sorted list with the items from lst.
        """
        result = list(lst)  # copy the list
        result.sort()
        return result
