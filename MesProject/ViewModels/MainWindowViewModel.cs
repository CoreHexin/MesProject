namespace MesProject.ViewModels
{
    public class MainWindowViewModel : NotificationObject
    {
		private string _timeStr = string.Empty;

		public string TimeStr
		{
			get { return _timeStr; }
			set 
			{ 
				_timeStr = value;
				RaisePropertyChanged();
			}
		}

		private string _dateStr = string.Empty;

		public string DateStr
		{
			get { return _dateStr; }
			set 
			{ 
				_dateStr = value; 
				RaisePropertyChanged();
			}
		}

		private string _weekStr = string.Empty;

		public string WeekStr
		{
			get { return _weekStr; }
			set 
			{ 
				_weekStr = value;
				RaisePropertyChanged();
			}
		}


		private string _machineCount;

        // 机台总数
        public string MachineCount
		{
			get { return _machineCount; }
			set 
			{ 
				_machineCount = value;
				RaisePropertyChanged();
			}
		}


		private string _productCount;

		// 生产计数
		public string ProductCount
		{
			get { return _productCount; }
			set 
			{ 
				_productCount = value; 
				RaisePropertyChanged();
			}
		}


		private string _unqualifiedCount;

		// 不良计数
		public string UnqualifiedCount
        {
			get { return _unqualifiedCount; }
			set 
			{ 
				_unqualifiedCount = value; 
				RaisePropertyChanged();
			}
		}

		public MainWindowViewModel()
        {
            UpdateTime();
			_machineCount = "298";
			_productCount = "1643";
			_unqualifiedCount = "34";
        }

		private void UpdateTime()
		{
			TimeStr = DateTime.Now.ToString("HH:mm");
			DateStr = DateTime.Now.ToString("yyyy-MM-dd");
			WeekStr = GetChineseWeek();
		}

		private string GetChineseWeek()
		{
			int weekIndex = (int)DateTime.Now.DayOfWeek;
			string[] weeks = new string[7] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
			return weeks[weekIndex];
		}
    }
}
