# Further Explanation about the classes

## MainWindow:
This class uses xaml for design. Implementing custom user controls like Joystick, HUD, etc. (more details about those below).
The MainWindow class is responsible of handling the front end and the design logic.
This class does not use some complex logic to decide how to move on, but instead, offloads that work to the ViewModel class (more details below).
MainWindow also uses data binding and gets notified by PropertyChanged notifications in order to function more fluently.

## ViewModel:
This class is the main driver of the application. It applies the logic of the application and handles the data that's being transferred from the model to the view and vice-versa.
The ViewModel class also updates the AdvancedDetailsVM (details below) in order to sync the graphs to the actual flight gear representation.

## FlightGearClient:
This class is the model and it handles the back end of the application. From sending the file to the FlightGear application to updating the ViewModel about changed values through the INotifyPropertyChange interface.

## AdvancedDetails:
This class is the front end of the graphs and the advanced details about the flight. It also marks anomalies in the graph.

## AdvancedDetailsVM:
The ViewModel of the advanced details part. Communicates with the model and the view in order to transfer data between them independently.

## AdvancedDetailsModel:
This class handles the back end and "heavy lifting" of the graph creation.

# User Controls

## Joystick:
This user control represents the joystick and uses data binding in order to move it around.
DISCLAIMER:  THIS USER CONTROL WAS GIVEN TO US BY OUR INSTRUCTORS AND WAS NOT CREATED BY US.

## Playback:
The playback controls uses data binding in order to change the slider as the flight goes on and also lets the user change the flight timing.
Quick explanation about the buttons:

![playback](https://i.imgur.com/UiIB0E1.png)

We'll go over from left to right:

Back - Brings the flight all the way back to the start.

Rewind - Brings the flight back 5 seconds.

Pause - Pauses the flight.

Play - Continue playing from the same spot (if paused or stopped).

Stop - Pauses the flight but also sets it all the way back to the start.

Forward - Sets the flight 5 seconds ahead.

Skip - Sets the flight all the way to the very end.

Play Speed - Let's the decide which speed he wants to watch the flight in (Inspired by the YouTube player).

Slider - Let's the user jump back and forwards as he wishes.

## PitchRoll:
This user control represents the pitch of the plane and the roll.

![PitchRoll](https://i.imgur.com/cZihUAm.png)

The pitch is the angle of the plane's nose to the x axis (in this representation, the nose is on the left).
The roll represents the tilt of the plane.

## HUD:
This user control shows useful data about the current conditions of the flight.

![HUD](https://i.imgur.com/dIqzyo6.png)

Yaw: The direction of the plane (in degrees).
Altimeter: The height of the plane above sea level (in feet).
Air Speed: The speed of the air outside the plane (in kt).

## Joystick:
This user control shows the elevation and aileron of the plane in joystick form.

![Joystick](https://i.imgur.com/svSZ7Xn.png)

THIS USER CONTROL WAS GIVEN TO US BY OUR INSTRUCTORS.