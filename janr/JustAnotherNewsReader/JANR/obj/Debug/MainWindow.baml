   M S B A M L   `   `   `  ����  JANR    JANR.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$7  1Just Another News Reader 简易新闻发布系统��5   	   $	��350��63   $	��525��6@    � WindowStartupLocation$ Manual=�6L    � WindowState$ 	Maximized=�6k    J� 	AllowDrop . .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5
   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $��Center=�6B   .+�5      �� $��设备:��6   �� 5      -   5         ��Name$ ComboBoxPresetDevice��5       �� SelectedIndex$ -1��69   $	��150��6L   5      5      �� 5      $��1��6   $ 
Horizontal=�6)   $��Center=�6B   .+�5      �� $��屏幕分辨率:��6   �� 5      -   5        	 ��Name$	 TextBoxDeviceSize��5      $��60��65   6A   �� 5      $��屏幕缩放比例:��6   �� 5      -   5        $	 TextBoxDevicePixelRatio��5      $��60��6;   6F   �� 5      $��内容边界:��6   �� 5      -   5        $	 TextBoxDeviceMargin��5      $��60��67   6C   5      �� 5      -   5        
 ��Name$
 
PanelAdmin��5      $��3��6-   $ 
Horizontal=�6=   $��Center=�6V   .+�5       �� -   5         ��Name$ ButtonAdminUsers��5       $��用户管理��63   6B   �� 5!      -   5        $ ButtonAdminCatagories��5!      $��分类管理��68   6G   �� 5"      -	   5        $ ButtonAdminNews��5"      $��新闻管理��62   6A   5#      �� 5$      -
   5        $
 PanelBeforeLogin��5$      $��4��63   $ 
Horizontal=�6C   $��Center=�6\   .+�5%      �� $��
用户名:��6   �� 5&      -   5        $	 TextBoxUserName��5&      $��Admin��63   $��60��6@   �� 5'      $��密码:��6   �� 5(      -   5        $	 TextBoxPassword��5(      $��admin��63   $��60��6@   �� 5)      -   5        $ ButtonLogin��5)      $��登录��6.   6;   5*      �� 5+      -   5        $
 PanelAfterLogin��5+      $y�	Collapsed=�62   $��4��6I   $ 
Horizontal=�6Y   $��Center=�6r   .+�5,      �� $��	Welcome, ��6   �� 5-      -   5         ��Name$ TextBlockUserName��5-      $�� ��67   5.      5/      � 50   
   $��1��6    51      �� 52      $��2* �6#   �� 53      $��2* �6#   �� 54      $
��Auto �6#   �� 55      $��* �6#   .+�57      � $��0��6    58      �� 59      �� 5:      $
��Auto �6$   �� 5;      $
��Auto �6$   �� 5<      .+�5>      �� $��新闻预览界面��6   $��28(�6*   $��1��68    �� TextAlignment$ Center=�6E   �� 5?      -   5         ��Name$ BorderWebBrowserDevice��5?      $��2��69   $	��568��6F   $	��320��6S   
���   �._�5@      (  "System.Windows.Controls.WebBrowser  -   5          Name$ WebBrowserDevice��5@      6g   5A      5B      � 5C      $��1��6    5D      �� 5E      �� 5F      $
��Auto �6$   �� 5G      $
��Auto �6$   �� 5H      .+�5J      �� $��新闻编辑界面��6   $��28(�6*   $��1��68   $ Center=�6E   �� 5K      -   5        $ BorderEditorDevice��5K      $��2��65   ��6B   ��  �� ElementName$ BorderWebBrowserDevice�� �� Path$ Height���6�   �� $ BorderWebBrowserDevice��$ Width���6�   �� $ BorderWebBrowserDevice��$ 
Background�._�5L      �� -   5        $	 EditorDevice��5L      $��16(�64    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6W   ��6k   �� $ WebBrowserDevice��$ Margin�6�   5M      5N      � 5O      $��3��6    5P      �� 5Q      �� 5R      .+�5T      �� $��2��6   .+�5U      �� $��10q�6!   .+�5V      �� $��新闻来源:��6    �� 5W      -   5        $	 TextBoxNewsSourceUrl��5W      �� 5X      -   5        $ ButtonFetchSource��5X   !   $��分析��6<   6I   5Y      �� 5Z      $��10q�6!   .+�5[      �� $��新闻标题:��6    �� 5\      -   5        $	 TextBoxNewsTitle��5\      �� 5]      $��发布时间:��6    �� 5^      -   5        $	 TextBoxNewsOriginalTime��5^      �� 5_      $��新闻摘要:��6    �� 5`      -   5        $	 TextBoxNewsIntro��5`   "    . $
 Wrap=�6Q   �� 5a      $��新闻分类:��6$   �� 5b      -   5        $
 StackPanelCatagory��5b   %   $ 
Horizontal=�6A   6\   �� 5c      -   5        $ ButtonSubmit��5c   !   $��0,10,0,0q�67   $��提交��6I   6V   5d      �� 5e      -   5        $ ButtonClearFormat��5e      $��10q�68   $��清除正文格式��6D   6U   �� 5f      -   5        $ ButtonUploadImage��5f      $��10q�68   $��插入图片��6D   6S   �� 5g      -   5        $ TextBlockDescription��5g       $
��Wrap=�6?   $��10q�6S   6a   5h      5i      5j      5l      5m      