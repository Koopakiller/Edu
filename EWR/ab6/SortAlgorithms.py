# Author: Tom Lambert
# Content: Implementation of sorting algorithms for EWR/ab6.

from abc import ABCMeta
from abc import abstractmethod

class SortAlgorithm(object):
    __metaclass__ = ABCMeta

    @abstractmethod
    def sort(self, lst):
        NotImplementedError()

    def chunk(lst, n):
        """Yield successive n-sized chunks from lst."""
        for i in range(0, len(lst), n):
            yield lst[i:i + n]


class MergeSort(SortAlgorithm):

    @staticmethod
    def merge(l_lst, r_lst):
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

    @staticmethod
    def merge_sort(self, lst):
        if len(lst) <= 1:
            return lst
        chunks = self.chunk(lst, 2)
        l_lst = self.sort(chunks[0])
        r_lst = self.sort(chunks[1])
        return self.merge(l_lst, r_lst)

    def sort(self, lst):
        self.merge_sort(lst)



# class QuickSort(SortAlgorithm):

# class GnomeSort(SortAlgorithm):

class PythonSort(SortAlgorithm):

    def sort(self, lst):
        result = list(lst)
        result.sort()
        return result
