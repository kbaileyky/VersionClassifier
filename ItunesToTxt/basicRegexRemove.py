import re
import sys

if len(sys.argv) != 3:
	print "Error: basicRegex <pattern> <infile>"
	print "length = " + str(len(sys.argv))
	sys.exit(2)

try:
	pat = re.compile(sys.argv[1])
except SyntaxError:
	print "Not a valid regex pattern"
	sys.exit(2)

try:
	f = open(sys.argv[2])
except IOError as e:
	print "Problem opening file {2}. Error ({0}) : {1}".format(e.errno, e.strerror, sys.argv[2])
	sys.exit(2)

lines = f.readlines()
f.close

count = 0
ret = ""
for l in lines:
	#print l
	#print pat.pattern
	m = pat.findall(l)
	if not m:
		ret = ret + l

print ret
