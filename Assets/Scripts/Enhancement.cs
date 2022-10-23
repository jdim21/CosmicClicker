using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enhancements
{
    public enum EnhancementType
    {
        Potency,
        Focus,
        Timed,
        Precision,
    }

    public enum EnhancementQuality
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }
}

public class Enhancement
{

    public Enhancements.EnhancementType type;
    public Enhancements.EnhancementQuality quality;
    public int baseDamage;

    public Enhancement(Enhancements.EnhancementType enhancementType, Enhancements.EnhancementQuality quality, int baseDamage)
    {
        this.type = enhancementType;
        this.baseDamage = baseDamage;
        this.quality = quality;
    }
}
