import sys
import re
import json

def CompJson(json1, json2):
	entrylist1 = json1["EntryList"]
	entrylist2 = json2["EntryList"]

	if len(entrylist1) != len(entrylist2):
        	print "Files not the same length: {0} vs {1}".format(len(entrylist1), len(entrylist2))
        	sys.exit(2)

	difflist = []
	for i in range(0, len(entrylist1)):
        	if entrylist1[i]["classification"]["int_type"] != entrylist2[i]["classification"]["int_type"]:
                	difflist[len(difflist):] =  [entrylist1[i], entrylist2[i]]
		#	print difflist
	return difflist


if len(sys.argv) != 4:
        print "Error: basicRegex <infile1> <infile2> <outfile>"
        print "length = " + str(len(sys.argv))
        sys.exit(2)


try:
        f = open(sys.argv[1])
except IOError as e:
        print "Problem opening file {2}. Error ({0}) : {1}".format(e.errno, e.strerror, sys.argv[1])
        sys.exit(2)

text1 = f.readlines()
f.close


try:
        f = open(sys.argv[2])
except IOError as e:
        print "Problem opening file {2}. Error ({0}) : {1}".format(e.errno, e.strerror, sys.argv[2])
        sys.exit(2)

text2 = f.readlines()
f.close

if len(text1) != len(text2):
        print "Files not the same length: {0} vs {1}".format(len(text1), len(text2))
        sys.exit(2)

difflist = []
numEntries = 0
for ele in range(0, len(text1)):

	json1 = json.loads(text1[ele])
	json2 = json.loads(text2[ele])

	if len(json1) != len(json2):
        	print "Files not the same length: {0} vs {1}".format(len(entrylist1), len(entrylist2))
        	sys.exit(2)
	
	numEntries = numEntries + len(json1["EntryList"])
	ls = CompJson(json1, json2)
	if len(ls) > 0:
		difflist.append( ls)


print difflist

percent = ((numEntries - len(difflist))/numEntries) * 100
print "Number different - {0}	Total Classified - {1}	Percent Agreement - {2}%".format(len(difflist), numEntries, percent)	

with open(sys.argv[3], 'w') as outfile:
	for e in difflist:
		outfile.write("Number different - {0}\tTotal Classified - {1}\tPercent Agreement - {2}%".format(len(difflist), numEntries, percent))
		outfile.write("\n")
		outfile.write(sys.argv[1])
		outfile.write("\n")
        	outfile.write(json.dumps(e[0], indent=4))
		outfile.write("\n")
		outfile.write(sys.argv[2])
		outfile.write("\n")
        	outfile.write(json.dumps(e[1], indent=4))
		outfile.write("\n")
        outfile.close()


