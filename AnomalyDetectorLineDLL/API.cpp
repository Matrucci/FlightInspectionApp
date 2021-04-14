#include "pch.h"
#include "API.h"
#include "commands.h"
#include "SimpleAnomalyDetector.h"
#include "anomaly_detection_util.h"
#include "CLI.h"
#include <iostream>
#include <fstream>
#include <sstream>





extern "C" __declspec(dllexport) void getAnomaly(const char* CSVLearnFileName, const char* CSVTestFileName, const char* txtFileName) {

	TimeSeries tsLearn(CSVLearnFileName);
	TimeSeries tsTest(CSVTestFileName);
	SimpleAnomalyDetector simpleAD;
	simpleAD.learnNormal(tsLearn);
	simpleAD.detectTotxtFile(tsTest, txtFileName);
}



/**
*
* //getAnomalies - help to get information from the menue by giving the information in a txt file.
*
void EXPORT getAnomalies(const char* inputFile, const char* outputFile) {
	STDtest std(inputFile, outputFile);
	CLI cli(&std);
	cli.start();
	std.close();
}
**/


/**

////////////////////////////
extern "C" __declspec(dllexport) void getNumberCheck() {
	ofstream myfile;
	myfile.open("checkHelp.txt");
	myfile << "Writing this to a file.\n";
	myfile.close();
}


/**
extern "C" __declspec(dllexport) void getCorrelativeFeatureNameNew(const char* CSVfileName, const char* selectedFeatureName) {
	SimpleAnomalyDetector simpleAD;
	string newStr = simpleAD.mostCorrelative(CSVfileName, selectedFeatureName);
	ofstream myfile;
	myfile.open("checkfour.txt");
	myfile << "Writing this to a file.\n";
	myfile << newStr;
	myfile.close();
}

////////////////////////////


extern "C" __declspec(dllexport) void* getCorrelativeFeatureData(const char* CSVfileName, const char* selectedFeatureName, float MinX, float MaxX) {
	string newStr = "";
	correlativeFeatureWrapper* wrapper = new correlativeFeatureWrapper();
	SimpleAnomalyDetector simpleAD;
	simpleAD.mostCorrelative(CSVfileName, selectedFeatureName, wrapper, MinX, MaxX);

	////////////////////////////
	ofstream myfile;
	myfile.open("checkfive.txt");
	myfile << "selectedFeatureName is: " << selectedFeatureName << "\n";
	myfile << "newStr is: " << newStr << "\n";
	myfile << "3" << "\n";
	myfile.close();
	////////////////////////////

	return (void*)wrapper;
}


int correlativeStrLen(correlativeFeatureWrapper* wrapper) {
	return wrapper->correlativeNameLen();
}

char getCharByIndex(correlativeFeatureWrapper* wrapper, int x) {
	return wrapper->getCharByIndex(x);
}


float getMinY(correlativeFeatureWrapper* wrapper) {
	return wrapper->getMinY();
}
float getMaxY(correlativeFeatureWrapper* wrapper) {
	return wrapper->getMaxY();
}

**/