   M S B A M L   `   `   `  ����  Geldverleih.UI    Geldverleih.UI.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $
��This��5   -     � Title$  Geldverleih��5   	   $	��516��6   $	��846��6*   .��5      ]� .+�5   
   �� -   5        $��
statusBar1��5   O   $��Bottom=�6   ���	#FFA5C140$��23��6C   .@�5      �� $��Konto: Andreas Tarakanov��6   5	      �� 5
      $Y��S                            +++ Hier ist ein Ticker mit aktuellen Informationen +++��6   5      �� 5      $=��7                          Datenbank: Andy-Pc\SqlExpress��6   5      5      c� 5   
   -   5        $��toolBar1��5   '   $	��Top=�6   $��58��67   .@�5      �� -   5        $��AddKundeButton��5      .��5      �� $��icons/add_user.png��6   67   5      �� 5      -   5        $��EditKundeButton��5      .��5      �� $��icons/pencil.png��6   65   5      �� 5      $	��520��6   ��5      �� 5      5      �� 5      $	��150��6   $��30��6"    �� SelectionBrush �	#FF5DBA24���	#FFEBEBEB$��Right=�6`   .��6w    � 	Resources 5       �� 
TargetType �� Property �� Value��%_   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 15��69   5       5!      .��6%   5"      5#      �� 5$   
   .@�5%      �� -   5        $��KundenuebersichtTab��5%   /   $��Kundenübersicht��6   .��5&      ]� ��. .+�5'      &   System.Windows.Controls.DataGrid  -   5        $��KundenDataGrid��5'   h      AutoGenerateColumns .  $
��Left=�6;   $��-3,2,0,0q�6V   $	��Top=�6~    5(      =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%X   '        �� 5)      "  6$   .�5*   "   ��  � �� �#5FBA276G   5+      5,         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ Vorname��6R   	   MinWidth$		 100Q�6c   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 61   ��  �� Path$ Vorname�6c     5/      $
 Name��6O   $		 100Q�6]   
 61   �� $
 Name�6]     50      $ Adresse��6R   $		 120Q�6c   
 61   �� $ Adresse�6c     51      $	 PLZ��6N   $	 20Q�6[   
 61   �� $	 PLZ�6[     52      $ Wohnort��6R   $	 80Q�6c   
 61   �� $ Wohnort�6c   54      55      56      �� 57      -   5        $��StatistikTab��57      $��	Stastiken��6*   .��58      � -	   5        $��grid1��58      $��0,0,0,0q�6$   .+�59      �� -
   5        $��EinnahmenLabel��59   l   $��
Einnahmen:��6   $��28��61   $
��Left=�6=   $��
13,117,0,0q�6X   $	��Top=�6�   �� 5:      -   5        $��EinnahmenTextblock��5:   \   $��23��6    $
��Left=�6,   $��148,122,0,0q�6G   $	��0.0��6v   $	��Top=�6�   �� 5;      -   5        $��VerliehenesGeldLabel��5;   s   $��Verliehenes Geld:��6   $��28��68   $
��Left=�6D   $��
13,158,0,0q�6_   $	��Top=�6�   �� 5<      -   5        $��VerliehenesGeldTextblock��5<   \   $��23��6    $
��Left=�6,   $��148,163,0,0q�6G   $	��0.0��6|   $	��Top=�6�   �� 5=      -   5        $��ZinsenEinnahmenLabel��5=   s   $��Zinsen einnahmen:��6   $��28��68   $
��Left=�6D   $��
13,196,0,0q�6_   $	��Top=�6�   �� 5>      -   5        $��ZinsenEinnahmenTextblock��5>   \   $��23��6    $
��Left=�6,   $��148,201,0,0q�6G   $	��0.0��6|   $	��Top=�6�   �� 5?      -   5        $ ��EinnahmenAnzeigenNachLabel��5?   m   $��Einnahmen von��6   $��28��64   $
��Left=�6@   $��13,9,0,0q�6[   $	��Top=�6�   $	��151��6�   (  "System.Windows.Controls.DatePicker  5@      -   5        $��EinnahmeVonDatePicker��5@   [   $��25��6!   $
��Left=�6-   $��	13,43,0,0q�6H   $	��Top=�6x   $	��115��6�   �� 5A      -   5        $��label1��5A   n   $��Einnahmen bis��6   $��28��64   $
��Left=�6@   $��	170,9,0,0q�6[   $	��Top=�6|   $	��151��6�     5B      -   5        $��EinnahmenBisDatePicker��5B   \   $��25��6!   $
��Left=�6-   $��
170,43,0,0q�6H   $	��Top=�6z   $	��115��6�   �� 5C      -   5        $��AuswertenButton��5C   j   $��Anzeigen��6   $��37��60   $
��Left=�6<   $��	13,74,0,0q�6W   $	��Top=�6�   $	��115��6�   6�   5D      5E      5F      5G      5H      