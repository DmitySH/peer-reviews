   M S B A M L   `   `   `  ����J  FMainApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:MainApplication.Views.WindowsMainApplication.Views.Windows  5   	   6    0MainApplication.Views.Windows.RegistrationWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	   9local+clr-namespace:MainApplication.Views.Windows   5   	   -   5          � AllowsTransparency$
  True��5
   	   
������  � WindowStyle$
 None=�6<    � Title$ Registration��5   	   $	��670��6   $	��480��6+    � 
ResizeMode$ NoResize=�67    � WindowStartupLocation$ CenterScreen=�6M    � 	Resources 5         TB �� 
TargetType  RegisterTextBox �� BasedOn �� Property	 �� Value��%<  &	        7  �� 5   
   " ��6   8   .�5      ��  �@=�ErrorTemplate	 5      �� 5      .R�5      �� .+�5      �� �� 5      ��6+   �� ��5   "   �� 5   &   .R�5   *   �� 
���  ����64   �� *ErrorContent+6R   5   '   5      5      5      5      5      5      .��5       � .+�5!   
   � 
 � RowDefinitions
 5"      �� 5#      $��4* �6    �� 5$      $��7* �6    .+�5&      ��  �� CornerRadius$ 15��6   $��2��6'   ��5'      �� 5(      .��5)      �  � Color$ SkyBlue��6'    � Offset$	 0.1Q�67   � 5*      $ SeaGreen��6'   $ 1Q�68   5+      5-      �� 5/      $��0��6   $��Center=�6&   .+�50      �� $��5q�6   $$��../../Images/warehouseLogo.png��6#   $	��150��6K   6Z   �� 51      $��Registration��6   $��	Helvetica)�60   $��Light"�6G   $��30(�52      
���   �$��Center=�6=   53      �� 55      $��1��6   .+�56      ��  �� Orientation$ Horizontal =�6   .+�57      ��  � ToolTip$ Enter email��6    ^� 	Placement$ Bottom=�64    ^� InitialShowDelay$
 1000��6V   #��[  6}   ��6�   �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=�.  (MahApps.Metro.IconPacks.PackIconMaterial  58         Kind$ EmailOutline=�61   $��Center=�6E   $��30��6`   $��30��6l   59      �� 5:      $	��480��6   $��2��6%   
���   � J� Opacity$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5;      $ Horizontal =�6   .+�5<      �� #��[  6   $ Bottom=�6:   $
 1000��6\   $ Enter password��6�   $��	 10 0 0 0q�6�   ��5=      �� *
Password+$ PropertyChanged=�  5>      $ 
KeyOutline=�61   $��Center=�6C   $��30��6^   $��30��6j   5?      �� 5@      $	��480��6   $��2��6%   
���   �$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5B      $ Horizontal =�6   .+�5C      �� $ Bottom=�6   $
 1500��6@   $ Enter phone number��6g   #��[  6�   ��6�   �� *	TelNumber+$ PropertyChanged=�  5D      $ PhoneOutline=�61   $��Center=�6E   $��30��6`   $��30��6l   5E      �� 5F      $	��480��6   $��2��6%   
���   �$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5H      $ Horizontal =�6   .+�5I      �� $ Bottom=�6   $
 1500��6@   $ Enter address��6g   #��[  6   ��6�   �� *	Address+$ PropertyChanged=�  5J      $ HomeOutline=�61   $��Center=�6D   $��30��6_   $��30��6k   5K      �� 5L      $	��480��6   $��2��6%   
���   �$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5N      $ Horizontal =�6   .+�5O      �� $ Bottom=�6   $
 1500��6@   $ 
Enter name��6g   #��[  6|   ��6�   �� *Name+$ PropertyChanged=�  5P      $ AccountOutline=�61   $��Center=�6G   $��30��6b   $��30��6n   5Q      �� 5R      $	��480��6   $��2��6%   
���   �$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5T      $ Horizontal =�6   .+�5U      �� $ Bottom=�6   $
 1500��6@   $ Enter surname��6g   #��[  6   ��6�   �� *	SurName+$ PropertyChanged=�  5V      $ AccountBoxOutline=�61   $��Center=�6J   $��30��6e   $��30��6q   5W      �� 5X      $	��480��6   $��2��6%   
���   �$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5Z      $ 
Horizontal=�6   .+�5[      �� $ Bottom=�6   $
 1500��6@   $ Enter patronymic��6g   #��[  6�   ��6�   �� *
Patronymic+$ PropertyChanged=�  5\      $ AccountDetailsOutline=�61   $��Center=�6N   $��30��6i   $��30��6u   5]      �� 5^      $	��480��6   $��2��6%   
���   �$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5_      $ 
Horizontal=�6   .+�5`      �� $��10 10 50 10q�5b      $��Accept��62     StartupButton#��[ 6C   $��Bottom=�5c      $��70��68   $��30(�6D   $	��200��6R   ��5`      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window��5a   ]   �� *RegisterUserCommand+5c   R   �� 5d      $��10 10 10 10q�5f      $��Cancel��62   #��[ 6C   $��Bottom=�5g      $��70��68   $��30(�6D   $	��200��6R   ��5d      ��  �� *FindAncestor+
 Window��5e   ]   �� *CloseWindowCommand+5g   R   5h      5i      5j      5k      5l      