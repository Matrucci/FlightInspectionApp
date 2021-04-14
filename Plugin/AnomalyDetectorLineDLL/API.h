#ifndef _STRINGWRAPPER_H_
#define _STRINGWRAPPER_H_
#include <string>
#include <iostream>
using namespace std;

/**
class correlativeFeatureWrapper {
	string str;
	float MinY;
	float MaxY;

public:
	correlativeFeatureWrapper() {
		str = "";
		MinY = 0;
		MaxY = 0;
	}
	void setMinY(float YMin) {
		MinY = YMin;
	}
	void setMaxY(float YMax) {
		MaxY = YMax;
	}
	void setStr(string correlativeItemName) {
		str = correlativeItemName;
	}
	int correlativeNameLen() {
		return str.size();
	}
	char getCharByIndex(int x) {
		return str[x];
	}
	float getMinY() {
		return MinY;
	}
	float getMaxY() {
		return MaxY;
	}
};
**/


#endif





extern "C" __declspec(dllexport) void getAnomaly(const char* CSVLearnFileName, const char* CSVTestFileName, const char* txtFileName);

/**
////////////////////////////
extern "C" __declspec(dllexport) void getNumberCheck();
//extern "C" __declspec(dllexport) void getCorrelativeFeatureNameNew(const char* CSVfileName, const char* selectedFeatureName);
////////////////////////////

extern "C" __declspec(dllexport) void* getCorrelativeFeatureData(const char* CSVfileName, const char* selectedFeatureName, float MinX, float MaxX);

extern "C" __declspec(dllexport) int correlativeStrLen(correlativeFeatureWrapper * wrapper);

extern "C" __declspec(dllexport) char getCharByIndex(correlativeFeatureWrapper * wrapper, int x);

extern "C" __declspec(dllexport) float getMinY(correlativeFeatureWrapper * wrapper);

extern "C" __declspec(dllexport) float getMaxY(correlativeFeatureWrapper * wrapper); #pragma once
**/