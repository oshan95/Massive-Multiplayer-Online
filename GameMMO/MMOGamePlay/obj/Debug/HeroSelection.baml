   M S B A M L   `   `   `  ����F  BMMOGamePlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MMOGamePlayMMOGamePlay  5   	       MMOGamePlay.HeroSelection   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MMOGamePlay     5   	     � Title$  HeroSelection��5   	   $	��450��6   $	��800��6,   .��5	      � .+�5
   
   �� -   5         ��Name$ lblHeroList��5
      $��SELECT HERO��6%   $��Center=�6;   $��61��6X   $��0,33,0,0q�6d   $	��Top=�6v   $	��238��6�   ���	#FF14F1DD$��36(�6�   $
��Bold"�6�   ��6�   Z� 6�   �� 5   
   -   5         ��Name$ 	listHeros��5      $��Center=�6&   $	��231��6C   $��	0,122,0,0q�6P   $	��Top=�6c   $	��454��6{   
���   �$��16(�6�   $
��Bold"�6�    � 	Resources 5       �� 
TargetType �� Property �� Value��%T   '��       �� 5      " ��6   .�5      ��  � ��$
 Left=�6C   5      5      ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      � 	 � Color$	 Black��6#   
 � Offset$
 0Q�61   � 5      $	 	#FF69EAEA��6#   $
 1Q�65   5       �� View 5      �� 5      .P�5      �� $��	Hero Name��6%    �� Width$	 250��68   �� 5      $��
SelectHero��6%    �� CellTemplate 5      �� 5      .R�5   "   �� -   5        $��Select��5   )   6:   5      5!      5"      5%      5&      ��5'      �� 5(   
    �� ImageSource$ /images/bg.jpg��6    j� Stretch$ UniformToFill=�62   5*      