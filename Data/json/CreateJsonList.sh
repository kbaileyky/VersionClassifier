#!/bin/bash

rm ./MasterJsonFile2.txt
echo "Old file removed"

cat ./CompJson/* >> MasterJsonFile2.txt
echo "Master File Created"

numLines=$(cat MasterJsonFile2.txt | wc -l)
echo "Total Lines "$numLines


