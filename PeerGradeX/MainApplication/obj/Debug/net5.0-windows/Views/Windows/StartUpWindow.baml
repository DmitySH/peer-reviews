   M S B A M L   `   `   `  ����J  FMainApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:MainApplication.Views.WindowsMainApplication.Views.Windows  5   	   G(clr-namespace:MainApplication.ViewModelsMainApplication.ViewModels  5   	   1    +MainApplication.Views.Windows.StartUpWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:MainApplication.Views.Windows     5   	   ?
viewModels(clr-namespace:MainApplication.ViewModels     5   	   -   5          � Title$  Welcome��5	   	   $	��420��6?   $	��750��6L    � WindowStyle$
 None=�6X    � 
ResizeMode$ NoResize=�6k    � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5
       
  seller �� ImageSource 	 buyer��%u   &	        &	 1     �� 5   
   $ ../../Images/seller.png��6$   �� 5   
   $ ../../Images/buyer.png��6#   5       � DataContext 5      7  1MainApplication.ViewModels.StartUpWindowViewModel  5   
   .��5      � .+�5   
   � $��0��6   #��[  6   .+�5      �� $��10 10 10 10q�5   �   $��Login as seller��6�     StartupButton#��[ 6�   $	��Top=�6�   $	��120��6�   $��48(�6�   ��5      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window��5   ]   �� *LoginSellerCommand+6�   �� 5      $��10 10 10 10q�6?   $��Become a seller��6T   #��[ 6n   $��Bottom=�6�   $	��120��6�   $��46(�6�   ��6   �� *RegisterSellerCommand+6�   5      � 5   
   $��1��6   #��[ 6   .+�5      �� $��10 10 10 10q�5   �   $��Login as buyer��6�   $	��120��6�   $	��Top=�6�   #��[ 6�   $��48(�6�   ��5      ��  �� *FindAncestor+
 Window��5   ]   �� *LoginBuyerCommand+6�   �� 5      $��10 10 10 10q�6>   $��Become a buyer��6S   #��[ 6l   $��Bottom=�6�   $	��120��6�   $��46(�6�   ��6   �� *RegisterBuyerCommand+6�   5      �� 5   
   $��1��5   9   $��25��6I   $��25��6U   $	��Top=�6`   $��Right=�6x   ��5      ��  �� *FindAncestor+
 Window��5      �� *CloseWindowCommand+��5      �� 5      	 �� 
TargetType"	 ��6"   .R�5       �� 
 ��Name$
 Bd��6   
������ ._�5!      F�  F�Name$ Pt��6   =����        �@ @ � 5  5 �@  5 � 5 @ �@B @B 	 �� Stretch$ Uniform=�5"      
���   �$��2��6@   5#       �� Triggers 5$      P� 5%       P� Property �@J�IsMouseOver P� Value$
 True��69   . �5&      ��  �� 
TargetName$ Bd��6%    �� Property � �� �� Value
 �  ��6K   �� 5'      $ Pt��6%    � ��
 �����6G   5(      5*      5,       � ColumnDefinitions 5-   
   �� 5.      $��1* �6   �� 5/      $��1* �6   51      52      