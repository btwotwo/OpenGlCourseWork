   M S B A M L   `   `   `  ����J  FMainApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MainApplicationMainApplication  5   	   G CGLWpfControl, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null=.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl
OpenTK.Wpf 5   	        MainApplication.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MainApplication     5   	   GglWpfControl.clr-namespace:OpenTK.Wpf;assembly=GLWpfControl     5   	     � Title$  
MainWindow��5	   	   $	��900��6   $
��1100��6)    � 
ResizeMode$ NoResize=�5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $	��10* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      $
��0.3* �6   .+�5   
   �� -   5         ��Name$ SetTetrahedron��5   !   $��0��6   .��6V   Тетраэдр6`   �� 5   
   -   5        $ SetCube��5   !   $��1��6   .��6J   Гексаэдр6T   �� 5   
   -   5        $ SetOctahedron��5   !   $��2��6   .��6T   Октаэдр6]   �� 5   
   -   5         ��Name$ SetWired��5   #   $��3��6   .��6o   Каркас6w     OpenTK.Wpf.GLWpfControl  5   
   -   5          Name$ OpenTkControl��5      $��1��5   $   $��4��61   5      5      5      