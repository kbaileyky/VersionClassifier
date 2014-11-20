import sys
import random
import math

#We want the same sample everytime right now!!!
random.seed(333)

if len(sys.argv) != 5:
        print "Error: generateArray <Max> <percent> <infile> <Outfile>"
        print "length = " + str(len(sys.argv))
        sys.exit(2)

max = float(sys.argv[1])
percent = float(sys.argv[2])

numberSample = max * (percent/100.0)
numberSample = int(math.ceil(numberSample))

print numberSample

try:
        f = open(sys.argv[3])
except IOError as e:
        print "Problem opening file {2}. Error ({0}) : {1}".format(e.errno, e.strerror, sys.argv[3])
        sys.exit(2)

lines = f.readlines()
f.close

samp = random.sample(lines, numberSample)

print type(samp)

file = open(sys.argv[4], 'w+')
for s in samp:
	file.write(s)

file.close()
