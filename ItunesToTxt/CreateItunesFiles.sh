#!/bin/bash

python ./basicRegexNoTotal.py 'id\d+' ./listofIDs.txt  > justIds.txt

while read line
do
        idnum=$line
        echo "https://itunes.apple.com/us/app/${idnum}?mt=8"
        str=$(curl -H 'Host: itunes.apple.com' -H 'Accept-Language: en-us, en;q=0.50' -H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' -H 'X-Apple-Store-Front: 143441-1,28' -H 'X-Apple-Tz: -25200' -A 'iTunes/12.0.1 (Macintosh; OS X 10.10) AppleWebKit/0600.1.25' "https://itunes.apple.com/us/app/${idnum}?mt=8")
        echo "$str" > "./htmlFiles/${idnum}html.txt"

done < justIds.txt

FILES=./htmlFiles/*

for f in $FILES
do 
	name=$(basename $f)
	shortname=${name%.*}
	shortname="./RawData/"$shortname".json"
	echo "Processing ${shortname} Json file...."	
	python extractJSON.py $f "./RawData/"
done

FILESRAW=./RawData/*

for f in $FILESRAW
do 
	name=$(basename $f)
	shortname=${name%.*}
	shortname="./CleanData/"$shortname"Clean"
	echo "Processing ${shortname} clean file...."	
	python versionFromJson.python $f $shortname
done

echo "Done writing files"

