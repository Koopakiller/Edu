# coding=utf-8
# Author: Tom Lambert
# Content: Implementierung der Sort-Klasse für ab6.


class Sort(object):
    """Implementiert Sortier-Algorithmen mit der Möglichkeit einer statistischen Auswertung"""

    def __init__(self):
        self.counter_swap = 0  # ~2 List item assignments
        self.counter_list_item_assignment = 0
        self.counter_item_compare = 0
        self.counter_get_item_from_list = 0
        self.counter_add_item_to_result_list = 0
        self.counter_recursive_call = 0
        self.counter_split_list = 0
        self.counter_copy_list = 0
        self.counter_sort_call = 0

    def quick_sort(self, lst):
        """
         Sortiert die lst-Liste mit dem Quick-Sort-Algorithmus und gibt die sortierte Liste zurück.
         Bestimmte Operationen werden in den counter_-Variablen gezählt.
        """
        self.counter_sort_call += 1
        if len(lst) > 1:
            self.counter_get_item_from_list += 1
            pivot = lst[0]
            ltp = []  # less than pivot item
            gtp = []  # greater than pivot item
            ep = []  # equals pivot item
            for item in lst:
                self.counter_get_item_from_list += 1
                self.counter_item_compare += 1
                if item < pivot:
                    self.counter_add_item_to_result_list += 1
                    ltp.append(item)
                elif item > pivot:
                    self.counter_add_item_to_result_list += 1
                    gtp.append(item)
                else:
                    self.counter_add_item_to_result_list += 1
                    ep.append(item)
            self.counter_split_list += 1
    
            self.counter_recursive_call += 1
            ltp = self.quick_sort(ltp)
            self.counter_recursive_call += 1
            gtp = self.quick_sort(gtp)
    
            result = ltp
            self.counter_add_item_to_result_list += len(ep)
            result.extend(ep)
            self.counter_add_item_to_result_list += len(gtp)
            result.extend(gtp)
            return result
        else:
            return lst
        
    def gnome_sort(self, lst):
        """
         Sortiert die lst-Liste mit dem Gnome-Sort-Algorithmus und gibt die sortierte Liste zurück.
         Bestimmte Operationen werden in den counter_-Variablen gezählt.
        """
        self.counter_sort_call += 1
        self.counter_copy_list += 1
        lst = list(lst)  # copy the list, because lists are mutable and passed by reference
        pos = 0
        while pos < len(lst):
            self.counter_get_item_from_list += 2
            self.counter_item_compare += 1
            if pos == 0 or lst[pos] >= lst[pos - 1]:
                pos += 1
            else:
                self.counter_swap += 1
                lst[pos], lst[pos - 1] = lst[pos - 1], lst[pos]
                pos -= 1
        return lst
        
    def insertion_sort(self, lst):
        """
         Sortiert die lst-Liste mit dem Insertion-Sort-Algorithmus und gibt die sortierte Liste zurück.
         Bestimmte Operationen werden in den counter_-Variablen gezählt.
        """
        self.counter_sort_call += 1
        self.counter_copy_list += 1
        lst = list(lst)  # copy the list, because lists are mutable and passed by reference
        for i in range(1, len(lst)):
            self.counter_get_item_from_list += 1
            val = lst[i]
            j = i
            while j > 0 and lst[j - 1] > val:
                self.counter_item_compare += 1
                self.counter_get_item_from_list += 1
                self.counter_list_item_assignment += 1
                lst[j] = lst[j - 1]
                j = j - 1  # breaks the while loop
            lst[j] = val
            self.counter_list_item_assignment += 1
        return lst
