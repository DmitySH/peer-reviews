   M S B A M L   `   `   `  ����G  CDataAnalyzer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:DataAnalyzerDataAnalyzer  5   	       DataAnalyzer.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:DataAnalyzer     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��131* �6   �� 5      $
��661* �6   .+�5   
   o� $��25��6   $	��Top=�6   $��2��63   .@�5      m� $
��File��6   .@�5      m� -   5        $��OpenCSV��5      $��Open CSV��6*   6V   m� 5      -   5        $��CloseWindow��5      $
��Exit��6.   6Z   m� 5      -   5        $��Analyze��5      $��Analyze numeric columns��6*   6e   5      m� 5      $��Plotter��6   .@�5      m� -   5        $��
CreateHist��5      $��Create histogram��6-   6d   m� 5      -   5        $��Create2D��5      $��Create 2D graphic��6+   6a   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ CSVGrid��5      $��10,30,21.6,0q�6$   $��2��6:   $��0��6N      
IsReadOnly$
 True��6^   ��6p   �� 5      5      5      