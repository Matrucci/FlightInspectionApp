#include "pch.h"
/*
 * SimpleAnomalyDetector.cpp
 *
 *  Created on: 8 срхїз 2020
 *      Author: Eli
 */

#include "SimpleAnomalyDetector.h"
#include "minCircle.h"

SimpleAnomalyDetector::SimpleAnomalyDetector() {
	threshold = (float)0.5;

}

SimpleAnomalyDetector::~SimpleAnomalyDetector() {
	// TODO Auto-generated destructor stub
}

Point** SimpleAnomalyDetector::toPoints(vector<float> x, vector<float> y) {
	//the change for dll
	Point** ps = new Point * [x.size()];
	//std::vector<Point*> ps(x.size());
	for (size_t i = 0; i < x.size(); i++) {
		ps[i] = new Point(x[i], y[i]);
	}
	return &ps[0];
}

float SimpleAnomalyDetector::findThreshold(Point** ps, size_t len, Line rl) {
	float max = 0;
	for (size_t i = 0; i < len; i++) {
		float d = abs(ps[i]->y - rl.f(ps[i]->x));
		if (d > max)
			max = d;
	}
	return max;
}

void SimpleAnomalyDetector::learnNormal(const TimeSeries& ts) {
	vector<string> atts = ts.gettAttributes();
	size_t len = ts.getRowSize();
	//vector<float> rows(atts.size());

	//the change for dll
	//float vals[atts.size()][len];

	std::vector<vector<float>> vals(atts.size(), vector<float>(len));
	for (size_t i = 0; i < atts.size(); i++) {
		vector<float> x = ts.getAttributeData(atts[i]);
		for (size_t j = 0; j < len; j++) {
			vals[i][j] = x[j];
		}
	}

	for (size_t i = 0; i < atts.size(); i++) {
		string f1 = atts[i];
		float max = 0;
		size_t jmax = 0;
		for (size_t j = i + 1; j < atts.size(); j++) {
			float p = abs(pearson(&vals[i][0], &vals[j][0], len));
			if (p > max) {
				max = p;
				jmax = j;
			}
		}
		string f2 = atts[jmax];
		Point** ps = toPoints(ts.getAttributeData(f1), ts.getAttributeData(f2));

		learnHelper(ts, max, f1, f2, ps);

		// delete points
		for (size_t k = 0; k < len; k++)
			delete ps[k];
		delete[] ps;
	}
}

void SimpleAnomalyDetector::learnHelper(const TimeSeries& ts, float p/*pearson*/, string f1, string f2, Point** ps) {
	if (p > threshold && p < 0.9) {
		/**
		size_t len = ts.getRowSize();
		correlatedFeatures c;
		c.feature1 = f1;
		c.feature2 = f2;
		c.corrlation = p;
		c.lin_reg = linear_reg(ps, len);
		c.threshold = findThreshold(ps, len, c.lin_reg) * (float)1.1; // 10% increase
		cf.push_back(c);
		**/
		Circle cl = findMinCircle(ps, ts.getRowSize());
		correlatedFeatures c;
		c.feature1 = f1;
		c.feature2 = f2;
		c.corrlation = p;
		c.threshold = cl.radius * (float)1.1; // 10% increase
		c.cx = cl.center.x;
		c.cy = cl.center.y;
		cf.push_back(c);
	}
}

vector<AnomalyReport> SimpleAnomalyDetector::detect(const TimeSeries& ts) {
	vector<AnomalyReport> v;
	for_each(cf.begin(), cf.end(), [&v, &ts, this](correlatedFeatures c) {
		vector<float> x = ts.getAttributeData(c.feature1);
		vector<float> y = ts.getAttributeData(c.feature2);
		for (size_t i = 0; i < x.size(); i++) {
			if (isAnomalous(x[i], y[i], c)) {
				string d = c.feature1 + "-" + c.feature2;
				v.push_back(AnomalyReport(d, (i + 1)));
			}
		}
		});
	return v;
}


