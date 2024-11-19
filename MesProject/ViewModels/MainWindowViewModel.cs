using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using MesProject.Models;
using SkiaSharp;

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

        public List<EnvironmentModel> EnvironmentModels { get; set; }

        private List<AlarmModel> _alarmModels;

        // 报警集合
        public List<AlarmModel> AlarmModels
        {
            get { return _alarmModels; }
            set
            {
                _alarmModels = value;
                RaisePropertyChanged();
            }
        }

        // 产能树状图数据集
        public ISeries[] CapacitySeries { get; set; }

        public Axis[] CapacityXAxes { get; set; }
        public Axis[] CapacityYAxes { get; set; }

        // 质量折线图数据集
        public ISeries[] QualitySeries { get; set; }

        public Axis[] QualityXAxes { get; set; }
        public Axis[] QualityYAxes { get; set; }

        public MainWindowViewModel()
        {
            UpdateTime();
            _machineCount = "298";
            _productCount = "1643";
            _unqualifiedCount = "34";

            EnvironmentModels = new List<EnvironmentModel>()
            {
                new EnvironmentModel() { Name = "光照(Lux)", Value = 123 },
                new EnvironmentModel() { Name = "噪音(db)", Value = 55 },
                new EnvironmentModel() { Name = "温度(℃)", Value = 80 },
                new EnvironmentModel() { Name = "湿度(%)", Value = 43 },
                new EnvironmentModel() { Name = "PM2.5", Value = 20 },
                new EnvironmentModel() { Name = "硫化氢(PPM)", Value = 15 },
                new EnvironmentModel() { Name = "氮气(PPM)", Value = 18 },
            };

            _alarmModels = new List<AlarmModel>()
            {
                new AlarmModel()
                {
                    Num = "01",
                    Message = "设备温度过高",
                    DateTime = DateTime.Now,
                    Duration = 7,
                },
                new AlarmModel()
                {
                    Num = "02",
                    Message = "车间温度过高",
                    DateTime = DateTime.Now,
                    Duration = 10,
                },
                new AlarmModel()
                {
                    Num = "03",
                    Message = "设备转速过快",
                    DateTime = DateTime.Now,
                    Duration = 12,
                },
                new AlarmModel()
                {
                    Num = "04",
                    Message = "设备气压偏低",
                    DateTime = DateTime.Now,
                    Duration = 90,
                },
            };

            CapacitySeries = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    Values = new int[] { 100, 200, 480, 450, 380, 450, 450, 330, 340 },
                    Name = "生产计数",
                    MaxBarWidth = 15,
                },
                new ColumnSeries<int>
                {
                    Values = new int[] { 15, 55, 15, 40, 38, 45 },
                    Name = "不良计数",
                    MaxBarWidth = 15,
                    Fill = new SolidColorPaint(SKColors.IndianRed),
                },
            };

            CapacityXAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[]
                    {
                        "8:00",
                        "9:00",
                        "10:00",
                        "11:00",
                        "12:00",
                        "13:00",
                        "14:00",
                        "15:00",
                        "16:00",
                    },
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,
                },
            };

            CapacityYAxes = new Axis[]
            {
                new Axis
                {
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,
                    ForceStepToMin = true,
                    MinStep = 100,
                    MinLimit = 0,
                    SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(50))
                    {
                        PathEffect = new DashEffect(new float[] { 3, 3 }),
                    },
                },
            };

            QualitySeries = new ISeries[]
            {
                new LineSeries<int> { Values = new int[] { 8, 2, 7, 6, 4, 14 }, Name = "质量" },
            };

            QualityXAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "1#", "2#", "3#", "4#", "5#", "6#" },
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,
                },
            };

            QualityYAxes = new Axis[]
            {
                new Axis
                {
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,
                    MinStep = 5,
                    ForceStepToMin = true,
                    MinLimit = 0,
                    SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(50))
                    {
                        PathEffect = new DashEffect(new float[] { 3, 3 }),
                    },
                },
            };
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
            string[] weeks = new string[7]
            {
                "星期日",
                "星期一",
                "星期二",
                "星期三",
                "星期四",
                "星期五",
                "星期六",
            };
            return weeks[weekIndex];
        }
    }
}
