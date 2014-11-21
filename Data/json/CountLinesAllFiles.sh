#!/bin/bash

FILES=./CompJson/*D

numLines=0
for f in $FILES
do 
        #name=$(basename $f)
	name=$f
	echo $name
        shortname=${name%.*}
	thisFile=$(cat $name | wc -l)
	numLines=$(($thisFile+$numLines))
        echo "${name}:       "$(cat $name | wc -l)
done
echo "Total Lines Desktop: "$numLines


FILES=./CompJson/*M
numLines=0
for f in $FILES
do 
        #name=$(basename $f)
	name=$f
        shortname=${name%.*}
	thisFile=$(cat $name | wc -l)
	numLines=$(($thisFile+$numLines))
        echo "${name}:       "$(cat $name | wc -l)
done
echo "Total Lines Mobile: "$numLines

FILES=./CompJson/*S
numLines=0
for f in $FILES
do 
        #name=$(basename $f)
	name=$f
        shortname=${name%.*}
	thisFile=$(cat $name | wc -l)
	numLines=$(($thisFile+$numLines))
        echo "${name}:       "$(cat $name | wc -l)
done
echo "Total Lines Sibling: "$numLines

FILES=./CompJson/*
numLines=0
for f in $FILES
do 
        #name=$(basename $f)
	name=$f
        shortname=${name%.*}
	thisFile=$(cat $name | wc -l)
	numLines=$(($thisFile+$numLines))
done
echo "Total Lines: "$numLines