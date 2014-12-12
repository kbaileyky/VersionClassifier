#install.packages("rjson")
#source("C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Analysis\\analysisScript1.R")
library("rjson")
library("xtable")

appendStr  <- function(str1, str2){
	ret = paste(str1, str2, sep = "~>~>~")
	ret = gsub("~>~>~", "", ret)
	print(ret)
	return (ret)
}

getFiledata <- function(ending){
	#json_file = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJsonClassified\\Summary"
	json_file = "Research_1/VersionClassifier/Data/json/VersionJsonClassified/Summary"
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


	print(wilcox.test(mobileBugs, desktopBugs))
	print(wilcox.test(mobileBugs, siblingBugs))
	print(wilcox.test(desktopBugs, siblingBugs, alternative="two.sided", var.equal=FALSE))
}

runAllWilcoxTests <-function(){
		filename = "/Users/kendallbailey/Research_1/VersionClassifier/Paper/tables.tex"
	
	
	runWilcoxTests(mobileBugs, desktopBugs, siblingBugs, "Bugs", FALSE, filename)
	runWilcoxTests(mobileFeatures, desktopFeatures, siblingFeatures, "Features", TRUE, filename)
	
	runWilcoxTests(mobileEnhancements, desktopEnhancements, siblingEnhancements, "Enhancements", TRUE, filename)
	
	runWilcoxTests(mobileNonFunc, desktopNonFunc, siblingNonFunc, "Non-Functional", TRUE, filename)
	
	runWilcoxTests(mobileCyc, desktopCyc, siblingCyc, "Cycle Length", TRUE, filename)
	
	
}

runWilcoxTests<- function(mobile, desktop, siblings, title, apnd, filename){
	
	length = min(length(mobile), length(desktop), length(siblings))
	
	print(length)
	
	mobSam = sample(mobile, length)
	deskSam = sample(desktop, length)
	sibSam = sample(siblings, length)
	
	mdP = wilcox.test(mobSam,deskSam)$p.value
	msP = wilcox.test(mobSam,sibSam)$p.value
	dsP = wilcox.test(sibSam,deskSam)$p.value
	heads = c("Mobile vs Desktop", "Mobile vs Sibling", "Sibling vs Desktop")
	a = c(mdP,msP,dsP)
	result = table(a)
	#cat("1,2 ",mdP, "\n")
#	cat("1,3 ", msP, "\n")
#	cat("2,3 ", dsP, "\n")

	result = data.frame(heads,a)
	colnames(result) <- c("Application Comparison", "p-value")
	
	print(xtable(result, digits=-2, caption = title))

	print(xtable(result, digits=-2,caption = title ), file=filename, append=apnd, include.rownames=FALSE, caption.placement= 'top')
	}
	
	

