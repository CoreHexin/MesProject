using MesProject.Models;

namespace MesProject.ViewModels
{
    public class WorkshopDetailViewModel : NotificationObject
    {
        private List<MachineModel> _machineModels;

        public List<MachineModel> MachineModels
        {
            get { return _machineModels; }
            set
            {
                _machineModels = value;
                RaisePropertyChanged();
            }
        }

        public WorkshopDetailViewModel()
        {
            _machineModels = new List<MachineModel>();
            GenerateMachineModels();
        }

        private void GenerateMachineModels()
        {
            var random = new Random();
            for (int i = 0; i < 20; i++)
            {
                int planNum = random.Next(100, 1000);
                MachineModels.Add(
                    new MachineModel()
                    {
                        Name = $"焊接机-{i + 1}",
                        PlanNum = planNum,
                        CompleteNum = random.Next(0, planNum),
                        Status = "作业中",
                        OrderNo = $"H00100-{i + 1}",
                    }
                );
            }
        }
    }
}
