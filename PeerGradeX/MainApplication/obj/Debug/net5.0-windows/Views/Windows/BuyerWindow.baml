   M S B A M L   `   `   `  ����J  FMainApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:MainApplication.Views.WindowsMainApplication.Views.Windows  5   	   ?$clr-namespace:MainApplication.ModelsMainApplication.Models  5   	   G(clr-namespace:MainApplication.ViewModelsMainApplication.ViewModels  5   	   /    )MainApplication.Views.Windows.BuyerWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:MainApplication.Views.Windows     5   	   5self$clr-namespace:MainApplication.Models     5   	   7vm(clr-namespace:MainApplication.ViewModels     5   	   X TXceed.Wpf.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5	   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
   	   $	��450��5   	   $	��900��6     � WindowStartupLocation$  CenterScreen=�6"    � Title 5   	   �� *
UserInfo+.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *CloseWindowCommand+6M   5      .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��_File��6   .@�5      m� $��
_Open file��6    m� Command 6/   �� *OpenFileCommand+5      m� 5      $��_About��6    6'   �� *ShowInfoCommand+m� 5      $��_Log out��6    6)   �� *LogOutCommand+ m� CommandParameter 6K   ��  �� RelativeSource �� *FindAncestor+	 �� AncestorType
	 Window5      � 5   
   
 � ColumnDefinitions
 5      �� 5      $
��1.6* �6#   �� 5      $
��2.1* �6#   �� 5       $
��2.5* �6#   .+�5"      �� $��0��6   $��	Warehouse��6'   .��5#      R� $��16(�6C   ��6   �� *RootsCollection+��5$      �� 5%       �� DataType$  MainApplication.Models.Catalog"  63    �� ItemsSource 6T   �� *
Catalogs+.R�5&      ��  �� Orientation$ 
Horizontal=�6)   .+�5'   "   �� $��Uniform=�6?   $��17��6Q   $��0 2 2 0q�6]   ��6(   �� *Src+6]   �� 5(   "   ��6,   �� *Title+5)      5*      ��5,      �� 5-       �� 
TargetType" Q�6    �5.      �� 5/   "    �� Property �@Q�
IsSelected �� Value 6?   �� *
IsSelected+�� 50   "    �@Q�
IsExpanded 6?   �� *
IsExpanded+ �� Mode$ TwoWay=�52      54      55      �� 57      $��1��6   ��6'   �� *SelectedFolder.Title+ �� FallbackValue$ Folder is not selected�� �� TargetNullValue$ Folder is not selected��.��5:      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��5<         
IsReadOnly$
 True��5=         GridLinesVisibility$ Vertical=�5>         VerticalGridLinesBrush
 �����   AlternatingRowBackground
 ��·���5:      �� *SelectedFolder.Items+ �� SelectedItem 5;      �� *SelectedItem+ J� InputBindings 5A      d� 5B       d� MouseAction$ LeftDoubleClicke�6'    �� Command 6E   �� *AddItemCommand+6j      Columns 5D      0  *System.Windows.Controls.DataGridTextColumn  5E      ,  &System.Windows.Controls.DataGridColumn   Header$ Product name��6-   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6C   �� *Name+  5F      $ Article��6-    6>   �� *	Article+  5G      $ Quantity��6-    6?   �� *
Quantity+  5H      $ Price��6-    6<   �� *Price+4
  .System.Windows.Controls.DataGridTemplateColumn
  5I      $ To buy��61     
  CellTemplate  5J      �� 5K   "   .R�5L   &   %  Xceed.Wpf.Toolkit.IntegerUpDown  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]], System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]!   Minimum! 069   "   DefaultValue" 06E   #   Maximum# 6V   �� *
Quantity+$   Value$ 5M   9   �� *ToBuy+% �� UpdateSourceTrigger$% PropertyChanged=�5N   #   5P      5R      5S      � 5U      $��2��6   & � RowDefinitions& 5V      �� 5W      $��2* �6$   �� 5X      $
��1.3* �6$   .+�5Z      �� $��0��6   ��5[      �� 5\      $ 
Horizontal=�6%   .+�5]      �� $��	Order №��6(   $��0 0 5  0q�67   �� 5^      $��0 0 5  0q�6M   ��6(   �� *CurrentOrder.Number+6M   5_      .��5a        $ False��5c      $
 True��5d      $ Vertical=�5e      
 �����
 � �����5a      �� *CurrentOrder.Items+ 5b      �� *SelectedItem+ 5h        5i      $ Product name��61    6G   �� *Name+  5j      $ Quantity��61    6C   �� *
Quantity+  5k      $ Total price��61    6F   �� *
TotalPrice+  5l       5m   "   �� 5n   &   .+�5o   *   �� $��Create order��61   ��6H   �� * DataContext.CreateOrderCommand+ �� 
	 Window6�   5p   '   5r      5t      5u      �� 5v      $��1��6   $��Your orders��6(   .��5w      �� ' �� DisplayMemberPath$' 	ShortInfo��6A   ��6   �� *
UserOrders+ 6_   �� *SelectedOrder+( � ContextMenu( 5x      �� 5y      .@�5z   "   m� $��Show details��6+    6A   �� *ShowOrderCommand+�� 5{   "   m� 5|   "   $��Pay for order��6+    6B   �� *PayForOrderCommand+5}      5      5�      5�      5�      5�      5�      