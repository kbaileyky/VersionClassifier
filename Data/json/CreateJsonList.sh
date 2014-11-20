#!/bin/bash

rm ./MasterJsonFile.txt
echo "Old file removed"

cat ./CompJson/* >> MasterJsonFile.txt
echo "Master File Created"

numLines=$(cat MasterJsonFile.txt | wc -l)
echo "Total Lines "$numLines


