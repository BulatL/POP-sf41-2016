   M S B A M L   `   `   `  ����M  IPOP-sf-41_2016 GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:POP_sf_41_2016_GUIPOP_sf_41_2016_GUI  5   	   *    $POP_sf_41_2016_GUI.UI.NamestajWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:POP_sf_41_2016_GUI     5   	     � Title$  NamestajWindow��5   	   $��568.664��6    $��464.916��61    � 	Resources 5	         ValidationError+  %POP_sf_41_2016_GUI.ValidatorConverter  	converter��%�   &	        &	 �     �� 5
   
   .R�5      ]� .+�5      �� 
���  ��$��16(�6-   $��Right=�6;   $��Center=�6R   !!!6t   �� 5      
���  ��$��1q�6+   ._�5      �� 5      5      5        5   
   6#   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��104* �6   �� 5      $
��353* �6    � RowDefinitions 5   
   �� 5      $	��82* �6   �� 5      $	��82* �6   �� 5      $	��82* �6   �� 5      $	��82* �6   �� 5      $
��114* �6   �� 5      $	��98* �6   .+�5!   
   �� $��Naziv��6   $
��Left=�6    $��	15,12,0,0q�6;   $	��Top=�6N   $��81��6f   $��39��6q   $��18(�6}   �� 5"   
   $��Sifra��6   $
��Left=�6    $��	15,12,0,0q�6;   $��1��6N   $	��Top=�6[   $��81��6s   $��39��6~   $��18(�6�   �� 5#   
   $
��Cena��6   $
��Left=�6   $��	15,12,0,0q�6:   $��2��6M   $	��Top=�6Z   $��81��6r   $��39��6}   $��18(�6�   �� 5$   
   $��Kolicina��6   $
��Left=�6#   $��	15,12,0,0q�6>   $��3��6Q   $	��Top=�6^   $��81��6v   $��39��6�   $��18(�6�   �� 5%   
   $��Tip namestaja��6   $
��Left=�6)   $��	10,10,0,0q�6D   $��4��6W   $	��Top=�6d   $��91��6|   $��60��6�   $��WrapWithOverflow=�6�   $��18(�6�   �� 5'   
   -   5        $��tbNaziv��5'       =� ErrorTemplate# [  6!   $��1��6i   $
��Left=�6y   $��34��6�   $��	10,12,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��213��6�   $��16(�6�   .��6�   ��5(      �� 5)       �� Path$ Naziv�6    �� UpdateSourceTrigger$ PropertyChanged=�6'    �� ValidationRules 5*      )  #POP_sf_41_2016_GUI.ValidationString  5+      5-      .��5.      5/      �� 50   
   $��1��6   $��	10,51,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�52      $	��333��67   $��23��6C   $	��Top=�6O   ��51      �� 	 �� ElementName$	 tbNaziv��$ (Validation.Errors)�
 �� 	Converter#
 [ 52   O   �� 54   
   -   5        $��tbSifra��54      # [  6!   $��1��6j   $
��Left=�6z   $��34��6�   $��	10,13,0,0q�6�   $��1��6�   $
 Wrap=�6�   $	��Top=�6�   $	��213��6�   $��16(�6�   .��6  ��55      �� 56      $ Sifra�6   $ PropertyChanged=�6'    57        58      5:      .��5;      5<      �� 5=   
   $��1��6   $��	10,52,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�5?      $	��333��67   $��23��6C   $	��Top=�6O   $��1��6g   ��5>      �� $	 tbSifra��$ (Validation.Errors)�#
 [ 5?   g   �� 5A   
   -   5        $��tbCena��5A      # [  6    $��1��6i   $
��Left=�6y   $��34��6�   $��	10,12,0,0q�6�   $��2��6�   $
 Wrap=�6�   $	��Top=�6�   $	��213��6�   $��16(�6�   .��6  ��5B      �� 5C      $ JedinicnaCena�6   $ 	LostFocus=�6/    5D      )  #POP_sf_41_2016_GUI.ValidationDouble  5E      5G      .��5H      5I      �� 5J   
   $��1��6   $��	10,52,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�5L      $	��333��67   $��23��6C   $	��Top=�6O   $��2��6g   ��5K      �� $	 tbCena��$ (Validation.Errors)�#
 [ 5L   g   �� 5N   
   -   5        $��
tbKolicina��5N      # [  6$   $��1��6m   $
��Left=�6}   $��34��6�   $��	10,12,0,0q�6�   $��3��6�   $
 Wrap=�6�   $	��Top=�6�   $	��213��6�   $��16(�6�   .��6
  ��5O      �� 5P      $ KolicinaUMagacinu�6   $ PropertyChanged=�63    5Q        5R      5T      .��5U      5V      �� 5W   
   $��1��6   $��	10,52,0,0q�6$   
���  ��$
��Bold"�6H   $
��Left=�5Y      $	��333��67   $��23��6C   $	��Top=�6O   $��3��6g   ��5X      �� $	 
tbKolicina��$ (Validation.Errors)�#
 [ 5Y   g   �� 5[   
   -   5        $��cbTipNamestaja��5[   >   $��1��6T   $
��Left=�6d   $��	21,23,0,0q�6   $��4��6�   $	��Top=�6�   $	��178��6�   $��34��6�   $��16(�6�    �� SelectedItem 6   �� $ TipNamestaja�6�   �� 5\   
   -   5        $��
bnOdustani��5\   $   $��Odustani��6   $��1��66   $
��Left=�6F   $��
203,23,0,0q�6a   $��5��6u   $	��Top=�6�   $��76��6�   $��36��6�   $��16(�6�   �� 5]   
   -   5        $��
btnPotvrdi��5]   #   $��Potvrdi��6   $��1��65   $
��Left=�6E   $��	77,23,0,0q�6`   $��5��6s   $	��Top=�6�   $��76��6�   $��36��6�   $��16(�6�   ��5^      �� 5_       �� 
TargetType" ��6   .�5`      ��  �� Property � {� �� Value .  62    �� Triggers 5a      ^� 5b       ^� 
Conditions 5c      �� 5d   "    �� Binding 6,   �� $ (Validation.HasError)�$	 tbNaziv�� �� Value	 false6p   �� 5e   "    6,   �� $ (Validation.HasError)�$	 tbSifra��	 false6p   �� 5f   "    6,   �� $ (Validation.HasError)�$	 tbCena��	 false6o   �� 5g   "    6,   �� $ (Validation.HasError)�$	 
tbKolicina��	 false6s   .0�5i      ��  � {� . 6:   5j      5l      5n      5p      5q      