using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Quests
{
    public interface IQuest
    {
        int Progress { get; set; }
        int Goal { get; }
        QuestTypes QuestType { get; }
        string Description { get; }

        string KillTarget { get; set; }
        string CaptureTarget { get; set; }
        Towns TravelTarget { get; set; }

        Towns OriginTown { get; set; }

        int Reward { get; set; }

        bool goldAwarded { get; set; }
    }
}
