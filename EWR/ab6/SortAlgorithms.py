

from abc import ABCMeta
from abc import abstractmethod

class SortAlgorithm(object):
    __metaclass__ = ABCMeta

    @abstractmethod
    def sort(self, lst):
        NotImplementedError()

class MergeSort(SortAlgorithm):

    def sort(self, lst):


# class QuickSort(SortAlgorithm):

# class GnomeSort(SortAlgorithm):

class PythonSort(SortAlgorithm):

    def sort(self, lst):
        result = list(lst)
        result.sort()
        return result
