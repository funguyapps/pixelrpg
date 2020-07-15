using PixelRPG.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Quests
{
    [Serializable]
    public class CaptureQuest : IQuest
    {
        public int Progress { get; set; }
        public int Goal { get; }
        public QuestTypes QuestType { get => QuestTypes.capture; }
        public string Description { get; }

        private string captureTarget;
        public string CaptureTarget { get => captureTarget; set => captureTarget = value; }

        public string KillTarget { get => null; set { return; } }
        public Towns TravelTarget { get => Towns.none; set { return; } }

        public Towns OriginTown { get; set; }

        public int Reward { get; set; }

        public bool goldAwarded { get; set; }

        public CaptureQuest(string target, int goal, string description, Towns originTown, int reward)
        {
            captureTarget = target;
            Goal = goal;
            Description = description;
            OriginTown = originTown;
            Reward = reward;
        }

        public CaptureQuest()
        {

        }
    }
}
