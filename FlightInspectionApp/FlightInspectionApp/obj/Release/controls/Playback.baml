   M S B A M L   `   `   `  ���� N  JFlightInspectionApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:FlightInspectionApp.controlsFlightInspectionApp.controls  +    %FlightInspectionApp.controls.playback   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:FlightInspectionApp.controls     .����   �� Orientation$  Vertical=�.+��  � RowDefinitions �� $��70 ��� $��50 �.+��� 
����������	#FF4798EA �� CornerRadius$ 50,50,50,50��._��� $  
Horizontal=�$	��Top=�$��Center=�$��0��.+��� -    ��Name$ 	skip_back��$��50��$��50��$��10, 10, 10, 10q�
������ 
������ .���� $7��1pack://application:,,,/Resources/029-previous.png������  �� 
TargetType" ��.���  �� Property � �� �� Value
 ��ح���  � �� ��  �� 
TargetType" ��.R���  ��Name$ Border��#��z��$ 40��._��� $��Center=�$��Center=�	 �� Triggers	 P� 
 P� Property
 �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ Border�� � ��
 ��ح��� -   $ rewind��$��50��$��50��$��10, 10, 10, 10q�
������ 
������ .���� $7��1pack://application:,,,/Resources/040-backward.png������ " ��.���  � ��
 ��ح���  � �� �� " ��.R��� $ Border��#��z��$ 40��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ��� $ Border�� � ��
 ��ح��� -   $ pause��$��50��$��50��$��10, 10, 10, 10q�
������ 
������ .���� $4��.pack://application:,,,/Resources/021-pause.png������ " ��.���  � ��
 ��ح���  � �� �� " ��.R��� $ Border��#��z��$ 40��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ��� $ Border�� � ��
 ��ح��� -   $
 play��$��50��$��50��$��10, 10, 10, 10q�
������ 
������ .���� $3��-pack://application:,,,/Resources/013-play.png������ " ��.���  � ��
 ��ح���  � �� �� " ��.R��� $ Border��#��z��$ 40��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ��� $ Border�� � ��
 ��ح��� -   $
 stop��$��50��$��50��$��10, 10, 10, 10q�
������ 
������ .���� $3��-pack://application:,,,/Resources/003-stop.png������ " ��.���  � ��
 ��ح���  � �� �� " ��.R��� $ Border��#��z��$ 40��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ��� $ Border�� � ��
 ��ح��� -   $ forward��$��50��$��50��$��10, 10, 10, 10q�
������ 
������ .���� $7��1pack://application:,,,/Resources/032-forwards.png������ " ��.���  � ��
 ��ح���  � �� �� " ��.R��� $ Border��#��z��$ 40��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ��� $ Border�� � ��
 ��ح��� -   $	 end��$��50��$��50��$��10, 10, 10, 10q�
������ 
������ .���� $3��-pack://application:,,,/Resources/033-next.png������ " ��.���  � ��
 ��ح���  � �� �� " ��.R��� $ Border��#��z��$ 40��._��� $��Center=�$��Center=�	 P� 
 �@J�IsMouseOver . . ��� $ Border�� � ��
 ��ح��� $��Play Speed: ��$��10, 20, 10, 10q�$
��Wrap=�$	��120��$��20(� �� TextAlignment$ Center=��� -   $��speed��$��50��$��25��$��5, 10, 10, 10q�.@��� -	   $��speed_01��$	��0.1���� -
   $��	speed_025��$
��0.25���� -   $��speed_05��$	��0.5���� -   $��	speed_075��$
��0.75���� -   $��speed_1��$��1�� �� 
IsSelected . �� -   $��	speed_125��$
��1.25���� -   $��speed_15��$	��1.5���� -   $��	speed_175��$
��1.75���� -   $��speed_2��$��2���� $��1��$  
Horizontal=�$��Bottom=�$��Center=�.+��� -   $��playback_slider��$	��600��$��Center=�$��Center=�$��10, 10, 10, 10q� � Minimum$ 0Q� �� IsSelectionRangeEnabled .  � Value �� *VM_LineNumber+ �� Mode$ TwoWay=� � Maximum �� *VM_NumberOfLines+ �� SelectionEnd �� *VM_LineNumber+ � 	Resources  �� Color��%�   (��       * SystemColors.HighlightBrushKey+)(��      *&$SystemColors.ControlDarkDarkBrushKey+)�� $ 	LightBlue���� $ Silver��