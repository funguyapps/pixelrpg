using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Quests
{
    [Serializable]
    public class TravelQuest : IQuest
    {
        public int Progress { get; set; }
        public int Goal { get; set; }
        public QuestTypes QuestType { get => QuestTypes.travel; }
        public string Description { get; }

        private Towns travelTarget;
        public Towns TravelTarget { get => travelTarget; set => travelTarget = value; }

        public string KillTarget { get => null; set { return; } }
        public string CaptureTarget { get => null; set { return; } }

        public Towns OriginTown { get; set; }

        public int Reward { get; set; }

        public bool goldAwarded { get; set; }

        public TravelQuest(Towns target, int goal, string description, Towns originTown, int reward)
        {
            travelTarget = target;
            Goal = goal;
            Description = description;
            OriginTown = originTown;
            Reward = reward;
        }

        public TravelQuest()
        {

        }
    }
}
