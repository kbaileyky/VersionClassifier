#install.packages("rjson")
#source("C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Analysis\\analysisScript1.R")
library("rjson")
library("xtable")
library("texreg")

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
	seriesLabels = c("Mobile", "Desktop", "Sibling")
	
	boxplot(mob, desk, sib, names=seriesLabels, main=title, xlab=xb, ylab=yb)




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
mobileCycles = mobileJson$Cycle$Data

length(mobileCycles)

desktopJson = getFiledata("Desktop.json")
desktopBugs = desktopJson$Bugs$Data
desktopFeatures = desktopJson$Features$Data
desktopEnhancements = desktopJson$Enhacements$Data
desktopNonFunc = desktopJson$NonFunc$Data
desktopCycles = desktopJson$Cycle$Data

siblingJson = getFiledata("Sibling.json")
siblingBugs = siblingJson$Bugs$Data
siblingFeatures = siblingJson$Features$Data
siblingEnhancements = siblingJson$Enhacements$Data
siblingNonFunc = siblingJson$NonFunc$Data
siblingCycles = siblingJson$Cycle$Data

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

	generateBoxPlot("Cycle Length", mobileCyc, desktopCyc, siblingCyc, sibMobNonFunc, sibDeskNonFunc, "Application Type", "Cycle Length (Days)", graphDirectory_file, "\\CycleBoxPlot.pdf")


	print(wilcox.test(mobileBugs, desktopBugs))
	print(wilcox.test(mobileBugs, siblingBugs))
	print(wilcox.test(desktopBugs, siblingBugs, alternative="two.sided", var.equal=FALSE))
	
	
	createHistogram(mobileFeatures, "Mobile Features")
	#createHistogram(desktopBugs, "Desktop Bugs")
	#createHistogram(siblingBugs, "Sibling Bugs")

	
}

runAllWilcoxTests <-function(){
		filename = "/Users/kendallbailey/Research_1/VersionClassifier/Paper/tables"
	
	
	runWilcoxTests(mobileBugs, desktopBugs, siblingBugs, "Bugs", FALSE, appendStr(filename, "bugs.tex"))
	runWilcoxTests(mobileFeatures, desktopFeatures, siblingFeatures, "Features", FALSE, appendStr(filename, "features"))
	
	runWilcoxTests(mobileEnhancements, desktopEnhancements, siblingEnhancements, "Enhancements", FALSE, appendStr(filename, "enhancements.tex"))
	
	runWilcoxTests(mobileNonFunc, desktopNonFunc, siblingNonFunc, "Non-Functional", FALSE, appendStr(filename, "nonFunc.tex"))
	
	runWilcoxTests(mobileCyc, desktopCyc, siblingCyc, "Cycle Length", FALSE, appendStr(filename, "cycle.tex"))
	
	runLinearModel(mobileCycles, mobileBugs, mobileFeatures, mobileEnhancements, mobileNonFunc, "Mobile Applications", appendStr(filename, "mobileMod.tex"), FALSE)
	
	runLinearModel(desktopCycles, desktopBugs, desktopFeatures, desktopEnhancements, desktopNonFunc, "Desktop Applications", appendStr(filename, "deskMod.tex"), FALSE)
	
	runLinearModel(siblingCycles, siblingBugs, siblingFeatures, siblingEnhancements, siblingNonFunc, "Sibling Applications", appendStr(filename, "sibMod.tex"), FALSE)
	
	
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
	
	
runLinearModel <- function(time, bugs, features, enhancements, nonFunc, title, filename, apnd) {
	print(length(time))
	
	summ = lm(formula= time ~bugs + features + enhancements + nonFunc)
	
	print(texreg(summ, custom.model.names = c(title), custom.coef.names = c("(Intercept)", "Bugs", "Features", "Enhancements", "Non-Functional" )), file=filename, append= apnd)
}

generateAllSummaries <- function(){
	filename = "/Users/kendallbailey/Research_1/VersionClassifier/Paper/summaries" 
	
	generateSummary(mobileCyc, desktopCyc, siblingCyc, "Cycle Summary", appendStr(filename, "Cycles.tex"))
	
	generateSummary(mobileBugs, desktopBugs, siblingBugs, "Bug Summary", appendStr(filename, "Bugs.tex"))
	
	generateSummary(mobileFeatures, desktopFeatures, siblingFeatures, "Feature Summary", appendStr(filename, "Features.tex"))
	
	generateSummary(mobileEnhancements, desktopEnhancements, siblingEnhancements, "Enhancement Summary", appendStr(filename, "Enhancements.tex"))
	
	generateSummary(mobileNonFunc, desktopNonFunc, siblingNonFunc, "Non-Functional Summary", appendStr(filename, "NonFunc.tex"))
	
}

generateSummary <- function(mobdata, deskdata, sibdata, title, filename){
	mn = c(mean(mobdata), mean(deskdata), mean(sibdata))
	mdn = c(median(mobdata), median(deskdata), median(sibdata))
	sdev = c(sd(mobdata), sd(deskdata), sd(sibdata))
	mx = c(max(mobdata), max(deskdata), max(sibdata))
	
	m = c(mean(mobdata), median(mobdata), sd(mobdata), max(mobdata))
	d = c(mean(deskdata), median(deskdata), sd(deskdata), max(deskdata))
	s = c(mean(sibdata), median(sibdata), sd(sibdata), max(sibdata))
	
	result = data.frame(mn,mdn,sdev,mx, row.names = c( "Mobile Applications", "Desktop Applications", "Sibling Applications"))
	#result = data.frame(m,d,s)
	#result = data.frame(mn,mdn,sdev,mx)

	colnames(result) <- c("mean", "median", "std", "max")
	print(result)
		
	print(xtable(result, digits=3,caption = title ), file=filename, append=FALSE, include.rownames=FALSE, caption.placement= 'top', hline.after =TRUE)
	
}
	
createHistogram <-function(data, titl){
	hist(data, main=titl, ylab="Number of Releases with x Features", xlab = "Number of Features in a Release")
	
}
	

