   M S B A M L   `   `   `  ����M  IPOP-sf-41_2016 GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:POP_sf_41_2016_GUIPOP_sf_41_2016_GUI  5   	   .    (POP_sf_41_2016_GUI.UI.TipNamestajaWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:POP_sf_41_2016_GUI     5   	     � Title$  Tip Namestaja��5   	   $��156.895��6   $��321.272��60    � 	Resources 5	         ValidationError+  %POP_sf_41_2016_GUI.ValidatorConverter  	converter��%�   &	        &	 �     �� 5
   
   .R�5      ]� .+�5      �� 
���  ��$��16(�6-   $��Right=�6;   $��Center=�6R   !!!6t   �� 5      
���  ��$��1q�6+   ._�5      �� 5      5      5        5   
   6#   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��79* �6   �� 5      $	��48* �6    � ColumnDefinitions 5   
   �� 5      $	��78* �6   �� 5      $
��181* �6   .+�5   
   �� $��Naziv��6   $
��Left=�6    $��	10,12,0,0q�6;   $	��Top=�6N   $��30��6f   $��60��6r   $��14(�6}   �� 5   
   -   5        $��Nazad��5      $��1��6!   $
��Left=�61   $��	136,6,0,0q�6L   $��1��6_   $	��Top=�6l   $��30��6�   $��60��6�    J� RenderTransformOrigin$ 	0.633,0.5&�6�   6�   �� 5   
   -   5        $��
btnPotvrdi��5      $��Potvrdi��6#   $��1��65   $
��Left=�6E   $��38,6,0,0q�6`   $	��Top=�6r   $��60��6�   $��1��6�   $��30��6�   ��5       �� 5!       �� 
TargetType" ��6   .�5"      ��  �� Property � {� �� Value .  62    �� Triggers 5#      ^� 5$      	 ^� 
Conditions	 5%      �� 5&   "   
 �� Binding
 6,   ��  �� Path$ (Validation.HasError)� �� ElementName$ tbNaziv�� �� Value	 false6p   .0�5(      ��  � {� . 6:   5)      5+      5-      �� 5.   
   -   5        $��tbNaziv��5.   j    =� ErrorTemplate# [  6   $��1��6Z   $
��Left=�6y   $��29��6�   $��7,12,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��189��6�   $��14(�6�   .��6�   ��5/      �� 50      $ Naziv�6    �� UpdateSourceTrigger$ PropertyChanged=�6'    �� ValidationRules 51      )  #POP_sf_41_2016_GUI.ValidationString  52      54      .��55      56      �� 57   
   $��1��6   $��7,46,0,0q�6$   
���  ��$
��Bold"�6G   $
��Left=�59      $	��202��67   $��23��6C   $	��Top=�6O   ��58      �� $ tbNaziv��$ (Validation.Errors)� �� 	Converter# [ 59   O   5:      5;      