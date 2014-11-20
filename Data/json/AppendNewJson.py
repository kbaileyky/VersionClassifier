import sys

if len(sys.argv) != 4:
        print "Error: basicRegex <original file> <newfile> <outfile>"
        print "length = " + str(len(sys.argv))
        sys.exit(2)

try:
        f = open(sys.argv[1])
	f2 = open(sys.argv[2])
except IOError as e:
        print "Problem opening file {2} or {3}. Error ({0}) : {1}".format(e.errno, e.strerror, sys.argv[1], sys.argv[2])
        sys.exit(2)

alreadyClass = f.readlines()
newStuff = f2.readlines()
f.close()
f2.close()

count = len(alreadyClass)

for i in range(count, len(newStuff)):
	alreadyClass.append(newStuff[i])

file = open(sys.argv[3], 'w+')
for l in alreadyClass:
	file.write(l)

file.close()
