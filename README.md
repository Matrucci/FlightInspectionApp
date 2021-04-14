# Flight inspection app
#### Created by:
- Matan Saloniko
- Idan Givati
- Raviv Haham
- Peleg Haham


Project Description
- 
In this project, we worked on several features:
- Interfacing with FlightGear (we worked with FlightGear 2020.3.6, but most versions would work).
- We created an application that works side by side with FlightGear and provides playback controls, visual representation of the airplane's controllers, the data about the flight and provides some analysis about the data in graph form.
- The program allows the user to upload 2 csv files, checks for anomalies about the second flight and marks them with red dots in the graph.

## Requirements:

- FlightGear is necessary in order to run the the application correctly.
Download links:
https://www.flightgear.org/download/

- After installation, head over to the Settings tab and in "Additional Settings" put the following lines and then press Fly!:

INPUT COMMAND HERE

## Project structure:

In this project we focused on MVVM and Data Binding both could be seen a lot throughout the code.
We created multiple Views (MainWindow and Advanced Details), each of them having a ViewModel and Model of it's own.
We also created multiple UserControls (HUD, Playback, PitchRoll, etc) that can be implemented in future projects independently.
Please note, that the Joystick UserControl was created by our course staff over at BIU and was not created by us. Full credit for this specific UserControl goes to them.

![UML](https://i.imgur.com/FNQjCmi.png)

[Classes Description](FlightInspectionApp/ClassDetails.md)


## DLL:
Throughout the project we used dll files several times:
AnomalyDetectorDLL.dll, AnomalyDetectorLineDLL.dll , AnomalyDetectorCircleDLL.dll , CSdll.dll

We used AnomalyDetectorDLL.dll to find the correlative fields and the regression line, which we saved on the mapCSV map, and this helped us to build the graphs of user stories 6, 7 and 8. We used AnomalyDetectorCircleDLL.dll and AnomalyDetectorLineDLL.dll that help us to show different kind of researchers that the anomaly detection algorithm find them so that the user can load a first dynamic algorithm that detects an anomaly using a line regration, investigate the flight with it and then load another algorithm that detects anomalies by circle.
Note that the CSdll.dll file is written in c# (compared to the other dll files that written in c++), it is loaded dynamically by the user, which allows great flexibility and thus the user can connect any algorithm he wants.
CSdll.dll should only call the "getAnomaly" method that it's arguments are:
string CSVLearnFileName, string CSVTestFileName, string txtFileName, and returns nothing (void), so it's signiture is: 
public static extern void getAnomaly(string CSVLearnFileName, string CSVTestFileName, string txtFileName)
The user who prepares the CSdll.dll file should make sure that in the first line he writes the shape with which he wanted to study the flight (regression line, circle etc.) A line below he will write for each pair of correlative fetures: the name of the properties, a line below will write the indexes of the regration in their set of values, and in the line below he will write the x points (and below the y points) that creates the shape in which he investigated the anomalies.

## Running this project on a brand new machine:

- First you will need to install Visual Studio.
	Download page:
	https://visualstudio.microsoft.com/
	We recommend getting the community version of Visual Studio. Although, any other version would work just fine.
- Installing FlightGear (as mentioned above).

### Running

- To run the application press ctrl+F5 in order to run without debugging.
- Run the FlightGear application with the configurations above.
- Upload your xml file that describes the flight's values (you can build your own but we have provided a file for example that would work).
- Upload your csv file with the flight's values (again, you can provide your own but one is provided inside the project's folder).
- Enjoy!

## Future improvements:

As we continue to work on this app, we encourage anyone that wants to help out to do so!
Just open the project in Visual Studio and add your own touches!
This project uses OxyPlot in order to build and display and graphs so you will probably want to get familiar with that.
Other than that, we would appreciate if you would try to stick to our design language and patterns.
Have fun with this project and don't forget to create a pull request once you're done so this project could have a little bit of YOU in it!
