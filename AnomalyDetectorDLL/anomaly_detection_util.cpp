#include "pch.h"
/*
 * animaly_detection_util.cpp
 *
 *  Created on: 11 срхїз 2020
 *      Author: Eli
 */


#include <iostream>
#include <fstream>
#include <sstream>
#include "anomaly_detection_util.h"
//#include<vector>

float avg(float* x, int size) {
	float sum = 0;
	for (int i = 0; i < size; sum += x[i], i++);
	return sum / size;
}

// returns the variance of X and Y
float var(float* x, int size) {
	float av = avg(x, size);
	float sum = 0;
	for (int i = 0; i < size; i++) {
		sum += x[i] * x[i];
	}
	return sum / size - av * av;
}

// returns the covariance of X and Y
float cov(float* x, float* y, int size) {
	float sum = 0;
	for (int i = 0; i < size; i++) {
		sum += x[i] * y[i];
	}
	sum /= size;

	return sum - avg(x, size) * avg(y, size);
}


// returns the Pearson correlation coefficient of X and Y
float pearson(float* x, float* y, int size) {
	return cov(x, y, size) / (sqrt(var(x, size)) * sqrt(var(y, size)));
}

// performs a linear regression and returns the line equation
Line linear_reg(Point** points, int size) {
	//the change for dll
	//float x[size], y[size];
	std::vector<float> x(size);
	std::vector<float> y(size);
	
	

	for (int i = 0; i < size; i++) {
		x[i] = points[i]->x;
		y[i] = points[i]->y;
	}

	

	//the change for dll
	//float a = cov(x, y, size) / var(x, size);
	//float b = avg(y, size) - a * (avg(x, size));
	float a = cov(&x[0], &y[0], size) / var(&x[0], size);
	float b = avg(&y[0], size) - a * (avg(&x[0], size));

	return Line(a, b);
}

// returns the deviation between point p and the line equation of the points
float dev(Point p, Point** points, int size) {
	Line l = linear_reg(points, size);
	return dev(p, l);
}

// returns the deviation between point p and the line
float dev(Point p, Line l) {
	return abs(p.y - l.f(p.x));
}





// performs a linear regression and returns the line equation
vector<float> getMinMaxY(vector<float> selectedFeature, vector<float> correlativeFeature, float MinX, float MaxX) {
	int size = selectedFeature.size();
	vector<float> minMaxYVec;
	//the change for dll
	//float a = cov(selectedFeature, correlativeFeature, size) / var(selectedFeature, size);
	//float b = avg(correlativeFeature, size) - a * (avg(selectedFeature, size));
	float a = cov(&selectedFeature[0], &correlativeFeature[0], size) / var(&selectedFeature[0], size);
	float b = avg(&correlativeFeature[0], size) - a * (avg(&selectedFeature[0], size));
	float MinY = (a * MinX) + b;
	float MaxY = (a * MaxX) + b;
	minMaxYVec.push_back(MinY);
	minMaxYVec.push_back(MaxY);
	return minMaxYVec;
}


