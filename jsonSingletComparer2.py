import sys
import re
import json

def AddToDictionary(dict, entry):
	str = MakeKey(entry)
	if not dict.has_key(str):
		dict[str] = len(dict)

def MakeKey(entry):
	print entry
	entryjson = json.loads(entry)
	print json.dumps(entryjson, indent = 2)
	str = entryjson["ApplicationName"] + entryjson["VersionNumber"] + entryjson["original_text"]
	return str

def DifferentBasic(entry1, entry2):
	json1 = json.loads(entry1)
	json2 = json.loads(entry2)

	#print json1["classification"]["int_type"], json2["classification"]["int_type"] 

	if json1["classification"]["int_type"] != json2["classification"]["int_type"]:
		return True
	else:
		return False

def HandleSplit(ls, difflist, splitlist):
	startSplit = 0
	firstFile = ls[0][1]
	while ls[startSplit][1] == firstFile:
		startSplit = startSplit + 1
	i = 0
	j = startSplit
	while i != startSplit and j != len(ls):
		json1 = json.loads(ls[i][0])
		json2 = json.loads(ls[j][0])

		if json1["entry"] == json2["entry"]:
			if DifferentBasic(ls[i][0], ls[j][0]):
				difflist.append([[ls[i][0], ls[i][1]], [ls[j][0],ls[j][1]]])
			print ls
			ls.pop(j)
			ls.pop(i)
			print ls
		else:
			splitlist.append(ls)
			print splitlist
			return
	

def CompJson(json1, json2, filename1, filename2):
	indexlist = CreateIndexList(json1, json2, filename1, filename2)
	
	difflist = []
	splitlist = []

	for e in indexlist:
		if len(e) == 2:
			if DifferentBasic(e[0][0], e[1][0]):
				difflist.append([e[0], e[1]])
	#	elif len(e) > 2:
	#		HandleSplit(e, difflist, splitlist)	
		else:
			for sube in e:
				splitlist.append(sube)
	return [difflist, splitlist]
	

def Print_Diff_List(difflist):
	for e in difflist:
		for sube in e:
			print sube
			#print sube[1]
			print ""
			#print json.dumps(json.loads(sube[0]), indent = 4)
			print ""

def Print_Basic_List(difflist):
	for sube in difflist:
		print sube
		#print sube[1]
		print ""
		#print json.dumps(json.loads(sube[0]), indent = 4)
		print ""


def CreateIndexList(json1, json2, filename1, filename2):
	entrylist1 = json1
	entrylist2 = json2


	difflist = []
	indexlist = [[]]
	j = 0
	dict = {"test" : 1}
	for e in entrylist1:
		AddToDictionary(dict,e)
	for e in entrylist2:
		AddToDictionary(dict,e)
	dict.pop("test", None)	
	for i in range(0, len(dict)):
		indexlist.append([])
	
	print filename1
	for e in entrylist1:
		indexlist[dict[MakeKey(e)]].append([e,filename1] )
	print filename2
	for e in entrylist2:
		indexlist[dict[MakeKey(e)]].append([e, filename2])
			
	return indexlist

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


temp = CompJson(text1, text2, sys.argv[1], sys.argv[2])
difflist = temp[0]
splitlist = temp[1]

#Print_Diff_List(difflist)

#print "Splits"

#Print_Basic_List(splitlist)
numEntries = max(len(text1), len(text2))
percent = (float(numEntries)- float(len(difflist))) / float(numEntries) * 100.0


with open(sys.argv[3], 'w') as outfile:
	outfile.write("Number different - {0}\tTotal Classified - {1}\tPercent Agreement - {2}%\tNumberSplit - {3}".format(len(difflist), numEntries, percent, len(splitlist)))	
	for e in difflist:
		for sube in e:
			outfile.write("\n")
			outfile.write(sube[1])
			#print sube[1]
			
			outfile.write(json.dumps(json.loads(sube[0]), indent = 4))
			outfile.write("\n")

	outfile.write("-----------SPLIT JSON ----------")
	for sube in splitlist:
		outfile.write("\n")
		outfile.write(sube[1])
		#print sube[1]
			
		outfile.write(json.dumps(json.loads(sube[0]), indent = 4))
		outfile.write("\n")

        outfile.close()


