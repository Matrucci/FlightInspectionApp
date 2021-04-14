#include "pch.h"
#include "API.h"
#include "commands.h"
#include "SimpleAnomalyDetector.h"
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




