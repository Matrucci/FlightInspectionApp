   M S B A M L   `   `   `  ����R  NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0>.clr-namespace:OxyPlot.Wpf;assembly=OxyPlot.WpfOxyPlot.Wpf  5   	   N JFlightInspectionApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:FlightInspectionAppFlightInspectionApp 5   	   9!clr-namespace:FlightInspectionAppFlightInspectionApp 5
   	   )   #FlightInspectionApp.AdvancedDetails   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   >oxy.clr-namespace:OxyPlot.Wpf;assembly=OxyPlot.Wpf     5   	   AFlightInspectionApp!clr-namespace:FlightInspectionApp     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	   	   3local!clr-namespace:FlightInspectionApp     5
   	     � Title$  
MainWindow��5   	   
����İ�$	��650��68   $
��1100��6E   .��5      � $��0,0,0.333,-0.333q�6   
�����w�$	��421��6A   $��Bottom=�6N   .+�5   
      OxyPlot.Wpf.PlotView  
�������$��707,-170,42,299q�6o      Model 6   �� *	plotModel+ �� UpdateSourceTrigger$ PropertyChanged=�6o     5   
   
�������$��311,-170,423,299q�6r    6   �� *plotModelTwo+$ PropertyChanged=�6r     5   
   
�������$��465,127,200.333,-3q�6t    J� RenderTransformOrigin$ 0.271,0.482&�6�    6   �� *plotModelThree+$ PropertyChanged=�6�   �� 5   
   -   5         ��Name$ l1��5      $��Right=�6   $	��591��6:   $��0,-170,807,0q�6G   
�������$	��Top=�6t   $	��276��6�   ��6�   �� *vm_Colnames+$ PropertyChanged=� �� SelectedItem 6  �� *vm_SelectedItem+$ PropertyChanged=�5      5      