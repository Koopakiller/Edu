import matplotlib
matplotlib.use("TkAgg")
import numpy as np
import matplotlib.pyplot as plt
import math

x = np.linspace(0, 4*math.pi, 101)
y1 = np.exp(-x)
y2 = np.sin(x)
plt.plot(x, y1, color="green")
plt.plot(x, y2, color="red")
plt.xlabel("x"); plt.ylabel("y")
plt.show()