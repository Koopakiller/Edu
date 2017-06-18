
maxExponent = 6
maxNumber = 8

for p in range(1, maxExponent + 1):
    format = "{:>" + str(max(len(str((maxNumber)**(p))) + 1, 4)) + "}"
    if p == 1:
        print (format.format("i"), end="")
    else:
        print (format.format("i^" + str(p)), end="")
print ("")
    
for i in range(1, maxNumber + 1):
    for p in range(1, maxExponent + 1):
        format = "{:>" + str(max(len(str((maxNumber)**(p))) + 1, 4)) + "}"
        print (format.format(i**p), end="")
    print ("")