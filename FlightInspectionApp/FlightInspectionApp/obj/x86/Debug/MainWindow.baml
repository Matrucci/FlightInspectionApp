   M S B A M L   `   `   `  ����N  JFlightInspectionApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FlightInspectionAppFlightInspectionApp  5   	   K*clr-namespace:FlightInspectionApp.controlsFlightInspectionApp.controls  5   	   $    FlightInspectionApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:FlightInspectionApp     5   	   ?Controls*clr-namespace:FlightInspectionApp.controls     5   	     � Title$  Main App��5	   	   $	��750��6   $
��1200��6'   .��5      � 
���   �.+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      +   %FlightInspectionApp.controls.playback  -   5          Name$ playback_controls��5   4   $y�Hidden=�6    64   �� 5      -   5        $��controllers��5   0   $ Vertical=�6   $y�Hidden=�6C   .+�5      �� $ 
Horizontal=�6   .+�5      �� -   5        $��slider_throttle��5      $	��340��64   $
��Left=�6A   $��10, 10, 10, 10q�6\    �� Orientation$ Vertical=�6t    � Minimum$ 0Q�6�    � Maximum$ 1Q�6�    � Value 6�   �� *VM_Throttle+6�   +   %FlightInspectionApp.controls.Joystick  5      -   5          Name$ joystick��5   (   $
��Left=�6:   $��10, 10, 10, 10q�6U   &    FlightInspectionApp.controls.HUD  5      $��30,0,0,0q�6#   $	��350��65   $	��350��6A   ,   &FlightInspectionApp.controls.PitchRoll  5      $��10,0,0,0q�6)   5      �� 5      -   5        $��slider_rudder��5      $	��340��6.   $
��Left=�6:   $��45, 10, 10, 10q�6U   $ -1Q�6�   $ 1Q�6�    6m   �� *	VM_Rudder+6�   5      5      �� 5   
   -   5         ��Name$ details_btn��5      $	��100��6&   $��40��62   $��Advanced details��6>   $��Bottom=�6Y   $
��Left=�6t   $��30, 0, 0, 30q�6�   $y�Hidden=�6�   �� 5   
   -   5        $ upload_xml_btn��5      $��Upload XML File��6)   $��Right=�6C   $��Bottom=�6_   $��
0,0,200,30q�6z   $	��150��6�   $��40��6�   6�   �� 5   
   -   5        $ upload_csv_reg_btn��5      $��Upload train CSV��6-   $��Right=�6H   $��Bottom=�6d   $��
0,0,200,30q�6   $	��150��6�   $��40��6�   $y�Hidden=�6�   6�   �� 5   
   -	   5        $ upload_csv_btn��5      $��Upload anomaly CSV File��6)   $��Right=�6K   $��Bottom=�6g   $��	0,0,25,30q�6�   $	��150��6�   $��40��6�   6�   �� 5   
   -
   5        $ 
upload_dll��5      $��Upload your DLL��6%   $
��Left=�6?   $��Bottom=�6Z   $��150, 0, 0, 30q�6u   $	��150��6�   $��40��6�   6�   5      5      