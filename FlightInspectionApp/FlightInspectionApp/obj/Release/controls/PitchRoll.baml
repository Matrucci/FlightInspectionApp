   M S B A M L   `   `   `  ���� N  JFlightInspectionApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:FlightInspectionApp.controlsFlightInspectionApp.controls  ,    &FlightInspectionApp.controls.PitchRoll   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:FlightInspectionApp.controls     .���   � RowDefinitions  �� $��* ��� $��* �.+��� $��Pitch��$	��Top=�$
��Left=�
�������$��15(��� $��0��
����  �	 �� X1$ 20��	 �� Y1$ 87��	 �� X2$	 330��	 �� Y2$ 87��$��4��F� 
�������$��2����B� ��D� E�  E� 
StartPoint$ 155,20&���@� ��  �� Size$ 50,50�� �� RotationAngle$ 45Q� �� 
IsLargeArc . 	 �� SweepDirection$	 CounterClockwise=�
 �� Point$
 155, 154&��� -   $��
pitch_line��$��0��
���  ��$	 260��$ 87��$ 90��$ 87��$��4��z���  �� CenterX$	 175Q� �� CenterY$ 87Q� �� Angle �� *
VM_Pitch+�� $
��Roll��$	��Top=�$
��Left=�
�������$��15(�$��1���� $��1��
����  �$	 175��$ 20��$	 175��$	 155��$��4���� $��1��
���  ��$ 90��$ 87��$	 260��$ 87��$��4��z��� $	 175Q�$ 87Q� �� *	VM_Roll+