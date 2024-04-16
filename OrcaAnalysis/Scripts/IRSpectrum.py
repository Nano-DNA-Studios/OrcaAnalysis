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


sorted_pairs = sorted(zip(frequencies, reverseIntensities))

# Unpacking the sorted pairs
sorted_freq, sorted_intensity = zip(*sorted_pairs)

# Convert tuples back to lists (if necessary)
sorted_freq = list(sorted_freq)
sorted_intensity = list(sorted_intensity)

#Round the Values for less Decimal points
sorted_freq = [round(num, 2) for num in sorted_freq]
sorted_intensity = [round(num, 2) for num in sorted_intensity]

#Find the Peaks (Mins in this case)
peaks = []
for i in range(1, len(sorted_intensity)-1):
    if sorted_intensity[i-1] > sorted_intensity[i] < sorted_intensity[i+1] and sorted_intensity[i] < 90:
        peaks.append(i)


#Plot the Graph and Show
plt.plot(sorted_freq, sorted_intensity)
plt.xlabel('Frequency')
plt.ylabel('Intensity')
plt.title(fileName.split(".")[0] + "  (" + fullPath + ")")

# Annotate peaks
for peak in peaks:
    plt.annotate(f'({sorted_freq[peak]}, {sorted_intensity[peak]})', # text to display
                 (sorted_freq[peak], sorted_intensity[peak]),        # point to annotate
                 textcoords="offset points", # how to position the text
                 xytext=(45,0),             # distance from text to points (x,y)
                 ha='center')               # horizontal alignment can


plt.show()