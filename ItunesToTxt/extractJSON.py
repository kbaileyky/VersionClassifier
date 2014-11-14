import re
import json
import sys


if len(sys.argv) != 2:
        print "Error: basicRegex <infile>"
        print "length = " + str(len(sys.argv))
        sys.exit(2)


try:
        f = open(sys.argv[1])
except IOError as e:
        print "Problem opening file {2}. Error ({0}) : {1}".format(e.errno, e.strerror, sys.argv[1])
        sys.exit(2)

text = f.read()
f.close

pat = re.compile('{.*}')
ls = pat.findall(text)

#get name of the new file
patName = re.compile("\"nameRaw\":\".*?\"")
name = re.search(patName, ls[1])
namestr =  name.group()
namestr = namestr.replace(" ", "")
namestr = namestr[11:-1]

with open("./RawData/" + namestr+".json", 'w') as outfile:
	outfile.write(json.dumps(json.loads(ls[1]), indent =4))
	outfile.close()	
