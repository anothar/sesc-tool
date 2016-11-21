using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SESC.DataParser;

namespace SescTool
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		// Just for testing
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private async void CombоBoxOnChanged(object sender, SelectionChangedEventArgs e)
		{
			var comboBox = sender as ComboBox;
			var comboBoxItem = comboBox?.SelectedItem as String;
			if(comboBoxItem == null) return;
			var week =await TimetableParser.Instance.GetStaticSchoolWeekOfClass(comboBoxItem);
			MD.Day = week.Days[0];
			TD.Day = week.Days[1];
			WD.Day = week.Days[2];
			THD.Day = week.Days[3];
			FD.Day = week.Days[4];
			SD.Day = week.Days[5];
		}

		private void NavigationButtonsOnClick(object sender, RoutedEventArgs e)
		{
			DisableAll();
			var toggleButton = e.Source as ToggleButton;
			if(toggleButton == null) return;
			switch(toggleButton.Name)
			{
				case "MDB":
					Scroller.ScrollToVerticalOffset(Header.ActualHeight);
					break;
				case "TDB":
					Scroller.ScrollToVerticalOffset(Header.ActualHeight + MDSP.ActualHeight);
					break;
				case "WDB":
					Scroller.ScrollToVerticalOffset(Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight);
					break;
				case "THDB":
					Scroller.ScrollToVerticalOffset(Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight + WDSP.ActualHeight);
					break;
				case "FDB":
					Scroller.ScrollToVerticalOffset(Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight + WDSP.ActualHeight +
													THDSP.ActualHeight);
					break;
				case "SDB":
					Scroller.ScrollToVerticalOffset(Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight + WDSP.ActualHeight +
													THDSP.ActualHeight + FDSP.ActualHeight);
					break;
			}
		}

		private void ScrollerOnChanged(object sender, ScrollChangedEventArgs e)
		{
			var offset = Scroller.VerticalOffset;
			if(offset + delta >= Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight + WDSP.ActualHeight +
				THDSP.ActualHeight + FDSP.ActualHeight)
			{
				DisableAll();
				SDB.IsChecked = true;
			}
			else if(offset + delta >= Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight + WDSP.ActualHeight +
				THDSP.ActualHeight)
			{
				DisableAll();
				FDB.IsChecked = true;
			}
			else if(offset + delta >= Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight + WDSP.ActualHeight)
			{
				DisableAll();
				THDB.IsChecked = true;
			}
			else if(offset + delta >= Header.ActualHeight + MDSP.ActualHeight + TDSP.ActualHeight)
			{
				DisableAll();
				WDB.IsChecked = true;
			}
			else if(offset + delta >= Header.ActualHeight + MDSP.ActualHeight)
			{
				DisableAll();
				TDB.IsChecked = true;
			}
			else if(offset + delta >= 0)
			{
				DisableAll();
				MDB.IsChecked = true;
			}
		}

		private void DisableAll()
		{
			MDB.IsChecked = false;
			TDB.IsChecked = false;
			WDB.IsChecked = false;
			THDB.IsChecked = false;
			FDB.IsChecked = false;
			SDB.IsChecked = false;
		}
		private double delta = 40;
	}
}