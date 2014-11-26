#!/bin/bash

file=./MasterJsonFile3k.txt

rm $file
echo "Old file removed"

cat ./CompJson/* >> $file
echo "Master File Created"

numLines=$(cat $file | wc -l)
echo "Total Lines "$numLines


