   M S B A M L   `   `   `  ����  Geldverleih.UI&     Geldverleih.UI.GeldEinzahlenView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  GeldEinzahlenView��5   	   $	��377��6#   $	��548��60   .��5      � .+�5   
   �� -   5        $��EinzahlbetragTextbox��5   K   $��23��6   $
��Left=�6   $��2,44,0,0q�69   $	��Top=�6g   $	��120��6   �� 5   
   -   5        $��EinzahlbetragLabel��5   h   $��Betrag zum einzahlen��6   $��28��6/   $
��Left=�6;   $��1,10,0,0q�6V   $	��Top=�6�   �� 5   
   -   5        $��EinzahlenButton��5   `   $��	Einzahlen��6   $��48��6%   $
��Left=�61   $��
160,19,0,0q�6L   $	��Top=�6w   $��95��6�   6�   &   System.Windows.Controls.DataGrid  5	   
   -   5        $%��BereitsEingezahlteVorgaengeGrid��5	   i      AutoGenerateColumns .  $	��222��6/   $
��Left=�6<   $��1,88,0,0q�6W   $	��Top=�6�   $	��526��6�    � 	Resources 5
      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%X   '        �� 5      "  6   .�5      ��  � �� �#5FBA276;   5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Datum��6D      MinWidth$	 100Q�6S   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� 
 �� Path$
 Datum�6S     5      $ Betrag��6E   $	 100Q�6U   	 6%   �� $
 Betrag�6U   5      �� 5   
   -   5        $��VorgangsStatusBar��5   N   $��30��6   $
��Left=�6    $��	1,308,0,0q�6;   ���	#FFA5C140$	��Top=�6~   $	��525��6�   .@�5      �� -   5        $��VorgangsNummerBarItem��5      $��Vorgangsnummer: ��69   6V   5      5      5      