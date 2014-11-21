import sys
import re
import json

def SplitCheck(ls1, ls2, i, j):
	retls =[[]]
	print i,j
	entry1 = json.loads(entrylist1[i])		
	entry2 = json.loads(entrylist2[i])		
	if entry1["entry"] in entry2["entry"]:
		retls=[[],[ls2[j]]]
		while entry1["original_text"].equals(entry2["original_text"]):
			
			retls[0][len(retls):] = ls1[i]
			i = i+1
	else:
		retls=[[ls1[i]],[]]
                while ls1[i]["original_text"].equals(ls2[j]["original_text"]):
                        retls[0][len(retls):] = ls2[j]
                        j = j+1
	print i, j
	print retls
	return [retls, i, j]

def CompJson(json1, json2):
	entrylist1 = json1
	entrylist2 = json2


	difflist = []
	splitlist = []
	j = 0
	for i in range(0, len(entrylist1)):
		entry1 = json.loads(entrylist1[i])		
		entry2 = json.loads(entrylist2[i])		

	
		if entry1["entry"].equals(entry2["entry"]):
	        	if entry1["classification"]["int_type"] != entry2["classification"]["int_type"]:
        	        	difflist[len(difflist):] =  [entrylist1[i], entrylist2[j]]
			#	print difflist
		else:
			#templist = SplitCheck(entrylist1, entrylist2, i, j)
        	        if entry1["entry"] in entry2["entry"]:
				splitlist.append(
			#splitlist[len(splitlist):] =  templist[0]
			#i = templist[1]
			#j = templist[2]	
			 
		j = j+ 1
		
	return [difflist, splitlist]


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

difflist = []
splitlist =[]
json1 =[]
json2 = []
numEntries = 0

for l in text1:
	json1.append(l)
	#print json.dumps(json.loads(l)["entry"])
	#print json1[len(json1)-1]
	print json.dumps(json.loads(json1[len(json1)-1]))

for l in text2:
	json2.append(l)
	json2[len(json2):] = json.loads(l)

ls = CompJson(json1, json2)
difflist = ls[0]
splitlist = ls[1]

print difflist

print "Splits"

print splitlist


with open(sys.argv[3], 'w') as outfile:
	for e in difflist:
#		outfile.write("Number different - {0}\tTotal Classified - {1}\tPercent Agreement - {2}%".format(len(difflist), numEntries, percent))
		outfile.write("\n")
		outfile.write(sys.argv[1])
		outfile.write("\n")
        	outfile.write(json.dumps(e[0], indent=4))
		outfile.write("\n")
		outfile.write(sys.argv[2])
		outfile.write("\n")
        	outfile.write(json.dumps(e[1], indent=4))
		outfile.write("\n")
	
	outfile.write("-----------SPLIT JSON ----------")
	for e in splitlist:
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


