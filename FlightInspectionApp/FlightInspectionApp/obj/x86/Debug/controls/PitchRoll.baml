   M S B A M L   `   `   `  ����N  JFlightInspectionApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:FlightInspectionApp.controlsFlightInspectionApp.controls  5      ,    &FlightInspectionApp.controls.PitchRoll   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:FlightInspectionApp.controls     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Pitch:��6   $	��Top=�6"   $
��Left=�6:   
�������$��15(�6h   �� 5   
   $	��Top=�6.   $
��Left=�6F   
�������$��15(�6t   $��50,0,0,0q�6�   ��6   �� *
VM_Pitch+6�   �� 5   
   $��0��6   
����  �	 �� X1$ 20��6*   	 �� Y1$ 87��62   	 �� X2$	 330��6:   	 �� Y2$ 87��6C   $��4��6K   F� 5   
   
�������$��2��6   ��5      B� 5      ��5      D� E� 5       E� 
StartPoint$ 155,20&�6)   ��5   &   @� �� 5   *    �� Size$ 50,50��65    �� RotationAngle$ 45Q�6B    �� 
IsLargeArc . 	 �� SweepDirection$	 CounterClockwise=�6g   
 �� Point$
 155, 154&�6�   5   '   5      5      5       5"      �� 5#   
   -   5        $��
pitch_line��5#      $��0��6   
���  ��$	 260��6;   $ 87��6D   $ 90��6L   $ 87��6T   $��4��6\   z�5$      �� 5%       �� CenterX$	 175Q�6"    �� CenterY$ 87Q�60    �� Angle 6=   �� *
VM_Pitch+5'      �� 5+   
   $��Roll:��6   $	��Top=�6!   $
��Left=�69   
�������$��15(�6g   $��1��6u   �� 5,   
   $	��Top=�6-   $
��Left=�6E   
�������$��15(�6s   $��50,0,0,0q�6�   $��1��6�   ��6   �� *	VM_Roll+6�   �� 5-   
   $��1��6   
����  �$	 175��6*   $ 20��63   $	 175��6;   $	 155��6D   $��4��6M   �� 5.   
   $��1��6   
���  ��$ 90��6)   $ 87��61   $	 260��69   $ 87��6B   $��4��6J   z�5/      �� 50      $	 175Q�6"   $ 87Q�60    6=   �� *	VM_Roll+52      54      55      