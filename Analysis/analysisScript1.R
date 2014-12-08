#install.packages("rjson")
#source("C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Analysis\\analysisScript1.R")
library("rjson")


appendStr  <- function(str1, str2){
	ret = paste(str1, str2, sep = "~>~>~")
	ret = gsub("~>~>~", "", ret)
	print(ret)
	return (ret)
}

getFiledata <- function(ending){
	json_file = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJsonClassified\\Summary"
 	str = appendStr(json_file, ending)
	json_data = fromJSON(file=str)
	return (json_data)
}

generateBoxPlot <- function(title, mob, desk, sib, sibM, sibD, xb, yb, path, file){
	seriesLabels = c("Mobile", "Desktop", "Sibling", "Sibling - Mobile", "Sibling - Desktop")
	
	boxplot(mob, desk, sib, sibM, sibD, names=seriesLabels, main=title, xlab=xb, ylab=yb)




	str =  appendStr(path, file)
	dev.copy(pdf,str)
	dev.off()
#	jpeg(file=str)
#	boxplot(write)
#	dev.off()
}


makeTable <- function(mob, desk, sib, sibM, sibD){
	x1 <- mob
	x2 <- desk
	boxplot(x1,x2, names=c("Mobile", "Desktop"))
#	jpeg(file=str)
#	boxplot(write)
#	dev.off()
}

mobileJson = getFiledata("Mobile.json")
mobileBugs = mobileJson$Bugs$Data
mobileFeatures = mobileJson$Features$Data
mobileEnhancements = mobileJson$Enhacements$Data
mobileNonFunc = mobileJson$NonFunc$Data

desktopJson = getFiledata("Desktop.json")
desktopBugs = desktopJson$Bugs$Data
desktopFeatures = desktopJson$Features$Data
desktopEnhancements = desktopJson$Enhacements$Data
desktopNonFunc = desktopJson$NonFunc$Data

siblingJson = getFiledata("Sibling.json")
siblingBugs = siblingJson$Bugs$Data
siblingFeatures = siblingJson$Features$Data
siblingEnhancements = siblingJson$Enhacements$Data
siblingNonFunc = siblingJson$NonFunc$Data

sibMobJson = getFiledata("SibMob.json")
sibMobBugs = sibMobJson$Bugs$Data
sibMobFeatures = sibMobJson$Features$Data
sibMobEnhancements = sibMobJson$Enhacements$Data
sibMobNonFunc = sibMobJson$NonFunc$Data

sibDeskJson = getFiledata("SibDesk.json")
sibDeskBugs = sibDeskJson$Bugs$Data
sibDeskFeatures = sibDeskJson$Features$Data
sibDeskEnhancements = sibDeskJson$Enhacements$Data
sibDeskNonFunc = sibDeskJson$NonFunc$Data

runReleaseAnalysis <- function(){
	graphDirectory_file = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Analysis"


	generateBoxPlot("Bugs per Release", mobileBugs, desktopBugs, siblingBugs, sibMobBugs, sibDeskBugs, "Application Type", "Number Bugs", graphDirectory_file, "\\BugBoxPlot.pdf")

	generateBoxPlot("Features per Release", mobileFeatures, desktopFeatures, siblingFeatures, sibMobFeatures, sibDeskFeatures, "Application Type", "Number Features", graphDirectory_file, "\\FeatureBoxPlot.pdf")
	generateBoxPlot("Enhancements per Release", mobileEnhancements, desktopEnhancements, siblingEnhancements, sibMobEnhancements, sibDeskEnhancements, "Application Type", "Number Enhancements", graphDirectory_file, "\\EnhancementBoxPlot.pdf")
	generateBoxPlot("Non-Functional per Release", mobileNonFunc, desktopNonFunc, siblingNonFunc, sibMobNonFunc, sibDeskNonFunc, "Application Type", "Number Non-Functional", graphDirectory_file, "\\NonFuncBoxPlot.pdf")


	print(t.test(mobileBugs, desktopBugs, alternative="two.sided", var.equal=FALSE))
	print(t.test(mobileBugs, siblingBugs, alternative="two.sided", var.equal=FALSE))
	print(t.test(desktopBugs, siblingBugs, alternative="two.sided", var.equal=FALSE))
}



