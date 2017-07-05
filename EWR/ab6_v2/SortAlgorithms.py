# Author: Tom Lambert
# Content: Implementation of sorting algorithms for EWR/ab6.


def quick_sort(self, lst):
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


def gnome_sort(self, lst):
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
            self.increase_call_counter("swap (~2 List item assignments)")
            lst[pos], lst[pos - 1] = lst[pos - 1], lst[pos]
            pos -= 1
    return lst


def insertion_sort(self, lst):
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
        while j > 0 and lst[j - 1] > val:
            self.increase_call_counter("Get item from list")
            self.increase_call_counter("Element Compare")
            self.increase_call_counter("List item assignment")
            lst[j] = lst[j - 1]
            j = j - 1  # breaks the while loop
        lst[j] = val
        self.increase_call_counter("List item assignment")
    return lst


def python_sort(self, lst):
    """
    Sorts the list using the standard python function.
    :param lst: The list to sort.
    :return: A sorted list with the items from lst.
    """
    result = list(lst)  # copy the list, because lists are mutable and passed by reference
    result.sort()
    self.increase_call_counter("python_list.sort")
    return result
