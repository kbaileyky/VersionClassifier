using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryClassifier
{
    public enum desc { Bug, Feature, Enhancement, Junk , Ads, RevChangeRequest, NotClassified};

    public enum appType { Desktop, Mobile, Sibling, NotClassified };

    interface ApplucationType
    {
        appType type { get; }

    }

}