void SimpleAnomalyDetector::detectTotxtFile(const TimeSeries& ts, const char* txtFileName) {
	ofstream myfile;
	myfile.open(txtFileName);
	myfile << "Circle\n";
	vector<AnomalyReport> v;

	////////////////////////////////////////
	ofstream myfileTwo;
	myfileTwo.open("circleCheck525.txt");
	////////////////////////////////////////

	for_each(cf.begin(), cf.end(), [&myfileTwo,&myfile, &v, &ts, this](correlatedFeatures c) {
		vector<int> anomalyVec = vector<int>();
		vector<float> DrawX = vector<float>();
		vector<float> DrawY = vector<float>();
		vector<float> x = ts.getAttributeData(c.feature1);
		vector<float> y = ts.getAttributeData(c.feature2);
		for (size_t i = 0; i < x.size(); i++) {
			if (isAnomalous(x[i], y[i], c)) {
				string d = c.feature1 + "-" + c.feature2;
				v.push_back(AnomalyReport(d, (i + 1)));
				anomalyVec.push_back(i);
			}
		}
		myfile << c.feature1 + "," + c.feature2 + "\n";
		//myfile << to_string(c.cx) + "," + to_string(c.cy) + "," + to_string(c.threshold) + "\n";
		if (anomalyVec.size() > 0)
		{
			for (size_t i = 0; i < anomalyVec.size() - 1; i++)
			{
				myfile << to_string(anomalyVec[i]) + ",";
			}
			myfile << to_string(anomalyVec[anomalyVec.size() - 1]) + "\n";
		}
		else
		{
			myfile << to_string(-1) + "\n";
		}
		
		////////////////////////////////////////
		myfileTwo <<"+++++++"+ c.feature1 + "," + c.feature2 + "+++++++\n";
		myfileTwo << "c.cx=" + to_string(c.cx) + " , ";
		myfileTwo << "c.cy=" + to_string(c.cy) + " , ";
		myfileTwo << "c.threshold=" + to_string(c.threshold) + "\n";
		myfileTwo << "newwwww525\n";
		////////////////////////////////////////


		int isAllNan = 1;
		if ((!isnan(c.cx)) && (!isnan(c.cy)))
		{
			if (!isnan(c.threshold))
			{
				isAllNan = 0;

				for (int i = -180; i < 180; i++)
				{
					float xPoint = (c.threshold * cos(i)) + c.cx;
					myfile << to_string(xPoint) + ",";
				}
				float xPoint = (c.threshold * cos(180)) + c.cx;
				myfile << to_string(xPoint) + "\n";

				for (int i = -180; i < 180; i++)
				{
					float yPoint = (c.threshold * sin(i)) + c.cy;
					myfile << to_string(yPoint) + ",";
				}
				float yPoint = (c.threshold * sin(180)) + c.cy;
				myfile << to_string(yPoint) + "\n";
			}	
		}

		if (isAllNan)
		{
			myfile << "-\n";
			myfile << "-\n";
		}
		});
	myfile.close();

	////////////////////////////////////////
	myfileTwo.close();
	////////////////////////////////////////
}


bool SimpleAnomalyDetector::isAnomalous(float x, float y, correlatedFeatures c) {
	//return ((c.corrlation > 0.5 && c.corrlation < threshold) && (dist(Point(c.cx, c.cy), Point(x, y)) > c.threshold));
	return ((c.corrlation > 0.5) && (dist(Point(c.cx, c.cy), Point(x, y)) > c.threshold));
}


/**
void SimpleAnomalyDetector::mostCorrelative(const char* CSVfileName, string selectedFeatureName, correlativeFeatureWrapper* wrapper, float MinX, float MaxX) {

	TimeSeries ts(CSVfileName);
	string correlativeFeature = mostCorrelativeHelper(ts, selectedFeatureName);
	wrapper->setStr(correlativeFeature);
	vector<float> vectorMinMaxY = getMinMaxY(ts.getAttributeData(selectedFeatureName), ts.getAttributeData(correlativeFeature), MinX, MaxX);
	wrapper->setMinY(vectorMinMaxY[0]);
	wrapper->setMaxY(vectorMinMaxY[1]);
}

string SimpleAnomalyDetector::mostCorrelativeHelper(const TimeSeries& ts, string selectedFeatureName) {
	vector<string> atts = ts.gettAttributes();
	size_t len = ts.getRowSize();
	size_t indexF1 = 0;

	//the change for dll
	//float vals[atts.size()][len];

	std::vector<vector<float>> vals(atts.size(), vector<float>(len));
	for (size_t i = 0; i < atts.size(); i++) {
		if (atts[i] == selectedFeatureName)
		{
			indexF1 = i;
		}
		vector<float> x = ts.getAttributeData(atts[i]);
		for (size_t j = 0; j < len; j++) {
			vals[i][j] = x[j];
		}
	}

	float max = 0;
	size_t jmax = 0;
	for (size_t j = indexF1 + 1; j < atts.size(); j++) {
		float p = abs(pearson(&vals[indexF1][0], &vals[j][0], len));
		if (p > max) {
			max = p;
			jmax = j;
		}
	}
	string correlativeFeature = atts[jmax];
	return correlativeFeature;
}
**/