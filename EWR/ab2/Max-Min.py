# coding=utf-8
# Author: Tom Lambert, Phillip Kries

print("Bitte geben Sie eine Liste von Komma-Getrennten Zahlen an:")
s = raw_input()

nums = s.split(",")
nums2 = []
for num in nums:
    try:
        nums2.append(int(num))
    except ValueError:
        print("Der Wert '" + num + "' ist keine Zahl und wird ignoriert")

print
print("Sie haben " + str(len(nums2)) + " Zahlen eingegeben")
print("Das Maximum ist " + str(max(nums2)))
print("Das Minimum ist " + str(min(nums2)))
