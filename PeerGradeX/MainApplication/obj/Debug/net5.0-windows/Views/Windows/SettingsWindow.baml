   M S B A M L   `   `   `  ����J  FMainApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MainApplication.ViewModelsMainApplication.ViewModels  5   	   M+clr-namespace:MainApplication.Views.WindowsMainApplication.Views.Windows  5   	   2    ,MainApplication.Views.Windows.SettingsWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7vm(clr-namespace:MainApplication.ViewModels     5   	   =local+clr-namespace:MainApplication.Views.Windows     5   	     � Title$  Settings��5	   	   $	��270��6   $	��350��6'    � WindowStartupLocation$ CenterScreen=�63    � DataContext 5
      8  2MainApplication.ViewModels.SettingsWindowViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��55px �6   �� 5      $
��55px �6   �� 5      $
��55px �6   �� 5      $
��75px �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��130px �6   �� 5      $��130px �6   .+�5   
   �� $��0��6   $��0��6%   $��3q�62   .+�5      �� $��Number of catalogs��6   �� 5      $
��Left=�6   $	��120��60   $��18��6<    � SmallChange$ 1Q�6H    � Minimum$ 1Q�6X    � Maximum$ 20Q�6d    � Value 6q   �� *CatalogNumber+5      �� 5   
   $��0��6   $��1��6%   $��3q�62   .+�5      �� $��Number of folders��6   �� 5      $
��Left=�6   $	��120��60   $��18��6<   $ 1Q�6H   $ 1Q�6X   $ 30Q�6d    6q   �� *FolderNumber+5       �� 5!   
   $��0��6   $��2��6%   $��3q�62   .+�5"      �� $��Number of items��6   �� 5#      $
��Left=�6   $	��120��60   $��18��6<   $ 1Q�6H   $ 1Q�6X   $	 150Q�6d    6r   �� *
ItemNumber+5$      �� 5%   
   $��0��6   $��3��6%   $��3q�62   .+�5&      �� $��4 10 5 8q�6   $��Min price for report��6+   �� 5'      $
��Left=�6   $	��120��60   $��18��6<   $ 1Q�6H   $ 1Q�6X   $ 30000Q�6d    6t   �� *
MinPayed+5(      �� 5)   
   $��1��6   $��0��6"   $��Center=�6N   	 �� 
IsReadOnly$
	 True��6i   ��6/   �� *CatalogNumber+6i   �� 5*   
   $��1��6   $��2��6"   $��Center=�6K   $
	 True��6f   ��6/   �� *
ItemNumber+6f   �� 5+   
   $��1��6   $��1��6"   $��Center=�6M   $
	 True��6h   ��6/   �� *FolderNumber+6h   �� 5,   
   $��1��6   $��3��6"   $��Center=�6I   $
	 True��6d   ��6/   �� *
MinPayed+6d   5-      5.      