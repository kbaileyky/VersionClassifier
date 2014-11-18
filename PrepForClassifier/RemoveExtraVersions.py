import re
import sys

if len(sys.argv) != 5:
        print "Error: basicRegex <start version pattern> <infile> <pattern2> <replace with>"
        print "length = " + str(len(sys.argv))
        sys.exit(2)

try:
        pat = re.compile(sys.argv[1])
	pat2 = re.compile(sys.argv[3])
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
str = ""
for l in lines:
	#print l
	#print pat.pattern
	#print pat2.pattern
	m = pat.findall(l)
	if not m:
	#	l = re.sub(pat2, sys.argv[4], l)
		count += 1
	else:
		print l
		l = "*******" + l
		print l
		print "\n"
	str = str + l
	


print "Number replaced:  {0}".format(count)
#print str
