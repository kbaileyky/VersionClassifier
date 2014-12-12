#source("C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Analysis\\releaseCycleAnalysisScript.R")
library("rjson")
source("C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Analysis\\analysisScript1.R")
#source(file="Research_1/VersionClassifier/Analysis/analysisScript1.R")


mobileJsonCyc = getFiledata("CyclesMobile.json")
desktopJsonCyc = getFiledata("CyclesDesktop.json")
siblingJsonCyc = getFiledata("CyclesSibling.json")
sibMobJsonCyc = getFiledata("CyclesSibMob.json")
sibDeskJsonCyc = getFiledata("CyclesSibDesk.json")

mobileCyc = mobileJsonCyc$Cycle$Data
desktopCyc = desktopJsonCyc$Cycle$Data
siblingCyc = siblingJsonCyc$Cycle$Data
sibMobCyc = sibMobJsonCyc$Cycle$Data
sibDesktCyc = sibDeskJsonCyc$Cycle$Data

RunAnalysis <- function(){
	print(t.test(mobileCyc , desktopCyc , alternative="two.sided", var.equal=FALSE))



}