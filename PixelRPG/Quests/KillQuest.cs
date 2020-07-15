using PixelRPG.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Quests
{
    [Serializable]
    public class KillQuest : IQuest
    {
        public int Progress { get; set; }
        public int Goal { get; }
        public QuestTypes QuestType { get => QuestTypes.kill; }
        public string Description { get; }

        private string killTarget;
        public string KillTarget { get => killTarget; set => killTarget = value; }

        public string CaptureTarget { get => null; set { return; } }
        public Towns TravelTarget { get => Towns.none; set { return; } }

        public Towns OriginTown { get; set; }

        public int Reward { get; set; }

        public bool goldAwarded { get; set; }

        public KillQuest(string target, int goal, string description, Towns originTown, int reward)
        {
            killTarget = target;
            Goal = goal;
            Description = description;
            OriginTown = originTown;
            Reward = reward;
        }

        public KillQuest()
        {

        }
    }
}
