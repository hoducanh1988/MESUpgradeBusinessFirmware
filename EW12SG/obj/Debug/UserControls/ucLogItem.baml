   M S B A M L   `   `   `  ����A  =EW12SG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:EW12SG.UserControlsEW12SG.UserControls  5      #    EW12SG.UserControls.ucLogItem   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:EW12SG.UserControls     5        � 	Resources  5
       �� 
TargetType   label_title �� Property �� Value  label_content �� Triggers �� Binding �� RelativeSource �� Mode �� Path	 �� Value��%�  &	        &	 �    �� 5   
   " ��6   .�5      ��  � ��$ 22��6'   �� 5       � �� �#7777776,   �� 5       � ��$ 0.7,0,0,0.7q�60   �� 5       � ��$ 13(�6)   �� 5       � ��$ Normal"�6+   �� 5       � ��$ 0,0,0,0q�6'   �� 5       � ��$	 210��6&   �� 5       � ��$
 Left=�64   �� 5       � ��$ Center=�62   �� 5       � ��$ 5,0,0,0q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ 22��6'   �� 5       � �� �#7777776,   �� 5       � ��$ 0.7,0,0.7,0.7q�60   �� 5       � ��$ 13(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 	210,0,0,0q�6'   �� 5       � ��$ Center=�62   �� 5        � ��$ 5,0,0,0q�6(   �� 5!       � ��
 �   �6+    5"      �� 5#       6   ��  �� $
 Self=�$ Content�
	 Passed6j   .a�5$      ��  � ��
 � � �63   5%      �� 5&       6   ��  �� $
 Self=�$ Content�
	 Failed6j   .a�5'      ��  � ��
 �  ��63   5(      �� 5)       6   ��  �� $
 Self=�$ Content�	 
Waiting...6j   .a�5*      ��  � ��
 � ���63   5+      5-      5/      .��51      �� 
�������$��5,5,5,5q�6$   .+�52   
   � .+�53      �� #��[  6   $��0.7,0.7,0,0.7q�69   $"��check mac and firmware basic��6Y   �� 54      #��[ 6   $��0.7,0.7,0.7,0.7q�6<   ��6^   �� $ checkMacAndFwBasic�
 �� UpdateSourceTrigger$
 PropertyChanged=�55      � 56   
   .+�57      �� #��[  6   $%��transfer file and check md5 sum��69   �� 58      #��[ 6   ��6;   �� $ tranFileAndCheckMd5Sum�$
 PropertyChanged=�59      � 5:   
   .+�5;      �� #��[  6   $��upgrade business firmware��69   �� 5<      #��[ 6   ��6;   �� $ upgradeBusinessFw�$
 PropertyChanged=�5=      5>      5@      