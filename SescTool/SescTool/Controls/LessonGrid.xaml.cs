using System.Windows.Controls;
using System.Windows.Media;
using ClassTimetable;

namespace SescTool.Controls
{
	public partial class LessonGrid
	{
		public LessonGrid()
		{
			InitializeComponent();
		}

		public SchoolDay Day
		{
			get { return _day; }
			set
			{
				_day = value;
				OnDayChanged();
			}
		}

		private void OnDayChanged()
		{
			for(var nles = 0; nles < 7; ++nles)
			{
				if(_day.Lessons[nles] == null)
				{
					var o = Lessons.Children[nles] as Border;
					if(o != null) o.Child = null;
					continue;
				}
				var gr = new Grid();
				if(_day.Lessons[nles].IsWholeClassLesson)
				{
					var tb = new TextBlock
					{
						Text = $"{_day.Lessons[nles].WholeClassLesson.Name} {_day.Lessons[nles].WholeClassLesson.Classroom}"
					};
					gr.Children.Add(tb);
					gr.Background = new SolidColorBrush(Colors.LightGreen);
				}
				else
				{
					gr.ColumnDefinitions.Add(new ColumnDefinition());
					gr.ColumnDefinitions.Add(new ColumnDefinition());
					gr.ColumnDefinitions.Add(new ColumnDefinition());
					var b1 = new Border();
					var tb1 = new TextBlock();
					if(_day.Lessons[nles].LessonsInGroups[0] != null)
						tb1.Text = $"{_day.Lessons[nles].LessonsInGroups[0].Name} {_day.Lessons[nles].LessonsInGroups[0].Classroom}";
					b1.Child = tb1;

					var b2 = new Border();
					var tb2 = new TextBlock();
					if(_day.Lessons[nles].LessonsInGroups[1] != null)
						tb2.Text = $"{_day.Lessons[nles].LessonsInGroups[1].Name} {_day.Lessons[nles].LessonsInGroups[1].Classroom}";
					b2.Child = tb2;

					var b3 = new Border();
					var tb3 = new TextBlock();
					if(_day.Lessons[nles].LessonsInGroups[2] != null)
						tb3.Text = $"{_day.Lessons[nles].LessonsInGroups[2].Name} {_day.Lessons[nles].LessonsInGroups[2].Classroom}";
					b3.Child = tb3;

					gr.Children.Add(b1);
					gr.Children.Add(b2);
					gr.Children.Add(b3);

					Grid.SetColumn(b1, 0);
					Grid.SetColumn(b2, 1);
					Grid.SetColumn(b3, 2);
					gr.Background = new SolidColorBrush(Colors.White);
				}
				var border = Lessons.Children[nles] as Border;
				if(border != null) border.Child = gr;
			}
		}

		private SchoolDay _day;
	}
}
