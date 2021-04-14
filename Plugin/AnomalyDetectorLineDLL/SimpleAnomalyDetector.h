/*
 * SimpleAnomalyDetector.h
 *
 *  Created on: 8 срхїз 2020
 *      Author: Eli
 */

#ifndef SIMPLEANOMALYDETECTOR_H_
#define SIMPLEANOMALYDETECTOR_H_

#include "anomaly_detection_util.h"
#include "AnomalyDetector.h"
#include "API.h"
#include <vector>
#include <algorithm>
#include <string.h>
#include <math.h>
#include <iostream>   // std::cout
#include <string>     // std::string, std::to_string
#include <fstream>
#include <sstream>

struct correlatedFeatures {
	string feature1, feature2;  // names of the correlated features
	float corrlation;
	Line lin_reg;
	float threshold;
	float cx, cy;
};



class SimpleAnomalyDetector :public TimeSeriesAnomalyDetector {
protected:
	vector<correlatedFeatures> cf;
	float threshold;
public:
	SimpleAnomalyDetector();
	virtual ~SimpleAnomalyDetector();

	virtual void learnNormal(const TimeSeries& ts);
	virtual vector<AnomalyReport> detect(const TimeSeries& ts);
	void detectTotxtFile(const TimeSeries& ts, const char* txtFileName);
	vector<correlatedFeatures> getNormalModel() {
		return cf;
	}
	void setCorrelationThreshold(float threshold) {
		this->threshold = threshold;
	}


	// helper methods
protected:
	virtual void learnHelper(const TimeSeries& ts, float p/*pearson*/, string f1, string f2, Point** ps);
	virtual bool isAnomalous(float x, float y, correlatedFeatures c);
	Point** toPoints(vector<float> x, vector<float> y);
	float findThreshold(Point** ps, size_t len, Line rl);
	
	/**
public:
	void mostCorrelative(const char* CSVfileName, string f1, correlativeFeatureWrapper* wrapper, float MinX, float MaxX);
	string mostCorrelativeHelper(const TimeSeries& ts, string f1);
	**/
};



#endif /* SIMPLEANOMALYDETECTOR_H_ */
#pragma once
#pragma once
