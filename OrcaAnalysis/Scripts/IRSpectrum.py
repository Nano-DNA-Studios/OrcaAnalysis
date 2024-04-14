from code import interact
from socket import inet_aton
import matplotlib.pyplot as plt
import numpy as np
import os
import re
import sys

file = sys.argv[1]

fullPath = os.path.abspath(file)

if not os.path.exists(fullPath) and not os.path.isfile(fullPath):
    print("File does not exist, stopping Program")
    exit()

if not os.path.splitext(fullPath)[1] == '.out':
    print("File is not a .out file, stopping Program")
    exit()

fileName = os.path.basename(fullPath)

# Regex Pattern to extract the Frequencies and Intensities
pattern = r'\s*\d+:\s+(\d+\.\d+)\s+\S+\s+(\d+\.\d+)'

frequencies = []
intensities = []

with open(file, 'r') as f:
    lines = f.readlines()
    for line in lines:
        match = re.search(pattern, line)

        if match:
            # Extracted frequency and intensity
            freq = float(match.group(1))
            intensity = float(match.group(2))

            # Append to lists
            frequencies.append(freq)
            intensities.append(intensity)

#Normalize Intensities
frequencies = np.array(frequencies)
intensities = np.array(intensities)
intensities = intensities / np.max(intensities) * 100

# Reverse the Intensities
reverseIntensities = 100 - intensities

#Plot the Graph and Show
plt.plot(frequencies, reverseIntensities, 'o')
plt.xlabel('Frequency')
plt.ylabel('Intensity')
plt.title(fileName.split(".")[0] + "  (" + fullPath + ")")
plt.show()