import re
import sys

if len(sys.argv) != 4:
	print "Error: basicRegex <patternMove> <patternAppendTo> <infile>"
	print "length = " + str(len(sys.argv))
	sys.exit(2)

try:
	patMove = re.compile(sys.argv[1])
	patAppend = re.compile(sys.argv[2])
except SyntaxError:
	print "Not a valid regex pattern"
	sys.exit(2)

try:
	f = open(sys.argv[3])
except IOError as e:
	print "Problem opening file {2}. Error ({0}) : {1}".format(e.errno, e.strerror, sys.argv[2])
	sys.exit(2)

lines = f.readlines()
f.close

count = 0
currentMatch = ""
for i in range(0,len(lines)):
	#print l
	#print patMove.pattern
	m = patMove.search(lines[i])
	if m:
		currentMatch = m.string
		lines[i] = ""
	else:
		m2 = patAppend.search(lines[i])
		if m2:
			lines[i] = lines[i] + currentMatch

for l in lines:
	print l
