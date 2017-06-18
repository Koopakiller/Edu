from __future__ import print_function

lists = [
    [1, 2, 3],
    [4, 5, 6],
    range(0, 15),
    range(0, 15, 4),
    xrange(0, 15, 4)                # Nur in Python 2, in Python 3 verhält sich range wie xrange
    ]
    
lists.append(lists[0] + lists[1])   # Listen verketten
lists.append(lists[0] * 3)          # 3mal hinter eiannder hängen
lists.append(3 * lists[0])          # ebenfalls 3mal hinter eiannder hängen

for list in lists:
    print (list)
    for i in list:
        print (" " + str(i), end="")
    print ()