   M S B A M L   `   `   `  ����J  FMainApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:MainApplication.Views.WindowsMainApplication.Views.Windows  5   	   /    )MainApplication.Views.Windows.LoginWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:MainApplication.Views.Windows     5   	   c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	   -   5          � Title$  Login��5	   	    � AllowsTransparency$
 True��6   
������  � WindowStyle$
 None=�6J   $	��670��6]   $	��480��6j    � 
ResizeMode$ NoResize=�6v    � WindowStartupLocation$ CenterScreen=�6�   .��5
      � .+�5   
   �  � RowDefinitions 5      �� 5      $	��13* �6    �� 5      $	��14* �6    .+�5      ��  �� CornerRadius$ 15��6   $��2��6'   ��5      �� 5      .��5      �  � Color$
 Aqua��6'    � Offset$
 -0.1Q�64   � 5      $ HotPink��6'   $ 1Q�67   5      5      �� 5      $
��0 20q�6   $��0��6'   $��Center=�64   .+�5      �� $��15q�6   $��../../Images/manLogo.png��6$   $	��200��6F   6U   �� 5      $��Login��6   $��	Helvetica)�6)   $��Light"�6@   $��45(�5      
���   �$��Center=�6=   5      �� 5      $��1��6   .+�5       �� 	 �� Orientation$	 
Horizontal=�6   $��
 0 20 0 0 q�66   .+�5!      �� $��10 4 4 4q�6   $��Bottom=�60   $	��415��6K   $��40��6W   
 � ToolTip$
 Enter email��6c    ^� 	Placement$ Bottom=�6y    ^� InitialShowDelay$
 1000��6�      RegisterTextBox#��[  6�   $��40(�6'  ��6�   �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=�6'  .  (MahApps.Metro.IconPacks.PackIconMaterial  5"         Kind$ EmailOutline=�61   $��Center=�6E   $��50��6`   $��50��6l   5#      �� 5$      $	��480��6   $��2��6%   
���   � J� Opacity$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5%      $��	 0 40 0 0q�6   $	 Horizontal =�60   .+�5&      �� $	��415��6   $��40��6*   #��[  66   $ Bottom=�6_   $
 1000��6�   $
 Enter password��6�   $��	 10 4 4 4q�6�   $��Bottom=�5'      $��40(�6y   ��6:   �� *
Password+$ PropertyChanged=�6y     5(      $ 
KeyOutline=�61   $��Center=�6C   $��50��6^   $��50��6j   5)      �� 5*      $	��480��6   $��2��6%   
���   �$	 0.5Q�6C   $��0 0 0 10q�6Q   6e   �� 5+      $	 
Horizontal=�6   $��0 30 0 0q�66   .+�5,      �� $��10 10 50 10q�5.      $��Accept��62     StartupButton#��[ 6C   $��Bottom=�5/      $��70��68   $��30(�6D   $	��200��6R   ��5,      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window��5-   ]   �� *LoginCommand+5/   R   �� 50      $��10 10 10 10q�52      $��Cancel��62   #��[ 6C   $��Bottom=�53      $��70��68   $��30(�6D   $	��200��6R   ��50      ��  �� *FindAncestor+
 Window��51   ]   �� *CloseWindowCommand+53   R   54      55      56      57      58      